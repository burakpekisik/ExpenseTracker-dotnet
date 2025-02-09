using YahooFinanceApi;

namespace Expense_Tracker.Services
{
    public class YahooFinanceService
    {
        public async Task<float?> GetCurrentPrice(string symbol)
        {
            try
            {
                var securities = await Yahoo.Symbols(symbol).Fields(Field.RegularMarketPrice).QueryAsync();
                var security = securities[symbol];
                return (float?)security.RegularMarketPrice;
            }
            catch
            {
                return null;
            }
        }
    }
}