using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class Confirmed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Email"] == null)
              //  Response.Redirect("Login.aspx");
           
            if(Request.QueryString["ConfirmatioType"] != null)
            {
                //ConfirmatioType = BookingRequest
                switch (Request.QueryString["ConfirmatioType"])
                {
                    case "BookingRequest":
                    {
                            string innerHtmlCode = "<h1 class='mb-2 bread'>Request Confirmed</h1>";
                            innerHtmlCode += "<h7 class='mb-2 bread'>a tutor will be assigned to you ASAP</h7>";
                            ConformationInput.InnerHtml = innerHtmlCode;
                    }break;
                    case "Booking_Finalization":
                    {
                            string innerHtmlCode = "<h1 class='mb-2 bread'>Booking has been finalised</h1>";
                            innerHtmlCode += "<h7 class='mb-2 bread'>The client should receive a notification</h7>";
                            ConformationInput.InnerHtml = innerHtmlCode;
                            /// TODO: Button that redirects to Booked sessions
                    }
                    break;
                    case "BookingRenegotiation":
                        {
                            string innerHtmlCode = "<h1 class='mb-2 bread'>Alternative Date submitted</h1>";
                            if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                            {                                
                                innerHtmlCode += "<h7 class='mb-2 bread'>The Tutor should receive a notification</h7>";
                             
                            }
                            else
                            {                              
                                innerHtmlCode += "<h7 class='mb-2 bread'>The client should receive a notification</h7>";                                
                            }
                            ConformationInput.InnerHtml = innerHtmlCode;
                        }
                        break;
                    case "PaymentCompletion":
                    {
                        string innerHtmlCode = "<h7 class='mb-2 bread'>Payment has be finalized thank you ery much</h7>";                                                        
                        ConformationInput.InnerHtml = innerHtmlCode;
                    }
                    break;
                    case "BookingRejection":
                        {
                            if (Request.QueryString["status"] != null)
                            {
                                switch (Request.QueryString["status"])
                                {
                                    case "Client" :
                                        {
                                            string innerHtmlCode = "<h7 class='mb-2 bread'>Tutor will be notified off the booking decline</h7>";
                                            ConformationInput.InnerHtml = innerHtmlCode;
                                        }
                                        break;
                                    case "Tutor":
                                        {
                                            string innerHtmlCode = "<h7 class='mb-2 bread'>Client will be notified of the booking decline</h7>";
                                            ConformationInput.InnerHtml = innerHtmlCode;
                                        }
                                        break;
                                }
                            }                      
                        }
                        break;
                    case "AccountDeactivation":
                        {
                           
                            string innerHtmlCode = "<h7 class='mb-2 bread'>This account has been deactivated</h7>";
                            ConformationInput.InnerHtml = innerHtmlCode;
                           
                        }
                        break;

                }
            }
            
        }
    }
}