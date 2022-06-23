using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mesaj.Models;

namespace mesaj.ViewModel
{
    public class ResimModel
    {
        public int ResimId { get; set; }
        public string ResimAdi { get; set; }
        public int? KullaniciId { get; internal set; }
    }
}