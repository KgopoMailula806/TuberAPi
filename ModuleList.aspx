<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="QuadCore_Website.ModuleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


       <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Modules Currently On Offer</h1>
                        </div>

                        <div class="form-group row justify-content-center">
                            <table class="table">
                                <thead class="thead-primary">
                                    <tr class="text-center">
                                        <th>Module Name</th>
                                        <th>Module Code</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>

                                <tbody id="modules" runat="server">
                                    <tr class="text-center">
                                        <td>Example Module</td>
                                        <td>Module Code</td>
                                        <td>    
                                            <div class="form-group row justify-content-center">     
                                                <a href="#" class="btn btn-primary">add module</a>
			                                </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>


                        <div class="form-group row justify-content-center">
                            <a href="Modules.aspx" runat="server" class="btn btn-primary py-3 px-5"> my modules</a>
                        </div>

                      </div>
                    </div>
                </div>

</asp:Content>
