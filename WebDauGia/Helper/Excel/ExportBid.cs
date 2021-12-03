using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebDauGia.Helper.Excel

{
    public class ExportBid
    {
        public int IdUser { get; set; }
       
        public string IdProduct { get; set; }
        public int BidPrice { get; set; }
        public string BidTime { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DateCreate { get; set; }
       
    }
}