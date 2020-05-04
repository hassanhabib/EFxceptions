// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Models.Exceptions
{
    public class ConstraintConflictException : EFxceptionException
    {
        //https://regex101.com/r/MPsAM5/6
        //https://regexr.com/53t1o

        private static readonly string SqlMessageTemplateRegexPattern =
            $@"^(?:The (?<{RegexGroupName.StatementType}>INSERT|UPDATE|DELETE|ALTER TABLE) statement " +
            $@"conflicted with the (?<{RegexGroupName.ConstraintType}>FOREIGN KEY|REFERENCE|CHECK|SAME TABLE REFERENCE) constraint " +
            $@"\""(?<{RegexGroupName.ConstraintName}>.+)\""\. The conflict occurred in database " +
            $@"\""(?<{RegexGroupName.DatabaseName}>.+)\""\, (?:(?:table \""(?<{RegexGroupName.TableName}>.+)\""\.)|" +
            $@"(?:table \""(?<{RegexGroupName.TableName}>.+)\"", column \'(?<{RegexGroupName.ColumnName}>.+)\'\.)))$";

        private static readonly Regex SqlMessageRegex = new Regex(SqlMessageTemplateRegexPattern, RegexOptions.Compiled);

        public ConstraintConflictException(DbUpdateException sourceException) : base(sourceException)
        {

            //if(!(sourceException.GetBaseException() is SqlException))
            if (sourceException.GetBaseException() is SqlException sqlException && sqlException.Number == 547)
            {
                ParseSqlMessage(sqlException.Errors[0].Message,
                    ref this.statementType,
                    ref this.constraintType,
                    ref this.constraintName,
                    ref this.databaseName,
                    ref this.tableName,
                    ref this.columnName);
                return;
            }

            throw new ArgumentException(
                message: $"Unsupported exception." /*TODO be more specific */,
                paramName: nameof(sourceException));
        }

        static void ParseSqlMessage
            (string sqlErrorMessage, ref StatementType statementType, ref ConstraintType constraintType,
            ref string constraintName, ref string databaseName, ref string tableName, ref string? columnName)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(sqlErrorMessage));

            var match = SqlMessageRegex.Match(sqlErrorMessage);

            Debug.Assert(match.Success);
            var groups =
                match
                .Groups
                .Cast<Group>()
                .Skip(1) //first group is always the whole match
                .ToArray();

            Debug.Assert(groups.Length == 5 || groups.Length == 6);
            Debug.Assert(groups.Count(g => g.Success) >= 5); //column name is optional

            foreach (var group in groups)
            {
                if (group.Value.Length == 0)
                {
                    continue;
                }

                var groupNameLiteral = getGroupName(group);

                //important to have IsDefined before, because group.Value may be "0"
                var groupName = (RegexGroupName)Enum.Parse(typeof(RegexGroupName), groupNameLiteral);
                switch (groupName)
                {
                    case RegexGroupName.StatementType:
                        var statementTypeLiteral = removeSpaces(group.Value);

                        statementType =
                            (StatementType)Enum.Parse(
                                enumType: typeof(StatementType),
                                value: statementTypeLiteral,
                                ignoreCase: true);

                        continue;
                    case RegexGroupName.ConstraintType:
                        var constraintTypeLiteral = removeSpaces(group.Value);

                        constraintType =
                            (ConstraintType)Enum.Parse(
                                enumType: typeof(ConstraintType),
                                value: constraintTypeLiteral,
                                ignoreCase: true);

                        continue;
                    case RegexGroupName.ConstraintName:
                        constraintName = group.Value;
                        continue;
                    case RegexGroupName.DatabaseName:
                        databaseName = group.Value;
                        continue;
                    case RegexGroupName.TableName:
                        tableName = group.Value;
                        continue;
                    case RegexGroupName.ColumnName:
                        columnName = group.Value;
                        continue;
                }
            }

            static string getGroupName(Group group)
            {
                dynamic dynamicGroup = group;
                return dynamicGroup.Name;
            }

            static string removeSpaces(string input) => input.Replace(" ", string.Empty);
        }

        public override string EndUserMessage { get; }

        private readonly StatementType statementType;
        public StatementType StatementType => statementType;

        private readonly ConstraintType constraintType;
        public ConstraintType ConstraintType => constraintType;

        private readonly string constraintName;
        public string ConstraintName => constraintName;

        private readonly string databaseName;
        public string DatabaseName => databaseName;

        private readonly string tableName;
        public string TableName => tableName;

        private readonly string? columnName;
        public string? ColumnName => columnName; //may not be present in older SQL versions - research!

        private enum RegexGroupName
        {
            StatementType,
            ConstraintType,
            ConstraintName,
            DatabaseName,
            TableName,
            ColumnName
        }
    }

    public enum StatementType
    {
        //INSERT
        Insert,
        //UPDATE
        Update,
        //DELETE
        Delete,
        //ALTER TABLE
        AlterTable,
    }

    public enum ConstraintType
    {
        //FOREIGN KEY
        ForeignKey,
        //REFERENCE
        Reference,
        //CHECK
        Check,
        //SAME TABLE REFERENCE
        SameTableReference,
    }
}
