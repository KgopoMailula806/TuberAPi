<%@ Page Title="Error 404: Page Not Found" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="QuadCore_Website.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">
                <div class="form-group row justify-content-center">
                    <img src="images/error_page.jpg"/>
                </div>
                <div class="form-group row justify-content-center">
                    
                    <div class="col-md-3 d-flex">
                            <p class="btn btn-primary"><a href="Home.aspx"></a>Go Back Home</p>
                        </div>
                        <div class="col-md-3 d-flex">
                            <p class="btn btn-primary"><a href="#"></a>Report This To Tuber</p>
                        </div>
                 </div>
            </div>
	</section>
    

</asp:Content>
