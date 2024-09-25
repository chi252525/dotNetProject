//這個 API Controller 用來處理 Expense 相關的 HTTP Request
//這個 Controller 使用Expense.cs 這個model 來處理資料
//這個 Controller 會使用ExpenseContext.cs 這個model來存取資料庫
//api 的路徑是/api/expense

/// <summary>
/// 新增一筆Expense 資料
/// </summary>
/// <param name="expense">Expense 資料</param>
/// <returns> 新增的Expense 資料<returns>
/// <response code="400">午餐不能超過400</response>
/// <remarks>
/// Sample request
/// 

using System;   
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ExpenseAPI.Model;

namespace ExpenseAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> Get()
        {
            return _context.Expenses;
        }

        [HttpGet("{id}")]
        public ActionResult<Expense> Get(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            return expense;
        }

        [HttpPost]
        //q: 提供curl 呼叫的範例
        //a: curl -X POST http://localhost:5000/api/expenses -H "Content-Type: application/json" -d '{"amount": 100, "description": "Office Supplies", "date": "2023-10-01T00:00:00"}'
        // 如果Description 是午餐 ，要回傳 400 並且說明午餐不能報帳
        public ActionResult<Expense> Post(Expense expense)
        {
            if (expense.Description == "午餐")
            {
                return BadRequest("午餐不能報帳");
            }
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return NoContent();
        }
    }
}