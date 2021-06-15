<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="UserRegistrationTypeSelection.aspx.cs" Inherits="QuadCore_Website.UserRegistrationTypeSelection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/dayne-topkin-cB10K2ugb-4-unsplash-min.jpg');">
      <div class="overlay"></div>
      <div class="container">
        <div class="row no-gutters slider-text align-items-center justify-content-center">
          <div class="col-md-9 ftco-animate text-center">                
               <h1 class="mb-2 bread">sign up as a</h1>
               <h6 class="mb-2 bread">Bringing the tutor to you. :)</h6>                          
          </div>
        </div>
      </div>
    </section>
    <div class="form-group row justify-content-center">
             
          </div>
    <section class="ftco-section contact-section">
        <div class="container">
          <div class="form-group row justify-content-center">
             <div class="col-md-3 d-flex">
          	    <div class="bg-light align-self-stretch box p-4 text-center">
	            <p id="ASTutorPTag" runat="server"><a href="UserRegistration.aspx?As=Client" class="btn btn-primary px-4 py-3 mt-3">Student</a></p>
	          </div>
          </div>

          <div class="col-md-3 d-flex">
          	<div class="bg-light align-self-stretch box p-4 text-center">
	            <p id="ASClientPTag" runat="server"><a href="UserRegistration.aspx?As=Tutor" class="btn btn-primary px-4 py-3 mt-3">Tutor</a></p>
	          </div>
          </div>

        </div>
      </div>
    </section>

</asp:Content>
