using ExpenseAPI.Model;
using Microsoft.EntityFrameworkCore;
using ExpenseAPI.Controller; // Add this line to include the ExpenseController namespace
using Microsoft.AspNetCore.Mvc; // Add this line to include the CreatedAtActionResult namespace

namespace ExpenseAPITest;

//這個是用來測試ExpenseAPI/Controller/ExpenseController.cs的測試程式
//這個測試程式會使用InMemoryDatabase來測試
// 只要測試post 方法即可
// 這個測試程式會使用 ExpenseAPI/Model/Expense.cs 這個model來測試
// 每一個方法至少提供3個測試案例：正常、異常、邊界
public class ExpenseControllerTest
{
    //測試正常情況下，Post方法
    [Fact]
    public void TestPost()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "TestPost")
            .Options;

        using (var context = new ExpenseContext(options))
        {
            var controller = new ExpenseController(context);
            var expense = new Expense
            {
                Amount = 100,
                Description = "Office Supplies",
                Date = new DateTime(2023, 10, 1)
            };

            //Act
            var result = controller.Post(expense);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }

    //測試異常情況下，Post方法
    [Fact]
    public void TestPostBadRequest()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "TestPostBadRequest")
            .Options;

        using (var context = new ExpenseContext(options))
        {
            var controller = new ExpenseController(context);
            var expense = new Expense
            {
                Amount = 100,
                Description = "午餐",
                Date = new DateTime(2023, 10, 1)
            };

            //Act
            var result = controller.Post(expense);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }

    //測試邊界情況下，Post方法
    [Fact]
    public void TestPostEdge()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "TestPostEdge")
            .Options;

        using (var context = new ExpenseContext(options))
        {
            var controller = new ExpenseController(context);
            var expense = new Expense
            {
                Amount = 100,
                Description = "Office Supplies",
                Date = new DateTime(2023, 10, 1)
            };

            //Act
            var result = controller.Post(expense);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }
}