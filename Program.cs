using CostAccounting.Core;
using CostAccounting.Data;

namespace CostAccounting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IStockData>(provider =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "stock_data.json");
                return new StockData(filePath);
            });
            builder.Services.AddScoped<IShareCalculator, ShareCalculator>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
