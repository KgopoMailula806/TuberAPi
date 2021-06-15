<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="EditModule.aspx.cs" Inherits="QuadCore_Website.EditModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" runat="server">
            <h1 style="display:block;">Edit Module Details</h1>

            <form  runat="server">

                <div visible="true" runat="server" id="searchBlock">

                    <div class="form-group" style="display:block;">
                  <label class="font-weight-bold">Module Name</label>
                  <input type="text" id="oldName" placeholder="" class="form-control" runat="server" style="width:500px;">
                  <input type="text" id="id" placeholder="" visible="false" runat="server">
                </div>

                <div class="form-group" style="display:block; width:500px;">
                     <asp:Button ID="btnSearch" runat="server" Text="Fetch Module" style="display:block;" class="btn btn-primary" OnClick="btnSearch_Click"/>
                    <p style="color:red; display:block;" visible="false" runat="server"><br />Module Not Found!</p>
                </div>
              </div>

                <div visible="false" runat="server" id="editBlock">

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
                 <asp:Button ID="btnEdit" runat="server" Text="Edit Module" style="display:block;" class="btn btn-primary" OnClick="btnEdit_Click"/>
                 <p style="color:green; display:block;" visible="false" runat="server" id="msg"><br />Module Edited!</p>
             </div>
                </div>

            

            </form>
    </div>

</asp:Content>
