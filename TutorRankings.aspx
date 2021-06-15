<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="TutorRankings.aspx.cs" Inherits="QuadCore_Website.TutorRankings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Average Tutor Ratings</h1>
                        </div>

                        <div class="form-group row justify-content-center">
                            <table class="table text-center">
                                <thead class="thead-primary">
                                    <tr class="text-center text-nowrap">
                                        <th>&nbsp</th>
                                        <th>Tutor Name</th>
                                        <th>&nbsp</th>
                                        <th>Tutor Surname</th>
                                        <th>&nbsp</th>
                                        <th>Average Rating</th>
                                        <th>Actions</th>
                                        <th>&nbsp</th>
                                        <th>&nbsp</th>
                                    </tr>
                                </thead>

                                <tbody id="tutorRatings" runat="server">
                                    <tr class="text-center">
                                        <td>1</td>
                                        <td>Jacob</td>
                                        <td>&nbsp</td>
                                        <td>Surname</td>
                                        <td>&nbsp</td>
                                        <td>10</td>
                                        <td>     
                                                <a href="SingleTutorApp.aspx" class="btn btn-primary">view application</a></td>
    
                                        <td>    
                                            <div class="form-group row justify-content-center">     
                                                &nbsp;</div>

                                        </td>
                                  
                                        <td>&nbsp</td>
                                        
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                      </div>
                    </div>
                </div>

</asp:Content>
