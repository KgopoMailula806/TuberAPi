<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="TutorReport.aspx.cs" Inherits="QuadCore_Website.TutorReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    <div class="form-group row justify-content-center">
                        <h1>Tutor Summary</h1>
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


            
            </div>
        </section>

</asp:Content>
