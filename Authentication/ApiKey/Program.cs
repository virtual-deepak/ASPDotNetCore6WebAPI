namespace Authentication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.UseApiKey();
            app.Run();
        }
    }
}
