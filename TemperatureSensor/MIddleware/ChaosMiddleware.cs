public class ChaosMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Random _random = new();

    public ChaosMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        int roll = _random.Next(0, 100);
        if (roll < 10)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Chaos: Internal Server Error");
            return;
        }

        // 2. 10% - "connection reset"
        if (roll < 20)
        {
            throw new HttpRequestException("ECONNRESET: simulated network failure");
        }

        // 3. 20% - "timeout"
        if (roll < 40)
        {
            await Task.Delay(10_000);
            throw new TimeoutException();
        }

        // 4. 20% - "slow"
        if (roll < 60)
        {
            await Task.Delay(1500);
        }
        await _next(context);
    }
}