<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="FileTesting.aspx.cs" Inherits="QuadCore_Website.FileTesting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">

                    <div class="form-group row justify-content-center">
                        <h1>Files</h1>
                    </div>
                       <asp:DropDownList ID="Modules" runat="server">
                            <asp:ListItem Enabled="true" Text="Select Module" Value="-1"></asp:ListItem>
    
                    </asp:DropDownList>
                        <p id="selected_item" runat="server"></p>
                          <div class="form-group row justify-content-center" runat="server">
                              <asp:FileUpload ID="userFile" runat="server" CssClass ="btn btn-primary" onchange="ValidateSingleInput(this);"/>
                              <br /><p id="file_message">Blurry Thoughts</p>
                              <asp:Button ID="Upload" runat="server" Text="Upload"  class ="btn btn-primary" OnClick="Upload_Click"/>
                              </div>
                        <div class="form-group row justify-content-center" id="imageFrame" runat="server">
                            <asp:Image ID="imageDisplay" runat="server" />
                        </div>
                        
                    </div>
            </div>   
          </div>
	</section>

</asp:Content>
