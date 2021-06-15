<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ApplicationVerdict.aspx.cs" Inherits="QuadCore_Website.ApplicationVerdict" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
                    <div class="form-group row justify-content-center">
                        <div class="col-md-6 center">
                            <div class="form-group row justify-content-center">
                                <h1 id="verdic" runat="server">Application Verdict</h1>
                            </div>

                            <div class="form-group row justify-content-center">
                                <table class="table text-center">
                                    <thead class="thead-primary">
                                        <tr class="text-center text-nowrap">
                                            <th>&nbsp</th>
                                            <th>Applicant Name</th>
                                            <th>&nbsp</th>
                                            <th>&nbsp</th>
                                            <th>&nbsp</th>
                                            <th>Actions</th>
                                          
                                        </tr>
                                    </thead>

                                    <tbody id="meetings" runat="server">
                                        <tr class="text-center">
                                            <td>1</td>
                                            <td>Jacob Muzonde</td>
                                            <td>&nbsp</td>
                                            <td class="text-nowrap">
                                                <p style="display:inline-table">
                                                    <a class="btn btn-primary" href="EditMeeting.aspx">view application</a>&nbsp&nbsp<a class="btn btn-primary" href="EditMeeting.aspx">Accept</a>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                          </div>
                        </div>
                    </div>
        </section>
</asp:Content>
