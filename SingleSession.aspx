<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="SingleSession.aspx.cs" Inherits="QuadCore_Website.SingleSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/bg_1.jpg');">
      <div class="overlay"></div>
      <div class="container">
        <div class="row no-gutters slider-text align-items-center justify-content-center">
          <div class="col-md-9 ftco-animate text-center">
            <h1 class="mb-2 bread">Booked Sessions</h1>
            <p class="breadcrumbs">
                <span class="mr-2"><a href="Home.aspx">Home <i class="ion-ios-arrow-forward"></i></a></span>
                <span><a href="ViewBookedSessions.aspx">Booked Sessions <i class="ion-ios-arrow-forward"></i></a></span>
                <span>Session <i class="ion-ios-arrow-forward"></i></span>                
            </p>
          </div>
        </div>
      </div>
    </section>

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">
				<div class="row d-flex align-items-stretch no-gutters">
					<div class="col-md-6 p-4 p-md-5 order-md-last bg-light">
                        <div class="form-group">
                                <p><b>Tutor Name: </b>Skyler</p>
                                <p><b>Module: </b> Calculas</p>
                                <p><b>Location: </b> Congo</p>
                                <p><b>Date: </b> 1999/03/01</p>
                                <p><b>Time:</b> 16:20</p>
                                <label class="font-weight-bold">Session Description/Help Wanted In:</label>
                                <p>Separated they live in. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country</p>
                            </div>
					</div>
					<div class="col-md-6 d-flex align-items-stretch">
						<img src="images/teacher-1.jpg" width="400" height="450" />
					</div>
				</div>
			</div>
		</section>

</asp:Content>
