<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ViewBookedSessions.aspx.cs" Inherits="QuadCore_Website.ViewBookedSessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/bg_1.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <h1 class="mb-2 bread">Booked Sessions</h1>
                    <p class="breadcrumbs"><span class="mr-2"><a href="Home.aspx">Home <i class="ion-ios-arrow-forward"></i></a></span><span>Booked Sessions <i class="ion-ios-arrow-forward"></i></span></p>
                </div>
            </div>
        </div>
    </section>
    <section class="ftco-section">
        <div class="container-fluid px-4">
            <div class="container">
                <!--Seen or unseen radio buttons--> 
                   <label id="passedLabel" runat="server" class="font-weight-bold">passed sessions</label>
                         <asp:RadioButton ID="passed" value="1" GroupName="SeenOrUnseen"  onClick="checkupcommingButton();" runat="server"  /> 
                   <label id="upcommingLabel" runat="server" class="font-weight-bold">upcomming</label>                      
                        <asp:RadioButton ID="upcomming" value="0" GroupName="SeenOrUnseen" onClick="checkpassedButton();" runat="server" />
                   <label id="canceledLabel" runat="server" class="font-weight-bold">canceled</label>                      
                        <asp:RadioButton ID="canceled" value="2" GroupName="SeenOrUnseen" onClick="checkcanceledButton();" runat="server" />

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">                        
                        <!--Get bookings button--> 
                        <div class="form-group row justify-content-center" id="searchButton" runat="server" visible="true">
                        <asp:Button ID="SerachBTN" runat="server" Text="Get Bookings" class="btn btn-primary" OnClick="btn_GetBookedSessions_Click" />
                    </div>

                        <h3 id="noSessions" runat="server" visible="true">No sessions yet</h3>

                        <div id="sessionsTable" runat="server" visible="false" class="form-group row justify-content-center">
                            <table class="table">
                                <thead class="thead-primary">
                                    <tr class="text-center">
                                        <th>&nbsp</th>
                                        <th id="actor_title" runat="server">Tutor Name</th>
                                        <th>Module</th>
                                        <th>Email</th>
                                        <th>Date</th>
                                        <th>Time</th>
                                        <th>Location</th>
                                        <th>&nbsp</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody id="peeps" runat="server">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
     <script type="text/javascript">
         
        //selectElement.style.visibility = "hidden";
        fucntionToExecuteName();
        function fucntionToExecuteName() {

             
            var valueSelected = selectElement.options[selectElement.selectedIndex].value; // get selected option value

            var text = selectElement.options[selectElement.selectedIndex].text;
            console.log(valueSelected);
           
        }
        // Checked methods      
        function showAlert() { alert('You triggered an alert!'); }
         function checkupcommingButton() {
            document.getElementById("<%=passed.ClientID%>").checked = true;
            document.getElementById("<%=upcomming.ClientID%>").checked = false;
            document.getElementById("<%=canceled.ClientID%>").checked = false;
        }
         function checkpassedButton() {
            document.getElementById("<%=passed.ClientID%>").checked = false;
            document.getElementById("<%=upcomming.ClientID%>").checked = true;
            document.getElementById("<%=canceled.ClientID%>").checked = false;

         }
         function checkcanceledButton() {
             document.getElementById("<%=passed.ClientID%>").checked = false;
             document.getElementById("<%=upcomming.ClientID%>").checked = false;
             document.getElementById("<%=canceled.ClientID%>").checked = true;
         }
    </script>
</asp:Content>
