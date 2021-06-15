<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="DeactivateAcc.aspx.cs" Inherits="QuadCore_Website.DeactivateAcc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <!-- Modal -->
            <div class="form-group row justify-content-center" id="alert" runat="server" visible="false">
                <div class="col-md-6 center">
                    <div class="alert alert-success" role="alert">
                        <p>email with account deactivation link was sent to your email address.</p>
                    </div>

                </div>
            </div>

            <div class="form-group row justify-content-center" id="body" runat="server">
                <div class="col-md-6 center">

                    <div class="form-group row justify-content-center">
                        <h1>Deactivate Account</h1>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Email</label>
                        <input type="email" id="email" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-center">Password</label>
                        <input type="password" id="Password" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-center">Reason You Leaving Us (Optional)</label>
                        <textarea id="Reason" class="form-control" cols="20" rows="2" runat="server"></textarea>
                    </div>

                    <asp:Button ID="btnDeactivate" class="btn btn-primary" runat="server" Text="Deactivate Account" OnClick="btnDeactivate_Click" />

                </div>
            </div>
        </div>
    </section>

</asp:Content>
