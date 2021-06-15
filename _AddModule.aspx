<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="_AddModule.aspx.cs" Inherits="QuadCore_Website._AddModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" runat="server">
            <h1 style="display:block;">Add a new module</h1>

            <form  runat="server">
                <div class="form-group" style="display:block;">
                  <label class="font-weight-bold">Module Name</label>
                  <input type="text" id="name" placeholder="" class="form-control" runat="server" style="width:500px;">
             </div>

            <div class="form-group" style="display:block;">
               <label class="font-weight-bold">Module Code</label>
               <input type="text" id="code" placeholder="" class="form-control" runat="server" style="width:500px;">
            </div>

             <div class="form-group" style="display:block;">
                  <label class="font-weight-bold">Module Type</label>
                  <input type="text" id="type" placeholder="" class="form-control" runat="server" style="width:500px;">
             </div>
                
             <div class="form-group" style="display:block; width:500px;">
                 <asp:Button ID="btnModuleAdd" runat="server" Text="add module" style="display:block;" class="btn btn-primary py-3 px-5" OnClick="btnModuleAdd_Click"/>

                 <p style="color:green; display:block;" visible="false" runat="server" id="msg"><br /><br />Module Added!</p>
             </div>

            </form>
    </div>

</asp:Content>
