using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/shoppingcart")]
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
        public HttpResponseMessage CreateOrder(HttpRequestMessage request, [FromBody]CreateCartViewModel parameters)
        {
            OrderResult orderResult = new OrderResult()
            {
                status = false,
                message = "Có lỗi xảy rả với hệ thống"
            };
            
            if (parameters == null)
            {
                orderResult.message = "Dữ liệu rỗng.";

                return request.CreateResponse(HttpStatusCode.OK, orderResult);
            }
            else
            {
                if(parameters.orderViewModel == null || parameters.listcart == null)
                {
                    orderResult.message = "Dữ liệu không hợp lệ.";
                    return request.CreateResponse(HttpStatusCode.OK, orderResult);
                }
            }

            try
            {
                var order = parameters.orderViewModel;

                var orderNew = new Order();

                orderNew.UpdateOrder(order);

                // var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
                var cart = parameters.listcart;
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                bool isEnough = true;
                foreach (var item in cart)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.ProductID = item.ID;
                    detail.Quantity = item.Quantity;
                    detail.Price = item.Price;


                    if (detail.ProductID == 0)
                    {
                        orderResult.message = "Mã sản phẩm hoặc số lượng không hợp lệ";
                        return request.CreateResponse(HttpStatusCode.OK, orderResult);
                    }
                    else
                    {
                        if (detail.Quantity == 0)
                        {
                            detail.Quantity = 1;
                        }
                    }

                    orderDetails.Add(detail);

                    isEnough = _productService.SellProduct(detail.ProductID ,detail.Quantity);
                    break;
                }
                if (isEnough)
                {
                    var orderReturn = _orderService.Create(ref orderNew, orderDetails);
                    _productService.Save();

                    orderResult.status = true;
                    orderResult.message = "Đặt hàng thành công";
                    return request.CreateResponse(HttpStatusCode.OK, orderResult);

                }
                else
                {
                    orderResult.message = "Không đủ hàng.";
                    return request.CreateResponse(HttpStatusCode.OK, orderResult);
                }
            }
            catch(Exception e)
            {
                orderResult.message = e.ToString();
            }


            return request.CreateResponse(HttpStatusCode.OK, orderResult);

        }

    }

    public class OrderResult
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
