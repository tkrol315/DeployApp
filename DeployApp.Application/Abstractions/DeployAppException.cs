namespace DeployApp.Application.Abstractions
{
    public abstract class DeployAppException : Exception
    {
        public int StatusCode { get; set; }
        protected DeployAppException(string message, int statusCode) : base(message) { StatusCode = statusCode; }
    }
}
