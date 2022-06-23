using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mesaj.Models;

namespace mesaj.ViewModel
{
    public class MesajModel
    {
        public int MesajId { get; set; }
        public string Mesaj1 { get; set; }
        public int? Gonderen { get; internal set; }
    }
}