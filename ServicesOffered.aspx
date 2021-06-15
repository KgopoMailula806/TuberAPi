<%@ Page Title="" Language="C#" MasterPageFile="~/landingMaster.Master" AutoEventWireup="true" CodeBehind="ServicesOffered.aspx.cs" Inherits="QuadCore_Website.ServicesOffered" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="hero-wrap hero-wrap-2" style="background-image: url('images/matt-ragland-02z1I7gv4ao-unsplash-min.jpg');">
        <div class="overlay"></div>
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <h1 class="mb-2 bread">Servives Offered</h1>
                    <h6 class="mb-2 bread">Bringing the tutor to you. :)</h6>
                   
                </div>
            </div>
        </div>
    </section>

     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
        <div class="container">
            <div class="form-group row justify-content-center">
                <div class="col-md-6 center">
                    <div class="form-group row justify-content-center">
                            <h1>Pricing</h1>
                        </div>
                    <table class="table">
                        <!--Table Headings-->
                        <thead class="thead-primary">
                            <tr class="text-center">
                                <th id="Th1" runat="server">All modules have a pricing of R150/hr for a single session</th>
                                
                               
                            </tr>
                        </thead>
                        <!--Table Contents-->
                        <tbody id="Tbody1" runat="server">                            
                            
                        </tbody>
                    </table>
                    <div class="form-group row justify-content-center">
                            <h1>Modules</h1>
                        </div>
                    <table class="table">
                        <!--Table Headings-->
                        <thead class="thead-primary">
                            <tr class="text-center">
                                <th id="tutorOrClientHeading" runat="server">School Level</th>
                                <th>&nbsp</th>
                                <th>module name</th>
                                <th>&nbsp</th>
                               
                            </tr>
                        </thead>
                        <!--Table Contents-->
                        <tbody id="RequestEntries" runat="server">
                            <tr>
                                <td>Primary School: Grade 3-7 (CAPS & IEB)</td>
                                <td>&nbsp</td>
                                <td>Mathematics</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Natural Science</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>English</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Mathematics</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <!--Next module catelog-->
                            <tr>
                                <td>High School: Grade 8-9 (CAPS & IEB)</td>
                                <td>&nbsp</td>
                                <td>Mathematics</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Natural Science</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>English</td>
                                <td>&nbsp</td>                              
                            </tr>                          
                            <!--Next module catelog-->
                            <tr>
                                <td>High School: Grade 8-9 (CAPS & IEB)</td>
                                <td>&nbsp</td>
                                <td>Mathematics</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Natural Science</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>English</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <!--Next module catelog-->
                            <tr>
                                <td>Cambridge: (IGSCE 1, 2 and A-Level)</td>
                                <td>&nbsp</td>
                                <td>Mathematics</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Physical Science</td>
                                <td>&nbsp</td>                              
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp</td>
                                <td>Argriculture</td>
                                <td>&nbsp</td>                              
                            </tr>           
                        </tbody>
                    </table>
                  
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
