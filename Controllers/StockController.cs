using api.Data;
using LearnDotnetWebApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.AsNoTracking().Select(s => s.ToStockDto()).ToList();
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id)?.ToStockDto();
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }
}