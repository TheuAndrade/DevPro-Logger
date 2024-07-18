class Program
{
    static void Main(string[] args)
    {
        Logger.LogMessage("application.log", "User logged in", "INFO");
        Logger.LogMessage("application.log", "Failed login attempt", "WARNING");
    }
}
