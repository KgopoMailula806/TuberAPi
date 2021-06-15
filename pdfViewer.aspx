<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="pdfViewer.aspx.cs" Inherits="QuadCore_Website.pdfViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">

                    <div class="form-group row justify-content-center" id="Heading" runat="server">
                       <h4 id="head" runat="server" style="display:block;"></h4>
                    </div>
                    <div class="form-group row justify-content-center" id="Div1" runat="server">
                         <h5 id="name" runat="server" style="display:block;"></h5>
                    </div>
                       
                        <div class="form-group row justify-content-center" id="pdfFrame" runat="server">
                        </div>
                        
                    </div>
            </div>   
          </div>
	</section>

</asp:Content>
