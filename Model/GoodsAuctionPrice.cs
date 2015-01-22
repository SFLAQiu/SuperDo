using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class GoodsAuctionPrice {
        public int ProductId { get; set; }
        public int AuctionCount { get; set; }
        public decimal AuctionPrice { get; set; }
    }
}
