<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ShowNotificatioinDetails.aspx.cs" Inherits="QuadCore_Website.ShowNotificatioinDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="text pt-4" id="NoDetailsMessage" runat="server" visible="false">
                <p>No details About the notification</p>
            </div>          
            <div class="row d-flex align-items-stretch no-gutters">

                <!--Details column-->
                <div class="col-md-6 p-4 p-md-5 order-md-first" runat="server">

                    <!-- Booking Request-->
                    <div id="BookingRequestDiv" runat="server" visible="false">
                        <h1 class="mb-2 bread">Request Details</h1>

                        <div class="form-group">
                            <label class="font-weight-bold">Student Name:</label>
                            <p style="display: inline;" id="requestName" runat="server">Jacob Muzonde</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Contact Number:</label>
                            <p style="display: inline;" id="requestNumber" runat="server">073 574 9022</p>
                        </div>

                        <div class="form-group" id="requestLocationDIV" runat="server">
                            <label class="font-weight-bold">Session Location:</label>
                            <a> href="#" style="display: inline;" id="requestLocation" runat="server">Johanesburg, Gauteng</a>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Module Name:</label>
                            <p style="display: inline;" id="requestModule" runat="server">Linear Algebra</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Number of Periods:</label>
                            <p style="display: inline;" id="requestNPeriods" runat="server">5</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Date:</label>
                            <p style="display: inline;" id="requestDate" runat="server">2020/03/01</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Start Time:</label>
                            <p style="display: inline;" id="requestStartTime" runat="server">05:15</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request End Time:</label>
                            <p style="display: inline;" id="requestEndTime" runat="server">10:15</p>
                        </div>

                        <asp:Button ID="Accept_BookingBtn" class="btn btn-primary" runat="server" Text="Accept" OnClick="AcceptBookingBtn_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="RejectRequest" class="btn btn-primary" runat="server" Text="Reject" OnClick="RejectClientBookingRequest_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="Negotiate_BookingWithClient" class="btn btn-primary" runat="server" Text="Negotiate" OnClick="Negotiate_BookingWithClient_Click" />
                        

                        <div id="alert" runat="server"></div>
                    </div>

                    <!--Session-->
                    <div id="Session" runat="server" visible="false">
                        <h1>Booking Finalisation Details</h1>

                        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Accept Booking" OnClick="AcceptBookingBtn_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Negotiate Session Details" OnClick="Negotiate_BookingWithClient_Click" />

                    </div>

                    <!--Booking Finalisation Details-->
                    <div id="BookingFinalisationDetails" runat="server" visible="false">
                        <h1>Booking Details</h1>

                        <h5 id="finalizationDescription" runat="server">Description</h5>

                        <div class="form-group">
                            <label id="userFinalizationNameTitle" runat="server" class="font-weight-bold">Student Name:</label>
                            <p style="display: inline;" id="bookingFinalName" runat="server">Jacob Muzonde</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Module Name:</label>
                            <p style="display: inline;" id="bookingFinalModule" runat="server">073 574 9022</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Session Date:</label>
                            <p style="display: inline;" id="bookingFinalDate" runat="server">Johanesburg, Gauteng</p>
                        </div>

                        <a id="gotoBookings" class="btn btn-primary" href="ViewBookedSessions.aspx">See boookings</a>

                    </div>

                    <!--BookingCancelation-->
                    <div id="BookingCancelation" runat="server" visible="false">
                        <h1>Booking Cancelation</h1>

                        <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="Accept Booking" OnClick="AcceptBookingBtn_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="Negotiate Session Details" OnClick="Negotiate_BookingWithClient_Click" />

                    </div>

                    <!--Renegotiation View-->
                    <div id="renegotionView" runat="server" visible="false">
                        <h1 class="mb-2 bread">Request Details</h1>

                        <div class="form-group">
                            <label class="font-weight-bold">Student Proposed Location:</label>
                            <p style="display: inline;" id="rvLocation" runat="server">Johanesburg, Gauteng</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Tutor Proposed Location:</label>
                            <p style="display: inline;" id="rvTLocation" runat="server">Johanesburg, Gauteng</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Module Name:</label>
                            <p style="display: inline;" id="rvModule" runat="server">Linear Algebra</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Number of Periods:</label>
                            <p style="display: inline;" id="rvPeriods" runat="server">5</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Date:</label>
                            <p style="display: inline;" id="rvDate" runat="server">2020/03/01</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Start Time:</label>
                            <p style="display: inline;" id="rvSTime" runat="server">05:15</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request End Time:</label>
                            <p style="display: inline;" id="rvETime" runat="server">10:15</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Reason:</label>
                            <p id="rvReason" runat="server">10:15</p>
                        </div>

                        <p>How Would you like to respond</p>

                        <asp:Button ID="acceptAltDate" class="btn btn-primary" runat="server" Text="Accept" OnClick="AcceptBookingRequestNegotiationBtn_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="RejectAndCancelRequest" class="btn btn-primary" runat="server" Text="No" OnClick="NoToBookingRequestNogotiationBtn_Click" />&nbsp&nbsp&nbsp
                        <asp:Button ID="requestAlternativeDate" class="btn btn-primary" runat="server" Text="Send alternative date" OnClick="Negotiate_BookingTimeResponse_Click" />

                    </div>

                    <!--Booking Rejection-->
                    <div id="BookingRejection" runat="server" visible="false">
                        <h1>Booking Request Rejected</h1>
                        <h3>Booking request with the following details was rejected</h3>

                        <div class="form-group">
                            <label class="font-weight-bold">Session Location:</label>
                            <p style="display: inline;" id="rejectionLocation" runat="server">Johanesburg, Gauteng</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Module Name:</label>
                            <p style="display: inline;" id="rejectionModule" runat="server">Linear Algebra</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Number of Periods:</label>
                            <p style="display: inline;" id="rejectionPeriods" runat="server">5</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Date:</label>
                            <p style="display: inline;" id="rejectionDate" runat="server">2020/03/01</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request Start Time:</label>
                            <p style="display: inline;" id="rejectionStart" runat="server">05:15</p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Request End Time:</label>
                            <p style="display: inline;" id="rejectionEnd" runat="server">10:15</p>
                        </div>

                    </div>
                </div>

                <div class="col-md-6 p-4 p-md-5 order-md-last" runat="server">

                    <!--Renegotiation controlls-->
                    <div id="frstRenegotiationDiv" runat="server" visible="false">

                        <h1 class="mb-2 bread">Negotiation Conditions</h1>

                        

                        <div class="form-group" visible="false">
                            <label class="font-weight-bold" style="display: block;" runat="server" visible="false">Location</label>
                            <input type="text" id="address" placeholder="Locations" runat="server" readonly style="display: inline;" visible="false">&nbsp&nbsp&nbsp
                                <asp:Button ID="button3" runat="server" Text="Change Location" class="btn btn-primary" visible="false" />
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold" style="display: block;">Number of Periods</label>
                            <input type="number" placeholder="5" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold" style="display: block;">Alternative Time Proposal</label>
                            <input type="time" id="time" placeholder="" class="form-control" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold" style="display: block;">Alternative Date Proposal</label>
                            <input type="date" id="date" placeholder="" class="form-control" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Reason</label>
                            <input type="text" id="reason" placeholder="Specify Your Reason Here" class="form-control" runat="server">
                        </div>

                        <asp:Button ID="SubmitRenegotionation" class="btn btn-primary" runat="server" Text="Submit" OnClick="SubmitNegotiate_BookingWithClient_Click" />

                    </div>

                    <!--Renegotiation-->
                    <div class="text pt-4" id="RenegotiationDIV" runat="server" visible="false">

                        <h1 id="NegotiationDescription" runat="server">Booking Negotiation</h1>

                        <!--Renegotiation controlls-->
                        <div id="RenegotiationControlls" runat="server" visible="false">

                            <div class="form-group">
                                <label class="font-weight-bold">Alternative Date Proposal</label>
                                <input type="date" id="date1" placeholder="" class="form-control" runat="server">
                            </div>

                            <div class="form-group">
                                <label class="font-weight-bold">Alternative Time Proposal</label>
                                <input type="time" id="time1" placeholder="" class="form-control" runat="server">
                            </div>

                            <div class="form-group">
                                <label class="font-weight-bold" style="display: block;">Number of Periods</label>
                                <input type="number" placeholder="5" runat="server" id="_periods">
                            </div>

                            <div class="form-group">
                            <label class="font-weight-bold">Reason</label>
                            <input type="text" id="rrVReason" placeholder="Specify Your Reason Here" class="form-control" runat="server">
                        </div>

                            <asp:Button ID="submitAltDateResponse" class="btn btn-primary" runat="server" Text="Submit" OnClick="SubmitAltNegotiate_BookingWithClient_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </section>

</asp:Content>
