using api.Dtos.Stock;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<List<StockDto>> GetAllStocksAsync();
}