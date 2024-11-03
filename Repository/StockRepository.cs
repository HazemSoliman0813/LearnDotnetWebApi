using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository(ApplicationDbContext applicationDbContext) : IStockRepository
{
    public Task<List<StockDto>> GetAllStocksAsync()
    {
        return applicationDbContext.Stocks.AsNoTracking().Select(s => s.ToStockDto()).ToListAsync();
    }
}