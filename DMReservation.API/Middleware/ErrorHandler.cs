using DMReservation.Domain.DTOs;
using DMReservation.Domain.Exceptions;
using log4net;
using System.Diagnostics;

namespace DMReservation.API.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger("ApplicationBaseException");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (ApplicationBaseException ex)
            {
                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, ApplicationBaseException ex)
        {
            ErrorDto error = new ErrorDto(ex);

            StackTrace st = new StackTrace(ex, true);
            StackFrame? stFrame = st.GetFrame(0);
            int line = stFrame?.GetFileLineNumber() ?? -1;
            string date = $"{DateTime.Now}";

            _logger.Error($"[{ex.Code}]: {date} - Error in line {line} - {ex.Message} - {error.GetMessageSystem()}");
            _logger.Error($"[STACKTRACE]: {ex.StackTrace}");

            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsJsonAsync(error.GetMessageUser());
        }
    }
}
