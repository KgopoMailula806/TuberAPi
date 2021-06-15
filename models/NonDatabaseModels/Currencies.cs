using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadCore_Website.models.NonDatabaseModels
{
    public class Currencies
    {
        public string @base { get; set; }
        public Rates rates { get; set; }
        public string date { get; set; }


    }
}
