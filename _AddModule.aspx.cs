using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class _AddModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnModuleAdd_Click(object sender, EventArgs e)
        {
            string moduleName = name.Value;
            string moduleCode = code.Value;
            string moduleType = type.Value;

            int flag = ModuleFunctionality.addManagerModule(moduleName, moduleType, moduleCode);

            if (flag > 0)
            {
                msg.Visible = true;
                msg.InnerHtml = "<br/>" + name.Value + " is added to module list!";
                name.Value = "";
                code.Value = "";
                type.Value = "";
            }
                

        }
    }
}