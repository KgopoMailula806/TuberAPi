<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerDashboard.aspx.cs" Inherits="QuadCore_Website.ManagerDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Administration Dashboard</h2>

    <div class="row">
        <div class="col-lg-8">
            <div class="row">
                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-primary o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-handshake"></i>
                            </div>
                            <div class="mr-5" id="meetings" runat="server">26 Meetings Scheduled</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="MeetingList.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-warning o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-fw fa-list"></i>
                            </div>
                            <div class="mr-5" id="tutor_shortlist" runat="server">11 Tutors in Shortlist</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="Shortlist.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-success o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-file-alt"></i>
                            </div>
                            <div class="mr-5">Module Reports</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="ModuleReports.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-tertiary o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-star"></i>
                            </div>
                            <div class="mr-5">Tutor Ratings</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="TutorRankings.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-secondary o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-money"></i>
                            </div>
                            <div class="mr-5"  id="payments" runat="server">10 Outstanding Payments</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="OutstandingPayments.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3 col-sm-6 mb-3">
                    <div class="card text-white bg-info o-hidden h-100">
                        <div class="card-body">
                            <div class="card-body-icon">
                                <i class="fas fa-flag"></i>
                            </div>
                            <div class="mr-5">Elapse Tutorial Sessions!</div>
                        </div>
                        <a class="card-footer text-white clearfix small z-1" href="ElapsedSessions.aspx">
                            <span class="float-left">View Details</span>
                            <span class="float-right">
                                <i class="fas fa-angle-right"></i>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-4">
            <div class="top-campaign">
                <h3 class="title-3 m-b-30"><b>Worst rated tutors</b></h3>
                <div class="table-responsive">
                    <table class="table table-top-campaign">

                        <thead class="thead-primary">
                                    <tr class="text-center text-nowrap">
                                        
                                        <th>Tutor Name</th>
                                        <th>&nbsp</th>
                                        <th>Tutor Surname</th>
                                        <th>&nbsp</th>
                                        <th>Average Rating</th>
                                        <th>&nbsp</th>
                                      
        
                                    </tr>
                                </thead>

                        <tbody id="worstTutors" runat="server">
                            <tr>
                                <td>1. Sam Gumede</td>
                                <td>0.13</td>
                            </tr>
                            <tr>
                                <td>2. Mantwa Skhakhane</td>
                                <td>0.5</td>
                            </tr>
                            <tr>
                                <td>3. Covid Sinkwa</td>
                                <td>0.79</td>
                            </tr>
                            <tr>
                                <td>4. Madunusa Mkhabela</td>
                                <td>1.3</td>
                            </tr>
                            <tr>
                                <td>5. France Ntoni</td>
                                <td>1.54</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    </div>


    <input type="button" class="btn btn-primary" value="Hide Admin Links" onclick="change()" />


</asp:Content>
