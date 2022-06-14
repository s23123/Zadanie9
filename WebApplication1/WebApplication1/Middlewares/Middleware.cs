using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class Middleware
    {
        private readonly RequestDelegate next;
        private readonly string sciezka = "logs.txt";

        public Middleware(RequestDelegate next)
        {
            this.next = next;
        }

       

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }catch(Exception e)
            {
                await Logs(context,e);
            }
        }

        public async Task Logs(HttpContext context, Exception e)
        {
            using var stream = new StreamWriter(sciezka, true);
            await stream.WriteLineAsync($"{DateTime.Now}, {context.TraceIdentifier},{e.HResult}");
            await next(context);
        }
    }
}
