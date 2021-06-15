<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="UserNotifications.aspx.cs" Inherits="QuadCore_Website.UserNotifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">

            <div class="row d-flex align-items-stretch no-gutters">

                <!--First/left column-->
                <div class="col-md-6 p-4 p-md-5 order-md-first" runat="server">                    
                    <div class="form-group">

                        <!--Notification Filter dropdown-->
                        <asp:DropDownList ID="notificationFilter" runat="server" class="form-control" onchange="fucntionToExecuteName()">
                            <asp:ListItem Enabled="true" Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="From selected Date to present" Value="1"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="From selected Date to Other SelectedDate" Value="2"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="Before selected Date" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        
                        <!--dates--> 
                        <label id="selectedDateLabel" runat="server" class="font-weight-bold">From</label>
                        <input type="date" id="selectedDate" runat="server" class="form-control">

                        <label id="secondarySelectedDateLabel" runat="server" class="font-weight-bold">To</label>
                        <input type="date" id="secondarySelectedDate" runat="server" class="form-control">
                                               
                    </div>
                     <!--Seen or unseen radio buttons--> 
                   <label id="seenLabel" runat="server" class="font-weight-bold">seen</label>
                         <asp:RadioButton ID="Seen" value="1" GroupName="SeenOrUnseen"  onClick="uncheckunseenButton();" runat="server"  /> 
                   <label id="unseenLabel" runat="server" class="font-weight-bold">unseen</label>
                        <asp:RadioButton ID="Unseen" value="0" GroupName="SeenOrUnseen" onClick="uncheckseenButton();" runat="server" />

                    <!--search Button--> 
                    <div class="form-group row justify-content-center" id="searchButton" runat="server" visible="true">
                        <asp:Button ID="SerachBTN" runat="server" Text="Fetch Notifications" class="btn btn-primary" OnClick="btn_GetSearForNotifications_Click" />
                    </div>
                </div>
            </div>
            <h6 id="notificationHeader" runat="server">Unseen Notifications</h6>
            <!--All Notifications-->
            <div class="text pt-4" id="Notifications" runat="server" visible="true">
                
            </div>
            <div id="alert" runat="server"></div>

        </div>

    </section>
    <script type="text/javascript">
        //document.getElementById("<%=notificationFilter.ClientID%>").addEventListener("click", fucntionToExecuteName, false)

        //var selectElement = document.getElementById("<%=notificationFilter.ClientID%>");
        //selectElement.style.visibility = "hidden";
        fucntionToExecuteName();
        function fucntionToExecuteName() {

            var selectElement = document.getElementById("<%=notificationFilter.ClientID%>");
            var valueSelected = selectElement.options[selectElement.selectedIndex].value; // get selected option value

            var text = selectElement.options[selectElement.selectedIndex].text;
            console.log(valueSelected);
            switch (valueSelected) {
                case "0": //All show no visibility filtering components 
                    {     //Just fetch all of the notifications 
                        console.log(valueSelected);
                        selectElement.style.visibility = "visible";

                        document.getElementById("<%=selectedDateLabel.ClientID %>").style.visibility = "hidden";                       
                        document.getElementById("<%=selectedDate.ClientID %>").style.visibility = "hidden";
                        //In case the visibile was showing
                        document.getElementById("<%=secondarySelectedDateLabel.ClientID %>").style.visibility = "hidden";
                        document.getElementById("<%=secondarySelectedDate.ClientID %>").style.visibility = "hidden";
                                               
                    } break;
                case "1": //From selected Date to present
                    {
                        console.log(text);
                        selectElement.style.visibility = "visible";

                        document.getElementById("<%=selectedDateLabel.ClientID %>").style.visibility = "visible";
                        document.getElementById("<%=selectedDate.ClientID %>").style.visibility = "visible";
                        //In case the visibile was showing
                        document.getElementById("<%=secondarySelectedDateLabel.ClientID %>").style.visibility = "hidden";
                        document.getElementById("<%=secondarySelectedDate.ClientID %>").style.visibility = "hidden";
                    } break;
                case "2": //From selected Date to Other SelectedDate
                    {
                        selectElement.style.visibility = "visible";

                        document.getElementById("<%=selectedDateLabel.ClientID %>").style.visibility = "visible";
                        document.getElementById("<%=selectedDate.ClientID %>").style.visibility = "visible";
                        //In case the visibile was showing
                        document.getElementById("<%=secondarySelectedDateLabel.ClientID %>").style.visibility = "visible";
                        document.getElementById("<%=secondarySelectedDate.ClientID %>").style.visibility = "visible";
                    } break;
                case "3"://Before selected Date
                    {
                        selectElement.style.visibility = "visible";

                        document.getElementById("<%=selectedDateLabel.ClientID %>").style.visibility = "visible";
                        document.getElementById("<%=selectedDate.ClientID %>").style.visibility = "visible";
                        //In case the visibile was showing
                        document.getElementById("<%=secondarySelectedDateLabel.ClientID %>").style.visibility = "hidden";
                        document.getElementById("<%=secondarySelectedDate.ClientID %>").style.visibility = "hidden";

                    } break;
            }
        }
        // Checked methods      
        function showAlert() { alert('You triggered an alert!'); }
        function uncheckunseenButton() {
            document.getElementById("<%=Seen.ClientID%>").checked = true;
            document.getElementById("<%=Unseen.ClientID%>").checked = false;
        }
        function uncheckseenButton() {
            document.getElementById("<%=Seen.ClientID%>").checked = false;
            document.getElementById("<%=Unseen.ClientID%>").checked = true;
        }
    </script>
    <script runat="server">


    </script>
</asp:Content>
