using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mesaj.Models;

namespace mesaj.ViewModel
{
    public class GrupModel
    {
        public int GrupId { get; set; }
        public string GrupAdi { get; set; }
        public int? Kurucu { get; internal set; }
    }
}