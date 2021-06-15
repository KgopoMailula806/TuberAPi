<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="OutstandingPayments.aspx.cs" Inherits="QuadCore_Website.OutstandingPayments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Outstanding Payments</h1>
                        </div>

                        <div class="form-group row justify-content-center">
                            <table class="table text-center">
                                <thead class="thead-primary">
                                    <tr class="text-center text-nowrap">
                                        <th>&nbsp</th>
                                        <th>Client Name</th>
                                        <th>&nbsp</th>
                                        <th>Module Code</th>
                                        <th>&nbsp</th>
                                        <th>Invoice Date</th>
                                        <th>Amount</th>
                                        <th>&nbsp</th>
                                        <th>Actions</th>
                                         <th>&nbsp</th>
                                         <th>&nbsp</th>
                                    </tr>
                                </thead>

                                <tbody id="outPays" runat="server">
                                    <tr class="text-center">
                                        <td>1</td>
                                        <td>Jacob Muzonde</td>
                                        <td>&nbsp</td>
                                        <td>Sotra</td>
                                        <td>&nbsp</td>
                                        <td>08:50</td>
                                        <td>16-Jul-2020</td>
                                        <td>&nbsp</td>
                                        <td>&nbsp</td>
                                        <td>&nbsp</td>
                                        <td class="text-nowrap">
                                            <p style="display:inline-table">
                                                <a class="btn btn-primary" href="EditMeeting.aspx">remarks</a>&nbsp&nbsp<a class="btn btn-primary" href="EditMeeting.aspx">cancel</a>
                                            </p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                      </div>
                    </div>
                </div>

</asp:Content>
