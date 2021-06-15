<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ViewSingleBooking.aspx.cs" Inherits="QuadCore_Website.ViewSingleBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">            
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    
                    <div class="form-group row justify-content-center">
                        <h1>Session Information</h1>
                    </div>

                    <div>

                        <div class="form-group">
                            <label class="font-weight-bold">Name:</label>
                            <p style="display: inline;" id="requestName" runat="server">Jacob Muzonde</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Email:</label>
                            <p style="display: inline;" id="requestNumber" runat="server">073 574 9022</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Session Location:</label>
                            <p style="display: inline;" id="requestLocation" runat="server">Johanesburg, Gauteng</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Module Name:</label>
                            <p style="display: inline;" id="requestModule" runat="server">Linear Algebra</p>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold">Session Date:</label>
                            <p style="display: inline;" id="requestDate" runat="server">2020/03/01</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Session Start Time:</label>
                            <p style="display: inline;" id="requestStartTime" runat="server">05:15</p>
                        </div>

                        <div class="form-group row justify-content-center">
                            <asp:Button ID="CancelBooking" class="btn btn-primary" Style="background-color: grey;" runat="server" Text="Cancel Booking" OnClick="CancelBookingBookingWith_Click" />                            
                        </div>
                        <div class="form-group row justify-content-center">
                            <asp:Button ID="redirectBack" runat="server" Text="Back" class="btn btn-primary" OnClick="btn_back_Click" />
                         </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
