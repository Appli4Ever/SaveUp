namespace SaveUp.Web.API.Exception
{
    public class SaveUpDatabaseException : System.Exception
    {
        public SaveUpDatabaseException(string Message)
        : base(Message)
        {
        }

        public SaveUpDatabaseException(string Message, System.Exception innerException)
        : base(Message, innerException)
        {
        }
    }
}
