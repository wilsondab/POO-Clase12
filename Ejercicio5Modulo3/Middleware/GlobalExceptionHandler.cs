public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
            await next(context);

        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var respuesta = new
            {
                error = $"Error en API: {context.Request.Path.Value} {context.Request.Method} " +
                $" Detalle: {ex.Message} "
            };
            await context.Response.WriteAsJsonAsync(respuesta);

        }
    }
}
