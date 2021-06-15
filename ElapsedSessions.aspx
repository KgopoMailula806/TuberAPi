<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ElapsedSessions.aspx.cs" Inherits="QuadCore_Website.ElapsedSessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Elapsed Tutorial Sessions</h1>
                        </div>

                        <div class="form-group row justify-content-center">
                            <table class="table text-center">
                                <thead class="thead-primary">
                                    <tr class="text-center text-nowrap">
                                        <th>&nbsp</th>
                                        <th>Module Code</th>
                                        <th>&nbsp</th>
                                        <th>Tutor Name</th>
                                        <th>&nbsp</th>
                                        <th>Client Name</th>
                                        <th>&nbsp</th>
                                        <th>Location</th>
                                        <th>&nbsp</th>
                                        <th>Start Time</th>
                                        <th>&nbsp</th>
                                        <th>End Time</th>
                                        <th>&nbsp</th>
                                        <th>&nbsp</th>
                                    </tr>
                                </thead>

                                <tbody id="elapsedSession" runat="server">
                                    <tr class="text-center">
                                        <td>1</td>
                                        <td>Maths</td>
                                        <td>&nbsp</td>
                                        <td>Jacob Muzonde</td>
                                        <td>&nbsp</td>
                                        <td>Keane Burgers</td>
                                        <td>&nbsp</td>
                                        <td>28 Summers Street</td>
                                        <td>&nbsp</td>
                                        <td>18:30</td>
                                        <th>&nbsp</th>
                                        <th>19:30</th>
                                        <td>&nbsp</td>
                                        <td>&nbsp</td>
                                        
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                      </div>
                    </div>
                </div>

</asp:Content>
