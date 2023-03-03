namespace SaveUp.MigrateDatabase.CLI.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception inner) : base(message, inner)
        {

        }

        public DatabaseException(string message)
            : base(message)
        {

        }
    }
}
