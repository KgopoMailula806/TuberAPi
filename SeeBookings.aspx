<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="SeeBookings.aspx.cs" Inherits="QuadCore_Website.SeeBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/alvaro-serrano-hjwKMkehBco-unsplash.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <h1 class="mb-2 bread">Session Booking requests</h1>

                </div>
            </div>
        </div>
    </section>
    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    <table class="table">
                        <!--Table Headings-->
                        <thead class="thead-primary">
                            <tr class="text-center">
                                <th id="tutorOrClientHeading" runat="server">Tutor name</th>
                                <th>&nbsp</th>
                                <th>module name</th>
                                <th>&nbsp</th>
                                <th>date</th>
                                <th>&nbsp</th>
                                <th>status</th>
                                <th>&nbsp;</th>
                                <th>actions</th>
                            </tr>
                        </thead>
                        <!--Table Contents-->
                        <tbody id="RequestEntries" runat="server">
                            <tr>
                                <td><a href="#">Tutoring Rendered: Signal Processing</a></td>
                                <td>&nbsp</td>
                                <td>2020/03/01</td>
                                <td>&nbsp</td>
                                <td>Tutoring Rendered: Signal Processing</td>
                                <td>&nbsp</td>
                                <td>Invoice Summary</td>
                                <td>&nbsp</td>
                                <td>Tutoring Rendered: Signal Processing</td>
                                <td>&nbsp</td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Button ID="refreash" class="btn btn-primary" runat="server" Text="refresh" OnClick="Refreash_Click" />
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
