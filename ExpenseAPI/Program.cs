using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ExpenseAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// 設定資料庫上下文
builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseInMemoryDatabase("InMemoryExpenseDB"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Generate default data
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<ExpenseContext>();
    DataGenerator.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 新增 /api/expense 路由
app.MapGet("/api/expense", async ([FromServices] ExpenseContext db) =>
{
    return await db.Expenses.ToListAsync();
})
.WithName("GetExpenses")
.WithOpenApi();

app.Run();