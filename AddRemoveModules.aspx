<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="AddRemoveModules.aspx.cs" Inherits="QuadCore_Website.AddRemoveModules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">

                <div class="form-group row justify-content-center">
                    <h1 id="SignUph1Tag" runat="server">Sign Up</h1>
                </div>
                <div class="form-group row justify-content-center heading-section">
                    <h6 class="heading">This is where it all begins. :)</h6>
                </div>
                
				<div class="row d-flex align-items-stretch no-gutters">
					
                    <div class="col-md-6 p-4 p-md-5 order-md-first" id="UserCrederntialTable1" runat="server" visible="false">

                        <div class="form-group">
                            <label class="font-weight-bold">Full Name(s)</label>
                            <input type="text" id="Full_Names" placeholder="" class="form-control" runat="server">
                         </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Valid Phone Number</label>
                        <input type="text" id="Valid_Phone_Number" placeholder="" class="form-control" runat="server">
                    </div>

                     <div class="form-group">
                        <label class="font-weight-bold" id="EmailDivTage" runat="server">Email</label>
                        <input type="email" id="Email" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold" id="passwordDivTage" runat="server">Password</label>
                        <input type="password" id="Password" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Confirm Password</label>
                        <input type="password" id="confirm_password" placeholder="" class="form-control" runat="server">
                    </div>
                      <!--Message that is diplayed to user if the passwords dont match-->
                <div class="form-group" id="passDiv" runat="server" visible="false">
                    <p style="color:darkorange;" >Passwords do not match, try again!</p>
                </div>
                </div>
                    
                    <!---->
                <div class="col-md-6 p-4 p-md-5 order-md-last" id="removeModDiv" runat="server">

                    <div id="UserCrederntialTable2" runat="server" visible="false">
                        <div class="form-group">
                              <label class="font-weight-bold">Surname</label>
                              <input type="text" id="Surname" placeholder="" class="form-control" runat="server">
                         </div>

                         <div class="form-group">
                            <label class="font-weight-bold">Gender</label>
                            <input type="text" id="Gender" placeholder="" class="form-control" runat="server">
                        </div>
                        
                        <div class="form-group">
                            <label class="font-weight-bold" id="profileImageLableTag" runat="server"> Prfile Image</label>
                            <input type="file" id="profileImagefile" placeholder="" class="form-control" runat="server">
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Age</label>
                            <input type="number" id="Age" placeholder="" class="form-control" runat="server">
                        </div>
                    </div>

                    <!--Client part-->
                    <div class="form-group" runat="server" visible="false" id="ClientCredentialTable">
                        <div class="form-group">
                            <label class="font-weight-bold">Grade/Year of Study</label>
                            <input type="text" id="ClientCurrentGrade" class="form-control" runat="server" visible="true"/>
                        </div>
                    </div>

                    <!--Tutor part-->
                    <div class="form-group" runat="server" visible="false" id="TutorCredentialTable">
                        <div class="form-group">
                            <label class="font-weight-bold">Academic Transcript</label>
                            <input type="file" id="Academic_Transcript" runat="server" visible="true"/>
                         </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Police Clearance</label>
                            <input type="file" id="PoliceClearance" runat="server" visible="true"/>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold">Curriculum Vitae (Not Mandetory)</label>
                            <input type="file" id="TutorCv" runat="server" visible="true"/>
                        </div>
                     </div>
                
                    <!--Manager part-->
                    <div class="form-group" runat="server" visible="false" id="ManagerCredentialTable">
                        <div class="form-group">
                              <label class="font-weight-bold">Curriculum Vitae</label>
                              <input type="file" id="CV" runat="server" visible="true"/>
                        </div>
                  </div> 

              </div>                    
            </div>

           
   </section>
</asp:Content>
