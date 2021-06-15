<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="RequestTutor.aspx.cs" Inherits="QuadCore_Website.RequestTutor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/matt-ragland-02z1I7gv4ao-unsplash-min.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <h1 class="mb-2 bread">Request Tutor</h1>
                    <h6 class="mb-2 bread">Bringing the tutor to you. :)</h6>
                    <p class="breadcrumbs"><span class="mr-2"><a href="Home.aspx">Home <i class="ion-ios-arrow-forward"></i></a></span><span>Request Tutor <i class="ion-ios-arrow-forward"></i></span></p>
                </div>
            </div>
        </div>
    </section>

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="row d-flex align-items-stretch no-gutters">

                <!--First/left column-->
                <div class="col-md-6 p-4 p-md-5 order-md-first" runat="server">

                    <div class="form-group">
                        <div class="form-group">
                            <asp:Button ID="locationaSelect" runat="server" Text="Choose Session Location" class="btn btn-primary" OnClick="locationaSelect_Click" />
                        </div>
                        <div id="loc_box" runat="server" visible="false">
                            <label class="font-weight-bold">Location</label>
                        <input type="text" id="address" placeholder="Enter Meeting Location Here" class="form-control" runat="server" readonly>
                        </div>
                        
                    </div>

                    <div class="form-group">

                        <!--Module selection dropdown-->
                        <label class="font-weight-bold">Module</label>
                        <select visible="false" name="" id="ModuleDropdow" class="form-control" placeholder="Select Module" runat="server" onclick="btn_GetReleventTutors_Click">
                        </select>

                        <asp:DropDownList ID="moduleList" runat="server" class="form-control">
                            <asp:ListItem Enabled="true" Text="Select Module" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">

                        <label class="font-weight-bold">Date</label>
                        <input type="date" id="date" placeholder="" class="form-control" runat="server">
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Time</label>
                        <input type="time" id="time" placeholder="" class="form-control" runat="server">
                    </div>

                </div>
                <!--Next column-->
                <div class="col-md-6 p-4 p-md-5 order-md-last" runat="server">

                    <div class="form-group row justify-content-center">
                        <asp:Button ID="GetModuleTutors" runat="server" Text="Check Tutors" class="btn btn-primary" OnClick="btn_GetReleventTutors_Click" /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Button ID="PreviousTutors" runat="server" Text="Interested in Previous Tutors?" class="btn btn-primary" OnClick="btn_previousTutors_Click" />
                    </div>

                    <div class="form-group" id="tutorsFormGroup" runat="server" visible="false">
                        <!--DropDown with Tutor names-->
                        <label id="tutorLableTag" runat="server" class="font-weight-bold">Tutors</label>
                        <!--Tutor list-->
                        <asp:DropDownList ID="TutorList" runat="server" class="form-control">
                            <asp:ListItem Enabled="true" Text="Select Tutor" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!--The Choosen one-->
                    <div class="form-group">
                        <input type="text" id="date1" placeholder="" class="form-control" runat="server" visible="false" readonly>
                    </div>

                    <div class="form-group">
                        <div class="form-group row justify-content-center">
                            
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold">Session Periods</label>
                        <p style="color: darkorange">(every period is worth 1-hour)</p>
                        <input type="number" id="sessionPeriods" placeholder="" class="form-control" runat="server">
                    </div>

                    <!--Submit Button-->
                    <div class="form-group row justify-content-center">
                        <asp:Button ID="btn_submit" runat="server" Text="Submit Request" class="btn btn-primary py-3 px-5" OnClick="btn_submit_Click" />
                    </div>

                </div>
                <script>
                    
                    //    asyncCall();
                    validateFields();

                    function resolveAfter2Seconds() {
                        return new Promise(resolve => {
                            setTimeout(() => {
                                resolve('resolved');
                            }, 2000);
                        });
                    }

                    async function asyncCall() {
                        console.log('calling');
                        const result = await resolveAfter2Seconds();
                        console.log(result);
                        validateFields();
                        // expected output: "resolved"
                    }
                    
                    function validateFields() {

                        var everythingIsFilled = true;
                        var sessions = document.getElementById('<%= sessionPeriods.ClientID %>').value;
                        var sessionDate = document.getElementById('<%= date.ClientID %>').value;
                        var sessionTime = document.getElementById('<%= time.ClientID %>').value;

                        console.log(sessions);
                        console.log(sessionDate);
                        console.log(sessionTime);

                        if (sessions === null) {
                            everythingIsFilled = false
                        }
                        if (sessionDate === null) {
                            everythingIsFilled = false
                        }
                        if (sessionTime === null) {
                            everythingIsFilled = false
                        }

                        if (everythingIsFilled) {
                            document.getElementById('<%= btn_submit.ClientID %>').style.visibility = false;
                        } else {
                            document.getElementById('<%= btn_submit.ClientID %>').style.visibility = true;
                        }

                        
                     }
                </script>
            </div>
        </div>
    </section>
</asp:Content>
