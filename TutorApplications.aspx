<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="TutorApplications.aspx.cs" Inherits="QuadCore_Website.TutorApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Pending Tutor Applications</h1>
                        </div>

                        <div class="form-group row justify-content-center">
                            <table class="table">
                                <thead class="thead-primary">
                                    <tr class="text-center">
                                        <th>&nbsp</th>
                                        <th>Applicant Name</th>
                                        <th>&nbsp</th>
                                        <th>&nbsp</th>
                                    </tr>
                                </thead>

                                <tbody id="tutors" runat="server">
                                    <tr class="text-center">
                                        <td>1</td>
                                        <td>Jacob Muzonde</td>
                                        <td>Orange Farm</td>
                                        <td>    
                                            <div class="form-group row justify-content-center">     
                                                <a href="SingleTutorApp.aspx" class="btn btn-primary">view application</a>
			                                </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                      </div>
                    </div>
                </div>

</asp:Content>
