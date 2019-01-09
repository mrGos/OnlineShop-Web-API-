using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/home")]
    //[Authorize]
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        ICommonService _commonService;
        public HomeController(IErrorService errorService, ICommonService commonService) : base(errorService)
        {
            this._errorService = errorService;
            _commonService = commonService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAllBanners(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var slideModel = _commonService.GetSlides();
                    var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
                    var homeViewModel = new HomeViewModel();
                    homeViewModel.Slides = slideView;

                    response = request.CreateResponse(HttpStatusCode.Created, homeViewModel);
                }

                return response;
            });
        }

    }
}
