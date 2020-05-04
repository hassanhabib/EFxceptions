using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using EFxceptions.Models.Exceptions;

namespace EFxceptions.Tests
{
    public class SqlConstraintConflictExceptionTestData : IEnumerable<object[]>
    {
        //https://stackoverflow.com/questions/27235475/the-update-statement-conflicted-with-the-foreign-key-constraint
        //The UPDATE statement conflicted with the FOREIGN KEY constraint The conflict occurred in database table "dbo.Administrator", column 'AdministratorId'.

        const string CasesFileName = "ConstraintConflictExceptionTestData.csv";

        IEnumerable<(string inputSqlMessage, StatementType StatementType, ConstraintType ConstraintType, string ConstraintName,
            string DatabaseName, string TableName, string? ColumnName)> ParseCsvData()
        {
            var csvLines = getCsvLinesFromFile();

            foreach (var csvLine in csvLines)
            {
                string[] csvSplitLine = csvLine.Split(',');
                string[] dataColumnLiterals =
                    csvSplitLine[..6]
                    .Select(data => data.Trim())
                    .ToArray();

                //SQL message may contain commas
                string messageColumnLiteral = string.Join(',', csvSplitLine[6..]).Trim();

                string[] noSpaceLiterals =
                    dataColumnLiterals[..2]
                    .Select(literal => literal.Replace(" ", string.Empty))
                    .ToArray();

                StatementType statementType = (StatementType)Enum.Parse(typeof(StatementType),
                    noSpaceLiterals[0], ignoreCase: true);

                ConstraintType constraintType = (ConstraintType)Enum.Parse(typeof(ConstraintType),
                    noSpaceLiterals[1], ignoreCase: true);

                string constraintName = dataColumnLiterals[2];
                string databaseName = dataColumnLiterals[3];
                string tableName = dataColumnLiterals[4];

                string? columnName = dataColumnLiterals[5];
                if (columnName?.Trim().Length == 0)
                {
                    columnName = null;
                }

                yield return (messageColumnLiteral, statementType, constraintType, constraintName, databaseName,
                      tableName, columnName);
            }

            static IEnumerable<string> getCsvLinesFromFile()
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                var resourceNames = executingAssembly.GetManifestResourceNames();
                var resourceName = resourceNames.Single(resourceName =>
                    resourceName.EndsWith(CasesFileName));

                using var csvStream = executingAssembly.GetManifestResourceStream(resourceName);

                Debug.Assert(csvStream != null);
                using var reader = new StreamReader(csvStream);

                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine()!;
                }
            }

            //https://stackoverflow.com/questions/27235475/the-update-statement-conflicted-with-the-foreign-key-constraint
            //The UPDATE statement conflicted with the FOREIGN KEY constraint The conflict occurred in database table ""dbo.Administrator"", column 'AdministratorId'.
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var testDataItem in ParseCsvData())
            {
                yield return new object[]
                {
                    testDataItem.inputSqlMessage,
                    testDataItem.StatementType,
                    testDataItem.ConstraintType,
                    testDataItem.ConstraintName,
                    testDataItem.DatabaseName,
                    testDataItem.TableName,
                    testDataItem.ColumnName!
                };
            }
        }
    }
}