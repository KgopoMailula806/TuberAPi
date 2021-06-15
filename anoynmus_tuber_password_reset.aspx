<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="anoynmus_tuber_password_reset.aspx.cs" Inherits="QuadCore_Website.anoynmus_tuber_password_reset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">

                    <div id="alert" runat="server" visible="false">
                        <div class="alert alert-success" role="alert" id="">
                            <p>Your Was Successfully Changed, Use Your New Password By Signing In.</p>
                        </div>
                        <div class="form-group row justify-content-center">
                            <a href="Login.aspx" class="btn btn-primary">move to login page</a>
                        </div>
                    </div>

                    <div id="body" runat="server">
                        <div class="form-group row justify-content-center">
                            <h1>Reset Your Password</h1>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Email</label>
                            <input type="email" id="email" placeholder="" class="form-control" runat="server" readonly>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">New Password</label>
                            <input type="password" id="new_password" placeholder="" class="form-control" runat="server">
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold">Confirm New Password</label>
                            <input type="password" id="confirm_new_password" placeholder="" class="form-control" runat="server">
                        </div>
                        <div class="form-group row justify-content-center">
                            <asp:Button ID="btnReset" runat="server" Text="reset password" class="btn btn-primary py-3 px-5" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

</asp:Content>
