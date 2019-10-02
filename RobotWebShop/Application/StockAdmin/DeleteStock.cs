using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.StockAdmin
{
    public class DeleteStock
    {
        private AppDbContext _ctx;

        public DeleteStock(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int id)
        {
            var stock = _ctx.Stock.FirstOrDefault(x => x.StockID == id);

            _ctx.Stock.Remove(stock);

            await _ctx.SaveChangesAsync();

            return true;
        }

    }
}
