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
    public partial class EditModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["AcceptClient"] != null)
                {

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string moduleName = oldName.Value;
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Modules/GetModuleByName/" + moduleName + "/");

            if(response != null)
            {
                Module mod = JsonConvert.DeserializeObject<Module>(response);
                id.Value = mod.Id.ToString();
                name.Value = mod.Module_Name;
                code.Value = mod.Module_Code;
                type.Value = mod.Module_Type;

                searchBlock.Visible = false;
                editBlock.Visible = true;

            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string moduleID = id.Value;
            string moduleName = name.Value;
            string moduleCode = code.Value;
            string moduleType = type.Value;

            int flag = ModuleFunctionality.editModule(moduleID, moduleName, moduleType, moduleCode);

            if (flag > 0)
            {
                msg.Visible = true;
                name.Value = "";
                code.Value = "";
                type.Value = "";
            }
        }
    }
}