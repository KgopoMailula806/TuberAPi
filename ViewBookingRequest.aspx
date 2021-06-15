<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ViewBookingRequest.aspx.cs" Inherits="QuadCore_Website.ViewBookingRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="ftco-section">
			<div class="container-fluid px-4">
				<div class="row" id="sessionPanel" runat="server">
					
					<div class="col-md-3 course ftco-animate">																			 									
												
						<!--All Booking Request-->		
						<div class="text pt-4" id="Notifications" runat="server" visible="true">
								<h6>Fiil with anchors to view details about the request</h6>					
						</div>
					<div id="alert" runat="server"></div>	

					</div>				    
				</div>
			</div>
		</section>
</asp:Content>
