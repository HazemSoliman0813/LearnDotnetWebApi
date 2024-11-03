using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext context, StockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await stockRepository.GetAllStocksAsync();
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await context.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stock)
    {
        var stockModel = stock.ToStockfromCreate();
        await context.Stocks.AddAsync(stockModel);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatedStockDto stockDto)
    {
        var stockModel = await context.Stocks.FindAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        context.Entry(stockModel).CurrentValues.SetValues(stockDto);
        await context.SaveChangesAsync();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await context.Stocks.FindAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        context.Stocks.Remove(stockModel);
        await context.SaveChangesAsync();
        return NoContent();
    }
}