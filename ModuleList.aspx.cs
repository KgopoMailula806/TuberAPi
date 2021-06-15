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
    public partial class ModuleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string display = "";

            
            string moduleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/GetModules");

            if(moduleBody != null)
            {
                List<Module> modules = JsonConvert.DeserializeObject<List<Module>>(moduleBody);

                foreach (Module module in modules)
                {
                    display += "<tr class='text-center'>";
                    display += "<td>" + module.Module_Name + "</td>";
                    display += "<td>" + module.Module_Code + "</td><td>";
                    display += "<div class='form-group row justify-content-center'>";
                    display += "<a href='AddRemoveModules.aspx?type=add&mID=" + module.Id + "&uID=" + Session["ID"] + "' class='btn btn-primary'>add module</a>";
                    display += "</div></td></tr>";
                }
            }

            modules.InnerHtml = display;
            
        }

    }
}