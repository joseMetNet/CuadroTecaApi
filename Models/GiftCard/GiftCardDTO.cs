using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.GiftCard
{
    public class GiftCarResponse
    {
        public string api_Dice { get; set; }
        public int idGiftCard { get; set; }
        public string NameGiftCard { get; set; }
        public int idStatusGiftCard { get; set; }
        public string nameStatus { get; set; }
    }
    public class GiftCarResponse2
    {
        public int idGiftCard { get; set; }
        public string NameGiftCard { get; set; }
        public string reference { get; set; }
        public decimal value { get; set; }
        public int  idStatusGiftCard { get; set; }
        public string Status { get; set; }
        public string dateAdd { get; set; }


    }
    public class GiftCarResqueride 
    {
        public int idGiftCard { get; set; }
        public string NameGiftCard { get; set; }
        public string reference { get; set; }
        public decimal value { get; set; }
    }
    public class GiftCarStatusResqueride
    {
        public int idGiftCard { get; set; }
        public int idStatus { get; set; }
    }
}
