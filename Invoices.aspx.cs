using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class Invoices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string invoice_type = Request.QueryString["As"];
            makeInvoice(invoice_type);
        }

        public void makeInvoice(string type)
        {
            //the type can be used as a filter

            //get the client table ID
            string clientId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTablereferenceByForeignKey/" + Session["ID"].ToString());
            //Get the invoices
            if (!clientId.Equals("0"))
            {
                //Get the invoices
                string invoiceURl = SITEConstants.BASE_URL + "api/Invoice/GetUserInvoicesUnPaidInvoices/" + clientId;

                string invoiceJsonCollection = ApiComnunication.getJsonEntities(invoiceURl);

                if (!invoiceJsonCollection.Equals("0"))
                {
                    List<Invoice> Invoices = JsonConvert.DeserializeObject<List<Invoice>>(invoiceJsonCollection);
                    foreach (Invoice invoice in Invoices)
                    {
                        string entries = "";
                        entries = "<tr><td><a href='InvoiceDetails.aspx?invoiceID=" + invoice.Id + "&sessionID=" + invoice.Session_ID + "'>" + invoice.Description + "</a></td><td></td>";
                        entries += "<td>" + invoice.Date_Issued + "</td>";
                        entries += "<td> R "+ invoice.Amount + " </td><td></td>";
                        entries += "<td><a href='PaypalPayOut.aspx?invoiceID=" + invoice.Id + "&amount=" + invoice.Amount + "&sessionID=" + invoice.Session_ID + "' class='btn btn-primary'>pay</a></td></tr>";
                        InvoiceEntries.InnerHtml = entries;

                        if (type == "summary")
                        {

                        }
                        else if (type == "month")
                        {

                        }
                    }

                }
            }            
        }
    }
}