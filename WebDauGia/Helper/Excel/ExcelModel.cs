using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDauGia.Helper.Excel
{
    public class ExcelModel
    {
        public string IdUser { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string NameProduct { get; set; }
        public System.DateTime DateCreate { get; set; }

        public Nullable<bool> Status { get; set; }



    }
}