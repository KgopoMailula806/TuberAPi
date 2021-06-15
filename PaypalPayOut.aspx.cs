using Newtonsoft.Json;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class PaypalPayOut : System.Web.UI.Page
    {
        
        List<String> serials = new List<String>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is logged in before performing checkout

            /*insure that you check if the user's address details have been filled in (are not null), else prompt them to do it 
             Before the continue with this page
             */
            //don't forget to delete the product after checking out
            if (!IsPostBack)
            {


                if (Request.Cookies["ProductCookie"] != null)
                {
                    //requesting all the ID's from the cookie object
                    string myprods = Request.Cookies["ProductCookie"].Value.ToString();

                    //Storing all the needed serial numbers to the cart
                    string[] Per_Prod = myprods.Split('|');



                    //Now we wanna store them in an arraylist
                    for (int i = 0; i < Per_Prod.Length; i++)
                    {
                        serials.Add(Per_Prod[i]);

                    }
                }

                String itemIDs = "";
                for (int i = 0; i < serials.Count(); i++)
                {
                    //Get session serial numbers 
                    itemIDs += serials.ElementAt(i).ToString() + "\n";
                }



                /// string serialno = Request.QueryString["CHECKID"];
                /// var p = pc.getProduct(serialno);

                //Generating a random number for an order
                Random rand = new Random();
                int orderNumber = rand.Next();
                Session["Invoice_No"] = Request.QueryString["invoiceID"];

                //Convert.ToString(Math.Round(Convert.ToDouble(Session["CartTotal"].ToString()), 2)).Replace(",", ".");//replace comma with the dot
                // @GET("/api/latest?base=USD&symbols=ZAR")
                //https://api.ratesapi.io/api/latest?base=USD&symbols=ZAR
                                                
                String exchangeRates = ApiComnunication.getJsonEntities("https://api.ratesapi.io/api/latest?base=USD&symbols=ZAR");
                Currencies currencies = JsonConvert.DeserializeObject<Currencies>(exchangeRates);
                String amountInRnads = Request.QueryString["amount"].Replace(".", ",");
                QuadCore_Website.models.Rates randValue = new models.Rates();
                randValue.ZAR = double.Parse(amountInRnads);
                randValue.ZAR = randValue.ZAR / currencies.rates.ZAR;
                string amount = "" + Math.Round(randValue.ZAR, 2);
                amount = amount.Replace(",", ".");
                //TODO Convert to use dollars     
                //string display = "";
                //display +=
                //Remember we have to do this for all the items in the cart (i think)

                Response.Write("<form action='https://www.sandbox.paypal.com/cgi-bin/webscr' method='post' name='buyCredits' id='buyCredit'>");
                Response.Write("<input type='hidden' name='cmd' value='_xclick'>");
                Response.Write("<input type='hidden' name='business' value='kmailula806@gmail.com'>");//use business owmer's email here
                Response.Write("<input type='hidden' name='currency_code' value='USD'>");
                Response.Write("<input type='hidden' name='item_name' value='" + itemIDs + "'>");
                Response.Write("<input type='hidden' name='item_number' value='" + Request.QueryString["CHECKItems"] + "'>"); //any item ID/invoice ID
                Response.Write("<input type='hidden' name='amount' value='" + amount + "'>");
                Response.Write("<input type='hidden' name='return' value='http://localhost:44387/SuccessPage.aspx?invoiceID=" + Session["Invoice_No"] + "'>");
                Response.Write("</form>");

                Response.Write("<script type='text/javascript'>");
                Response.Write("document.getElementById('buyCredit').submit();");
                Response.Write("</script>");

                //payout.InnerHtml = display;

            }
        }
    }
}