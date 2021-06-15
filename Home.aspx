<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QuadCore_Website.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="home-slider owl-carousel">
      <div class="slider-item" style="background-image:url(images/nikhita-s-NsPDiPFTp4c-unsplash-min.jpg);">
      	<div class="overlay"></div>
        <div class="container">
          <div class="row no-gutters slider-text align-items-center justify-content-start" data-scrollax-parent="true">
          <div class="col-md-6 ftco-animate">
            <h1 class="mb-4" id="statement" runat="server">Tuber, Bringing The Tutor To You</h1>
            <p>A small river named Duden flows by their place and supplies it with the necessary regelialia.</p>
            <p id="signInBtn" runat="server"><a href="UserRegistrationTypeSelection.aspx" class="btn btn-primary px-4 py-3 mt-3">Sign Up</a></p>
          </div>
        </div>
        </div>
      </div>
    </section>
</asp:Content>
