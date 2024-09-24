//使用ExpenseContext 來產生預設資料


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseAPI.Model
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpenseContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExpenseContext>>()))
            {
                if (context.Expenses.Any())
                {
                    return;
                }
                //產生4筆資料 
              context.Expenses.AddRange(
    new Expense { Amount = 100, Description = "Office Supplies", Date = DateTime.Now },
    new Expense { Amount = 200, Description = "Travel", Date = DateTime.Now },
    new Expense { Amount = 150, Description = "Meals", Date = DateTime.Now },
    new Expense { Amount = 250, Description = "Software", Date = DateTime.Now }
);
                context.SaveChanges();
            }
        }
    }
}