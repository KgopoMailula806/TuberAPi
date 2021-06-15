using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace QuadCore_Website
{
    public partial class AddRemoveModules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //type=add&mID=" + module.Id + "&uID=" + Session["ID"]

            string type = Request.QueryString["type"];
            string mID = Request.QueryString["mID"];
            string uID = Request.QueryString["uID"];
            string userType = Session["UserStatus"].ToString();

            if(userType == "Manager")
            {
                if(type == "add")
                {
                    //int flag = ModuleFunctionality.addModule(mID);
                }
                else if(type == "rem")
                {
                    int flag = ModuleFunctionality.removeModule(mID);
                }
            }
            else
            {
                if (type == "add")
                {
                    int flag = ModuleFunctionality.addNewModule(uID, Convert.ToInt32(mID), userType);

                    if (flag == 1)
                    {
                        Response.Redirect("UserModules.aspx");
                    }
                    else if (flag == 0)
                    {
                        HttpContext.Current.Response.Write("<script>window.alert('You Already Registered For This Module in you alternative account')</script>");
                        Response.Redirect("ModuleList.aspx");
 
                    }
                    else if (flag == -1)
                    {
                        HttpContext.Current.Response.Write("<script>window.alert('Try Again Later')</script>");
                        Response.Redirect("UserModules.aspx");
                    }
                }
                else if (type == "rem")
                {
                    int flag = ModuleFunctionality.removeModule(uID, Convert.ToInt32(mID), userType);

                    if (flag == 1)
                    {
                        HttpContext.Current.Response.Write("<script>window.alert('Module Removed')</script>");
                        Response.Redirect("UserModules.aspx");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>window.alert('Try Again Later')</script>");
                        Response.Redirect("Home.aspx");
                    }
                }
            }

            


        }
    }
}