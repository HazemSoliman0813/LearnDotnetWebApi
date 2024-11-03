using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository(ApplicationDbContext applicationDbContext) : IStockRepository
{
    public Task<List<StockDto>> GetAllStocksAsync()
    {
        return applicationDbContext.Stocks.AsNoTracking().Select(s => s.ToStockDto()).ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(int id)
    {
        return await applicationDbContext.Stocks.FindAsync(id);
    }

    public async Task<Stock> CreateStockAsync(Stock stock)
    {
        await applicationDbContext.Stocks.AddAsync(stock);
        await applicationDbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, UpdatedStockDto stock)
    {
        var existingstock = await applicationDbContext.Stocks.FindAsync(id);
        if (existingstock == null)
        {
            return null;
        }
        applicationDbContext.Entry(existingstock).CurrentValues.SetValues(stock);
        await applicationDbContext.SaveChangesAsync();
        return existingstock;
    }

    public async Task<Stock?> DeleteStockAsync(int id)
    {
        var stock = await applicationDbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return null;
        }
        applicationDbContext.Stocks.Remove(stock);
        await applicationDbContext.SaveChangesAsync();
        return stock;
    }
}