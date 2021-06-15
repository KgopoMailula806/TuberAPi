<%@ Page Title="Reset Password" Async="true" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="QuadCore_Website.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">

                    <div class="form-group row justify-content-center">
                        <h1>Reset Your Password</h1>
                    </div>

                    <div class="form-group row justify-content-center heading-section" id="paragraph" runat="server" visible="false">
                        <h6>Enter your email address, linked to your account to reset your password.</h6>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Email</label>
                        <input type="email" id="email" placeholder="" class="form-control" runat="server">
                    </div>


                    <div class="form-group row justify-content-center">
                        <asp:Button ID="btnEmail" runat="server" Text="send me the email" class="btn btn-primary py-3 px-5" OnClick="btnEmail_Click" />
                    </div>

                    <div class="row form-group" visible="false" runat="server" id="ErrorMsgBox">
                        <div class="col-md-12">
                            <label class="font-weight-bold" for="message">Message</label>
                            <p style="color: red;">Invalid Email, Try Again!</p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row d-flex align-items-stretch no-gutters">
            </div>
        </div>
    </section>

</asp:Content>
