using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Dashboard
{

    public class storeOrUpdateVisitsWebResponse
    {
        public int idVisitsWeb { get; set; }
        public string possiblePurchase { get; set; }
        public string City { get; set; }

    }
    public class storeOrUpdateVisitsWebRequeride
    {
        public int idVisitsWeb { get; set; }
        public string City { get; set; }
        public string PossiblePurchase { get; set; }
    }

    public class getTotalVisitsWebResponse
    {
        public string TotalVisitsWeb { get; set; }
    }

    public class OrdersManagementbResponse
    {
        public int idOrder { get; set; }
        public string idTable { get; set; }
        public string priceTotality { get; set; }
        public string dateAdd { get; set; }
        public string IdMercadoPago { get; set; }
        public string invoice { get; set; }
        public string Name { get; set; }
        public string idPromotion { get; set; }
        public string NamePromotion { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string FullNameBuyer { get; set; }
        public string TypeSale { get; set; }
        public string numberFrames { get; set; }
        public string NumberMessage { get; set; }
        public string idStatusOrder { get; set; }
        public string NameStatus { get; set; }
        public string idUser { get; set; }
        public string NameOperator { get; set; }
        public string conveyor { get; set; }
        public string guide { get; set; }
        public string MaximumDate { get; set; }
        public string dispatchDate { get; set; }
        public string MaximumDateDelivery { get; set; }
        public string StatusEmailSale { get; set; }
        public string StatusEmailShipping { get; set; }
        public string StatusEmailDelivery { get; set; }
        public string StatusPoll { get; set; }
        public string surveyResult { get; set; }
        public string country { get; set; }
        public string ActualDeliveryDate { get; set; }
    }


    public class GetSalesDashboardResponse
    {
        public string salesToday { get; set; }
        public string SalesInproduction { get; set; }
        public string SalesDispatched { get; set; }
        public string TotalsOrders { get; set; }
        public string SalesDelivered { get; set; }
        public string SalesTotals { get; set; }
        public string Time_Dispatched_production { get; set; }
        public string Time_Dispatched_average { get; set; }

    }
    public class SalesPerDayResponse
    {
        public int idOrder { get; set; }
        public string dateAdd { get; set; }
        public string quantity { get; set; }

    }
    public class UpdateOrderRequeride
    {
        public int idOrder { get; set; }
        public string StatusEmailSale { get; set; }
        public string StatusEmailShipping { get; set; }
        public string StatusEmailDelivery { get; set; }
        public string StatusPoll { get; set; }
        public string surveyResult { get; set; }
    }

    public class UpdateOrderProductionRequeride
    {
        public string idOrder { get; set; }
        public string invoice { get; set; }
        public int idStatus { get; set; }
        public int idUser { get; set; }
        public string Conveyor { get; set; }
        public string Guide { get; set; }
        public string ActualDeliveryDate { get; set; }

    }
}
