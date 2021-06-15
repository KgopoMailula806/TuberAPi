using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models.NonDatabaseModels
{
    public class PaymentReport
    {
        public string ClientName { get; set; }
        public string ModuleName { get; set; }
        public double Amount { get; set; }
        public string DateIssued { get; set; }


        public PaymentReport(string ClientName, string ModuleName, double Amount, string DateIssued)
        {
            this.ClientName = ClientName;
            this.ModuleName = ModuleName;
            this.Amount = Amount;
            this.DateIssued = DateIssued;
        }

    }
}
