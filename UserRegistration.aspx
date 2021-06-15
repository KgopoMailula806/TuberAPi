<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="QuadCore_Website.UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="js/helper.js"></script>

    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/jj-ying-jD5RVR9BAS8-unsplash.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <div class="form-group row justify-content-center">
                        <h2 class="mb-2 bread" id="SignUph1Tag" runat="server">Sign Up</h2>
                    </div>
                    <div class="form-group row justify-content-center heading-section">
                        <h5 class="heading mb-2 bread">This is where it all begins. :)</h5>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!--User deatails input boxes -->
    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
            <div class="row d-flex align-items-stretch no-gutters">

                <div class="col-md-6 p-4 p-md-5 order-md-first" id="UserCrederntialTable1" runat="server" visible="false">

                    <div class="form-group">
                        <label class="font-weight-bold">Full Name(s)</label>
                        <input type="text" id="Full_Names" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Valid Phone Number</label>
                        <input type="text" id="Valid_Phone_Number" placeholder="" class="form-control" runat="server" oninput="checkSoudyNumber(this,'numMsg')">
                        <p id="numMsg" style="color:red;"></p>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold" id="EmailDivTage" runat="server">Email</label>
                        <input type="email" id="Email" onkeyup="validateEmail(this,'emailMsg')" class="form-control" runat="server">
                        <p id="emailMsg" class="text-primary"></p>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold" id="passwordDivTage" runat="server">Password</label>
                        <input type="password" id="Password" onkeyup="checkPasswordStrength(this,'passMsg')" class="form-control" runat="server">
                        <p id="passMsg" class="text-primary"></p>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Confirm Password</label>
                        <input type="password" id="confirm_password" onkeyup="confirmPasswordCheck(this,'confirmMsg')" class="form-control" runat="server">
                        <p id="confirmMsg" style="color: red;"></p>
                    </div>
                    <!--Message that is diplayed to user if the passwords dont match-->
                    <div class="form-group" id="passDiv" runat="server" visible="false">
                        <p style="color: darkorange;">Passwords do not match, try again!</p>
                    </div>
                </div>

                <div class="col-md-6 p-4 p-md-5 order-md-last">

                    <div id="UserCrederntialTable2" runat="server" visible="false">
                        <div class="form-group">
                            <label class="font-weight-bold">Surname</label>
                            <input type="text" id="Surname" placeholder="" class="form-control" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Gender</label>
                            <asp:DropDownList ID="GenderDropDown" runat="server" class="form-control">
                                <asp:ListItem Enabled="true" Text="Select Your Gender" Value="-1"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Female" Value="Female"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Other" Value="Other"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold" id="profileImageLableTag" runat="server">Profile Image</label>
                            <input type="file" id="profileImagefile" placeholder="" class="form-control" runat="server" onchange="ValidateImageExt(this);">
                            <br />
                            <p id="image_message" style="color: red;"></p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Age</label>
                            <input type="number" id="Age" placeholder="14" min="14" max="45" onchange="validateAge(this,'ageMsg')" class="form-control" runat="server">
                            <p id="ageMsg" class="text-primary"></p>
                        </div>
                    </div>

                    <!--Client part-->
                    <div class="form-group" runat="server" visible="false" id="ClientCredentialTable">
                        <div class="form-group">
                            <label class="font-weight-bold">Grade/Year of Study</label>
                            <%--<input type="text" id="ClientCurrentGrade" class="form-control" runat="server" visible="true" />--%>
                            <!--New Dropdown For Grades-->
                            <asp:DropDownList ID="level" runat="server" class="form-control">
                                <asp:ListItem Enabled="true" Text="Select Level" Value="-1"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 10" Value="g10"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 11" Value="g11"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="Grade 12" Value="g12"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="1st Year" Value="1st"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="2nd Year" Value="2nd"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="3rd Year" Value="3rd"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Educational Institution</label>
                            <input type="text" id="school" class="form-control" runat="server" visible="true" />
                        </div>
                    </div>

                    <!--Tutor part-->
                    <div class="form-group" runat="server" visible="false" id="TutorCredentialTable">
                        <div class="form-group">
                            <label class="font-weight-bold">Academic Transcript</label>
                            <input type="file" id="Academic_Transcript" runat="server" visible="true" onchange="ValidateFileExt(this,'file_message1');">
                            <br />
                            <p id="file_message1" style="color: red;"></p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Police Clearance</label>
                            <input type="file" id="PoliceClearance" runat="server" visible="true" onchange="ValidateFileExt(this,'file_message2');">
                            <br />
                            <p id="file_message2" style="color: red;"></p>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Curriculum Vitae (Not Mandetory)</label>
                            <input type="file" id="TutorCv" runat="server" visible="true" onchange="ValidateFileExt(this,'file_message3');">
                            <br />
                            <p id="file_message3" style="color: red;"></p>
                        </div>
                    </div>

                    <!--Manager part-->
                    <div class="form-group" runat="server" visible="false" id="ManagerCredentialTable">
                        <div class="form-group">
                            <label class="font-weight-bold">Curriculum Vitae</label>
                            <input type="file" id="CV" runat="server" visible="true" onchange="ValidateFileExt(this,'file_message');">
                            <br />
                            <p id="file_message" style="color: red;"></p>
                            
                        </div>
                    </div>

                </div>
            </div>

            <div class="form-group row justify-content-center">
                <asp:Button ID="btnRegister" runat="server" Text="Sign Up" class="btn btn-primary py-3 px-5" OnClick="Register_Click" />
            </div>
        </div>
        <p id="hiddenpassword" style="color: white;"></p>
    </section>

    

</asp:Content>
