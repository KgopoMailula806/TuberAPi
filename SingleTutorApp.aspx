<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="SingleTutorApp.aspx.cs" Inherits="QuadCore_Website.SingleTutorApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/helper.js"></script>
    <link rel="stylesheet" href="css/popup.css"/>

     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Applicant Details</h1>
                        </div>

                     <div class="form-group row justify-content-center">
                        <table class="table">
                                <thead class="thead-primary">
                                    <tr class="text-center">
                                        <th>Assessment</th>
                                        <th>Applicant Details</th>
                                        <th>Applicant Image</th>
                                        <th>Documents</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <tr class="text-center">
                                        <td>
                                          
                                            <p>Is police clearance valid?</p>
                                                <asp:RadioButtonList id="PC" runat="server" RepeatDirection="horizontal" CssClass="spaced">
                                                    <asp:ListItem>&nbsp &nbsp Yes &nbsp &nbsp &nbsp &nbsp&nbsp &nbsp &nbsp &nbsp&nbsp &nbsp &nbsp &nbsp </asp:ListItem>
                                                    <asp:ListItem>&nbsp &nbsp No  </asp:ListItem>
                                                </asp:RadioButtonList>

                                            <p>&nbsp</p>
                                            <p>Is CV impressive?</p>
                                                <asp:RadioButtonList id="CV" runat="server" RepeatDirection="horizontal" CssClass="spaced">
                                                    <asp:ListItem>&nbsp &nbsp Yes  &nbsp &nbsp &nbsp &nbsp &nbsp</asp:ListItem>
                                                    <asp:ListItem>&nbsp It's Okay</asp:ListItem>
                                                    <asp:ListItem>&nbsp &nbsp No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            <p>&nbsp</p>
                                            <p>Academic Record Average:</p>
                                                <asp:RadioButtonList id="AVR" runat="server" RepeatDirection="horizontal" CssClass="spaced">
                                                    <asp:ListItem>40%-59% </asp:ListItem>
                                                    <asp:ListItem>60%-79% </asp:ListItem>
                                                    <asp:ListItem>80%-99% </asp:ListItem>
                                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <div class="col-md-6 col-lg-4">
                                                <div class="form-group" id="TutorInfor" runat="server">
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                             <asp:Image ID="TutorImage" runat="server" width="400" height="450" />
                                        </td>
                                        <td id="documents" runat="server">
                                             
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                         <div class="form-group row justify-content-center">

                             <input type="button" value="Set Up Meeting" id="meeting" class="btn btn-primary" onclick="openForm()" />
                             &nbsp
                             &nbsp
                             &nbsp
                             &nbsp
                             <input type="button" value="Reject" id="rejection" class="btn btn-primary"/>
                         </div> 
                      </div>   
                    </div> </div>
               
            
	</section>

</asp:Content>
