using fotoTeca.Models.Dashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardDAL _repository1;
        public DashboardController( DashboardDAL repository1)
        {
            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));
        }

        [HttpPost("/storeOrUpdateVisitsWeb", Name = "storeOrUpdateVisitsWeb")]
        public async Task<List<storeOrUpdateVisitsWebResponse>> Post1(storeOrUpdateVisitsWebRequeride cli)
        {
            return await _repository1.storeOrUpdateVisitsWeb(cli);
        }

        [HttpGet("/GetSalesDashboard/{date1}/{date2}")]
        public async Task<List<GetSalesDashboardResponse>> get16( string date1 = "1000-00-00", string date2 = "1000-00-00")
        {
            return await _repository1.GetSalesDashboard( date1 , date2);
        }
        [HttpGet("/GetSalesPerDay/{date1}/{date2}")]
        public async Task<List<SalesPerDayResponse>> get17(string date1 = "1000-00-00", string date2 = "1000-00-00")
        {
            return await _repository1.GetSalesPerDay(date1, date2);
        }
        [HttpGet("/getTotalVisitsWeb")]
        public async Task<List<getTotalVisitsWebResponse>> get13()
        {
            return await _repository1.getTotalVisitsWeb();
        }
        [HttpGet("/GetOrdersManagement")]
        public async Task<List<OrdersManagementbResponse>> get14()
        {
            return await _repository1.GetOrdersManagement();
        }

        //[HttpGet("/GetOrdersManagementByid/{idOrder}")]
        //public async Task<List<OrdersManagementbResponse>> get15(int idOrder)
        //{
        //    return await _repository1.GetOrdersManagement(idOrder);
        //}

        //[HttpGet("/GetOrdersManagement/{idOrder}")]
        //public async Task<Management> get13(int idOrder)
        //{
        //    return await _repository1.GetOrdersManagementByid(idOrder);
        //}


        [HttpGet("/GetOrdersManagement/{idOrder}")]

        public async Task<ManagementGeneral> get(int idOrder)
        {
            return await _repository1.GetOrdersManagementByid(idOrder);
        }

      
        //PUT--------------------------------------------------------------------------------------------------------------------------------------

        [HttpPut("/UpdateOrder", Name = "UpdateOrder")]
        public async Task put1([FromBody] UpdateOrderRequeride us)

        {
            await _repository1.UpdateOrder(us);
        }

        [HttpPut("/UpdateOrderProduction", Name = "UpdateOrderProduction")]
        public async Task put2([FromBody] UpdateOrderProductionRequeride us)

        {
            await _repository1.UpdateOrderProduction(us);
        }

        [HttpPost("/UpdateInvoiceOrder", Name = "UpdateInvoiceOrder")]
        public async Task put3([FromBody] UpdateInvoiceOrderRequeride us)

        {
            await _repository1.UpdateInvoiceOrder(us);
        }

        public ActionResult prueba()
        {
            return View;
        }
    }
}
