using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string invoiceId = SITEConstants.BASE_URL + "api/Invoice/setInvoiceToPaid/" + Request.QueryString["invoiceID"];
            
            if (!invoiceId.Equals("0"))
            {               
                    Response.Redirect("Confirmed.aspx?ConfirmatioType=PaymentCompletion");               
            }
        }
    }
}