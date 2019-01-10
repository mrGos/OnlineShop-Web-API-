using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/shoppingCart")]
    public class ShoppingCartController : ApiControllerBase
    {
        #region Initialize
        private IOrderService _orderService;
        IProductService _productService;

        public ShoppingCartController(IErrorService errorService, IProductService productService, IOrderService orderService)
            : base(errorService)
        {
            this._productService = productService;
            this._orderService = orderService;
        }

        #endregion

        [Route("getallorders")]
        [HttpGet]
        public HttpResponseMessage GetAllOrders(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("createcart")]
        [HttpPost]
        public HttpResponseMessage CreateShoppingCart(HttpRequestMessage request, string orderViewModel, string listcart)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);

                //var orderNew = new Order();

                //orderNew.UpdateOrder(order);

                //var cart = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(listcart);
                //List<OrderDetail> orderDetails = new List<OrderDetail>();
                //bool isEnough = true;
                //foreach (var item in cart)
                //{
                //    var detail = new OrderDetail();
                //    detail.ProductID = item.ProductId;
                //    detail.Quantity = item.Quantity;
                //    detail.Price = item.Product.Price;
                //    orderDetails.Add(detail);

                //    isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                //    break;
                //}
                //if (isEnough)
                //{
                //    var orderReturn = _orderService.Create(ref orderNew, orderDetails);
                //    _productService.Save();
                //    response = request.CreateResponse(HttpStatusCode.Created, Json(new
                //    {
                //        status = true
                //    }));

                //}
                //else
                //{
                //    response = request.CreateResponse(HttpStatusCode.Created, Json(new
                //    {
                //        status = false
                //    }));
                //}
                response = request.CreateResponse(HttpStatusCode.Created, new { status = true });

                //var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                //response = request.CreateResponse(HttpStatusCode.Created, responseData);                

                return response;
            });
        }

    }
}
