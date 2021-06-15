<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="UserModules.aspx.cs" Inherits="QuadCore_Website.UserModules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Modules</h1>
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

                                <tbody id="UserDetailsDiv" runat="server">
                                    <tr class="text-center">
                                        <td>Example Module</td>
                                        <td>Module Code</td>
                                        <td>    
                                            <div class="form-group row justify-content-center">     
                                                <asp:Button ID="Button1" runat="server" Text="remove module" class="btn btn-primary"/>
			                                </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>


                        <div class="form-group row justify-content-center">
                            <a href="ModuleList.aspx" class="btn btn-primary py-3 px-5" visible="false" id="btnAdd" runat="server"> add a new module </a>
                        </div>

                      </div>
                    </div>
                </div>
</asp:Content>
