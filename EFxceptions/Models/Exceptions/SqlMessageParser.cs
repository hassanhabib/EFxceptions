namespace EFxceptions.Models.Exceptions
{
    public class SqlMessageParser
    {
        //examples tests
        //https://regex101.com/r/MPsAM5/2

        //Identifier name tests
        //https://regexr.com/53t1o

        //Level1 unicode support
        //http://unicode.org/reports/tr18/#Basic_Unicode_Support
        //https://docs.microsoft.com/en-us/sql/relational-databases/databases/database-identifiers?view=sql-server-ver15#rules-for-regular-identifiers
        internal const string SqlValidForeignKeyConstraintIdentifierFirstCharRegexPattern = @"[\p{L}_@]"; //no number sign (#)
        internal const string SqlValidIdentifierFirstCharRegexPattern = @"[\p{L}_@#]";
        internal const string SqlValidIdentifierOtherCharsRegexPattern = @"[\p{L}\p{N}]{0,127}";
        internal const string SqlValidIdentifierRegexPattern =
            SqlValidIdentifierFirstCharRegexPattern + SqlValidIdentifierOtherCharsRegexPattern;

        internal const string SqlValidForeignKeyConstraintIdentifierRegexPattern = SqlValidForeignKeyConstraintIdentifierFirstCharRegexPattern + SqlValidIdentifierOtherCharsRegexPattern;
    }
}
