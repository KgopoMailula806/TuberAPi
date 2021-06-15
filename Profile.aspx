<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="QuadCore_Website.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    <div class="form-group row justify-content-center">
                        <h1>Profile</h1>
                    </div>
                    <div class="form-group row justify-content-center heading-section">
                        <h6><span class="icon-star"></span><span class="icon-star"></span><span class="icon-star"></span><span class="icon-star"></span><span class="icon-star-half-empty"></span></h6>
                    </div>

                    <div class="form-group row justify-content-center">

                        <!--Image Display-->
                        <div class="col-md-6 p-4 p-md-5 order-md-last" id="profileImage" runat="server">
                            <asp:Image ID="profilePic" runat="server" Width="400" Height="450" />
                        </div>

                        <div class="col-md-6 p-4 p-md-5 order-md-first" id="UserInformation" runat="server">

                            <div class="form-group" id="UserDetailsDiv" runat="server">
                                <!--Dynamic-->
                            </div>


                        </div>
                    </div>
                </div>


            </div>

            <div class="container">
                <div class="form-group row justify-content-center" style="margin-left: 200px;">
                        <div class="col-md-4 d-flex">
                            <div class="form-group" id="module_overview" runat="server">
                                <p><b>Modules</b></p>
                                <p>Signal Processing</p>
                                <p>Informatics 2</p>
                                <p>Computer Science 1</p>
                                <a href="UserModules.aspx">
                                    <p class="btn btn-primary">edit</p>
                                </a>
                            </div>
                        </div>

                        <div class="col-md-4 d-flex">
                            <div class="form-group">
                                <p><b>Invoices</b></p>
                                <!--Invoices-->
                                <div id="invoices" runat="server">
                                </div>
                                <!--Invoice Summary page-->
                                <a href="Invoices.aspx?type=summary" class="btn btn-primary">Invoice Summary</a>
                            </div>
                        </div>
                        

                        <div class="col-md-4 d-flex">

                            <div class="form-group">
                                <p><b>Action</b></p>
                                <a href="DeactivateAcc.aspx">

                                    <p class="btn btn-primary">Deactivate Account</p>
                                </a>
                            </div>
                        </div>
                    </div>
            </div>
        </div>

    </section>



</asp:Content>
