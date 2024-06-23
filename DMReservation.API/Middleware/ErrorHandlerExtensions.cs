namespace DMReservation.API.Middleware
{
    public static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder UseErroHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandler>();
        }
    }
}
