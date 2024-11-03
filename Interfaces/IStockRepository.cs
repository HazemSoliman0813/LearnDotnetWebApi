using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<List<StockDto>> GetAllStocksAsync();
    Task<Stock?> GetStockByIdAsync(int id);
    Task<Stock> CreateStockAsync(Stock stock);
    Task<Stock?> UpdateStockAsync(int id, UpdatedStockDto stock);
    Task<Stock?> DeleteStockAsync(int id);
}