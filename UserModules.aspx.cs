using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuadCore_Website.HelperFunctionality;
using TuberAPI.models;


namespace QuadCore_Website
{
    public partial class UserModules : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Email"] == null)
                    Response.Redirect("Login.aspx");
            }

            string innerText = "";
            string userType = Session["UserStatus"].ToString();

            btnAdd.Visible = true;

            //get the clients modules
            string userId = Session["ID"].ToString();
            innerText = Display(ModuleFunctionality.getModules(userId, userType));
           
            UserDetailsDiv.InnerHtml = innerText;
            if (innerText.Length < 1)
             {
                 innerText += "<tr class='text-center'>";
                 innerText += "<td>You haven't enrrolled in any modules yet :[</td></tr>";

                 innerText = "<p>You haven't enrrolled in any modules yet :[ \"</p>";
             }
            innerText = Display(ModuleFunctionality.getModules(userId, userType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        private string Display(List<Module> modules)
        {
            string display = "";
            if (modules != null)
            {
                foreach (Module module in modules)
                {
                    display += "<tr class='text-center'>";
                    display += "<td> " + module.Module_Name + " </td><td>" + module.Module_Code + "</td><td>";
                    display += "<div class='form-group row justify-content-center'>";

                    if (Session["UserStatus"].ToString() == "Manager")
                    {
                        display += "<a href='AddRemoveModules.aspx?type=rem&mID=" + module.Id + "' class='btn btn-primary'>remove module</a>";
                    }
                    else
                    {
                        display += "<a href='AddRemoveModules.aspx?type=rem&mID=" + module.Id + "&uID=" + Session["ID"] + "' class='btn btn-primary'>remove module</a>";
                    }

                    display += "</div></td></tr>";

                }
            }
            else
            {                
            }
         

            return display;
        }

    }
}