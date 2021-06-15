<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="SystemTerminal.aspx.cs" Inherits="QuadCore_Website.SystemTerminal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/helper.js"></script>
     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">

                <div class="form-group row justify-content-center">
                    <h1>Administration Terminal</h1>
                </div>            
                
				<div class="row d-flex align-items-stretch no-gutters">
                    <aside class="sidebar" style="float:left; padding:0 20px">
                        <section><p><a href="#" onclick="changeSrc('TutorApplications.aspx')">View Applications</a></p></section>
                        <section><p><a href="#">HR</a></p></section>
                        <section><p><a href="#">Data View</a></p></section>
                        <section><p><a href="#">Booking Management</a></p></section>
                    </aside>

                   <iframe src="ex.aspx" id="portal" style="float:right;" width="898" height="600"></iframe>
                                                                         

            </div>    
         </div>            
	</section>
</asp:Content>
