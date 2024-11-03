using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext context, IStockRepository stockRepository) : ControllerBase
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
        var stock = await stockRepository.GetStockByIdAsync(id);
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
        await stockRepository.CreateStockAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatedStockDto stockDto)
    {
        var stockModel = await stockRepository.UpdateStockAsync(id, stockDto);
        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await stockRepository.DeleteStockAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}