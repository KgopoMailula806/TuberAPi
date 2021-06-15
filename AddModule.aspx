<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="AddModule.aspx.cs" Inherits="QuadCore_Website.AddModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">\

    <section>
      <div class="slider-item">
      	<div class="overlay"></div>
            <div class="container">
                <div class="row no-gutters slider-text align-items-center justify-content-start" data-scrollax-parent="true">
                    <div class="col-md-6 p-4 p-md-5 order-md-last">
                         <h1 class="mb-4">Add a Module</h1>
                    <div class="form-group">                    
               </div>

                   
                   <div class="form-group">
                       <label class="font-weight-bold">Module</label>                     
                       <input type="text" id="NewModule" placeholder="Enter Meeting Location Here" class="form-control" runat="server">
			       </div>

                   <div class="form-group">
                       <label class="font-weight-bold">Module Code</label>
                       <input type="text" id="ModuleCode" placeholder="Enter Meeting Location Here" class="form-control" runat="server">
			       </div>                   

                   <div class="form-group row justify-content-center">     
                   <asp:Button ID="btn_submit" runat="server" Text="Submit Request" class="btn btn-primary py-3 px-5" OnClick="btn_submit_Click" />

			       </div>
                   </div>

			       </div>

                    </div> 
                </div>
                
                
        
      </section>

</asp:Content>
