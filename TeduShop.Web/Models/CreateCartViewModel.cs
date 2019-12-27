using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class CreateCartViewModel
    {
        public OrderViewModel orderViewModel { get; set; }
        public List<ProductViewModel> listcart { get; set; }
    }

    public class ProductOrderModel
    {
        public int ID { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int Quantity { set; get; }

    }
    
}