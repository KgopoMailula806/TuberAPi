<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuadCore_Website.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/chris-panas-0Yiy0XajJHQ-unsplash-min.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <h1 class="mb-2 bread">Login</h1>
                    <div class="form-group row justify-content-center heading-section">
                        <h6 class="mb-2 bread">Don't have an account? Sign Up <a href="UserRegistrationTypeSelection.aspx">Here</a></h6>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">



                    <div class="form-group">
                        <label class="font-weight-bold">Email</label>
                        <input type="email" id="email" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-center">Password</label>
                        <input type="password" id="Password" placeholder="" class="form-control" runat="server">
                        <h6><a href="ResetPassword.aspx">Forgot Password?</a></h6>
                    </div>

                    <div class="form-group row justify-content-center">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary py-3 px-5" OnClick="btnLogin_Click" />
                    </div>

                    <div class="row form-group" visible="false" runat="server" id="ErrorMsgBox">
                        <div class="col-md-12">
                            <label class="font-weight-bold" for="message">Message</label>
                            <p style="color: red;">Invalid Password or Email, Try Again!</p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row d-flex align-items-stretch no-gutters">
            </div>
        </div>
    </section>

</asp:Content>
