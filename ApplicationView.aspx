<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ApplicationView.aspx.cs" Inherits="QuadCore_Website.ApplicationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--<link rel="stylesheet" href="css/popup.css" />-->
    <script src="js/helper.js"></script>

    <script type="text/javascript">
        function ShortListMiddleMan() {

            $.ajax({
                type: "POST",
                url: "ApplicationView.aspx/Shortlist",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert("Tutor added to shortlist");
                    window.location.replace("Shortlist.aspx");
                },
                error: function (e) {
                    alert("Something Went Wrong");
                }
            });
        }

        function RejectMiddleMan() {

            $.ajax({
                type: "POST",
                url: "ApplicationView.aspx/Reject",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert("Tutor is Rejcted");
                    window.location.replace("TutorApplications.aspx");
                },
                error: function (e) {
                    alert("Something Went Wrong");
                }
            });
        }

        function validateRadios() {
            var policeRadios = document.getElementsByName('Police');
            var val;
            for (var i = 0, n = policeRadios.length; i < n; i++) {
                if (policeRadios[i].checked) {
                    val = policeRadios[i].value;
                }
            }
            alert(val);
        }
    </script>

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section" id="play_ground">

        <form runat="server">
        <div class="container" id="blurry_content">
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    <div class="form-group row justify-content-center">
                        <h1>Applicant Details</h1>
                    </div>

                    <div class="form-group row justify-content-center">
                        <table class="table">
                            <thead class="thead-primary">
                                <tr class="text-center">
                                    <th>Assessment</th>
                                    <th>Applicant Details</th>
                                    <th>Applicant Image</th>
                                    <th>Documents</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr class="text-center" runat="server">
                                    <td>

                                        <p id="PoliceTag" runat="server">Is police clearance valid?</p>
                                        <fieldset id="Police" onclick="validateRadios()">
                                            <input type="radio" runat="server" id="PC_Yes" style="display: inline;" name="Police" />
                                            <label for="PC_Yes" style="display: inline;">Yes</label>
                                            &nbsp
                                                <input type="radio" runat="server" id="PC_No" style="display: inline;" name="Police" />
                                            <label for="PC_No" style="display: inline;">No</label>
                                        </fieldset>

                                        <p>&nbsp</p>
                                        <p>Is CV impressive?</p>
                                        <fieldset id="CV">
                                            <input type="radio" runat="server" id="CV_Yes" style="display: inline;" name="CV" />
                                            <label for="CV_Yes" style="display: inline;">Yes</label>
                                            &nbsp
                                                <input type="radio" runat="server" id="CV_No" style="display: inline;" name="CV" />
                                            <label for="CV_No" style="display: inline;">No</label>
                                        </fieldset>

                                        <p>&nbsp</p>
                                        <p>Academic Record Average:</p>
                                        <fieldset id="Academic">
                                            <input type="radio" runat="server" id="Tier1" style="display: inline;" name="Academic" />
                                            <label for="Tier1" style="display: inline;">40%-59%</label>
                                            <input type="radio" runat="server" id="Tier2" style="display: inline;" name="Academic" />
                                            <label for="Tier2" style="display: inline;">60%-79%</label>
                                            <input type="radio" runat="server" id="Tier3" style="display: inline;" name="Academic" />
                                            <label for="Tier3" style="display: inline;">80%-99%</label>
                                        </fieldset>

                                    </td>
                                    <td>
                                        <div class="col-md-6 col-lg-4">
                                            <div class="form-group" id="TutorInfor" runat="server">
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="TutorImage" runat="server" Width="400" Height="450" />
                                    </td>
                                    <td id="documents" runat="server"></td>
                                </tr>
                            </tbody>
                        </table>

                        <div class="form-group row justify-content-center">

                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mediumModal" id="meeting">Set Up Meeting</button>
                             &nbsp
                             &nbsp
                             &nbsp
                             &nbsp
                            <asp:Button ID="btnShortlist" runat="server" Text="Add To Shortlist" class="btn btn-primary" OnClick="btnShortlist_Click" />
                             &nbsp
                             &nbsp
                             &nbsp
                             &nbsp
                            <asp:Button ID="btnRejection" runat="server" Text="Reject" class="btn btn-primary" OnClick="btnRejection_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
            <div class="modal fade" id="mediumModal" tabindex="-1" role="dialog" aria-labelledby="mediumModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="mediumModalLabel">Set Meeting Details</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row justify-content-center">
                                <div class="col-md-6 center">
                                    <div class="form-group">
                                        <label class="font-weight-bold" style="display: block;">Meeting Date:</label>
                                        <input type="date" id="date" onfocus="(this.type='date')" min="" class="form-control" runat="server" />
                                    </div>

                                    <div class="form-group">
                                        <label class="font-weight-bold" style="display: block;">Meeting Time:</label>
                                        <input type="time" id="time" min="09:00" max="18:00" value="09:00" runat="server" class="form-control" />
                                    </div>


                                    <div class="form-group">

                                        <label class="font-weight-bold" style="display: block;">Meeting Venue:</label>
                                        <asp:DropDownList ID="Venue" Style="bottom: 20px;" runat="server" class="form-control">
                                            <asp:ListItem Enabled="true" Text="Select Meeting Venue" Value="-1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Aukland Park Office" Value="Auckland Park Office"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Soweto Office" Value="Soweto Office"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Cape Town Office" Value="Cape Town Office"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <asp:Button ID="btnSubMeeting" runat="server" Text="Submit Meeting Request" class="btn btn-primary" OnClick="btnSubMeeting_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>

    </section>





</asp:Content>
