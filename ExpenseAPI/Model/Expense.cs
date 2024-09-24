//這個是用來表示用來代表一筆支出的entity
//這個Entity 有幾個欄位 分別是Id,Date,Description,Amount
//Id 是一個整數，Date 是一個日期，Description 是一個字串，Amount 是一個浮點數

using System;
using System.ComponentModel.DataAnnotations;
namespace ExpenseAPI.Model
{
    public record Expense
    {
        public int Id { get; init; }
        [Required]
        public DateTime Date { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public double Amount { get; init; }
    }
}