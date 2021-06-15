<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="QuadCore_Website.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">

                    <div id="alert" runat="server" visible="false">
                        <div class="alert alert-success" role="alert" id="">
                            <p>Your Details Were Successfully Edited.</p>
                        </div>
                        <div class="form-group row justify-content-center">
                            <a href="Profile.aspx" class="btn btn-primary">see profile in profile page</a>
                        </div>
                    </div>

                    <div id="body" runat="server">
                        <div class="form-group row justify-content-center">
                            <h1>Edit Your Details</h1>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Full Name(s)</label>
                            <input type="text" id="Full_Names" placeholder="" class="form-control" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Surname</label>
                            <input type="text" id="Surname" placeholder="" class="form-control" runat="server">
                        </div>


                        <div class="form-group">
                            <label class="font-weight-bold">Valid Phone Number</label>
                            <input type="text" id="Valid_Phone_Number" placeholder="" class="form-control" runat="server">
                        </div>

                         <div class="form-group">
                            <label class="font-weight-bold">Gender</label>
                            <asp:DropDownList ID="gender" runat="server" class="form-control">
                                <asp:ListItem Enabled="true" Text="Select Your Gender" Value="-1"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Female" Value="Female"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Other" Value="Other"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Grade / Year Level</label>
                            <asp:DropDownList ID="level" runat="server" class="form-control">
                                <asp:ListItem Enabled="true" Text="Select Grade / Year" Value="-1"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 10" Value="10"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 11" Value="11"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 12" Value="12"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="1st Year" Value="1st"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="2nd Year" Value="2nd"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="3rd Year" Value="3rd"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Educational Institution</label>
                            <input type="text" id="institution" class="form-control" runat="server">
                        </div>

                        <div class="form-group row justify-content-center">
                            <asp:Button ID="btnEdit" runat="server" Text="edit details" class="btn btn-primary py-3 px-5" OnClick="btnEdit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

</asp:Content>
