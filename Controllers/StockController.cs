using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = context.Stocks.AsNoTracking().Select(s => s.ToStockDto()).ToList();
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = context.Stocks.Find(id)?.ToStockDto();
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockDto stock)
    {
        var stockModel = stock.ToStockfromCreate();
        context.Stocks.Add(stockModel);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
}