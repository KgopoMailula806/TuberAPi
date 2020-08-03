using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private TuberDbContext dbContext;

        public InvoiceController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllInvoices()
        {
            return Ok(dbContext.Invoices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientID}")]
        public IActionResult GetUserInvoicesUnPaidInvoices(int clientID)
        {
            var invoice = (from inv in dbContext.Invoices
                           where inv.Client_ID.Equals(clientID) && inv.is_Paid.Equals(0)
                           select inv).ToList();

            if (invoice != null)
            {
                return Ok(invoice);
            } else
                return Ok(0);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientID}")]
        public IActionResult GetUserInvoicesPaidInvoices(int clientID)
        {
            var invoice = (from inv in dbContext.Invoices
                           where inv.Client_ID.Equals(clientID) && inv.is_Paid.Equals(1)
                           select inv).ToList();

            if (invoice != null)
            {
                return Ok(invoice);
            }
            else
                return Ok(0);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{invoiceID}")]
        public IActionResult setInvoiceToPaid(int invoiceID)
        {
            var invoice = (from inv in dbContext.Invoices
                           where inv.Id.Equals(invoiceID)
                           select inv).ToList();

            if (invoice != null)
            {
                invoice.ElementAt(0).is_Paid = 1;
                dbContext.SaveChanges();
                return Ok(invoice.ElementAt(0).Id);
            }
            else
                return Ok(0);

        }
    }
}