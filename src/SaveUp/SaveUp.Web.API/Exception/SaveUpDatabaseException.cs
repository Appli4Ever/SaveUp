namespace SaveUp.Web.API.Exception
{
    public class SaveUpDatabaseException : System.Exception
    {
        public SaveUpDatabaseException(string message)
        : base(message)
        {
        }

        public SaveUpDatabaseException(string message, System.Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
