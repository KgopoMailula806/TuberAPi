<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ex.aspx.cs" Inherits="QuadCore_Website.ex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Poppins:200,300,400,500,600,700,800,900&display=swap" rel="stylesheet" />

    <link rel="stylesheet" href="css/open-iconic-bootstrap.min.css" />
    <link rel="stylesheet" href="css/animate.css" />

    <link rel="stylesheet" href="css/owl.carousel.min.css" />
    <link rel="stylesheet" href="css/owl.theme.default.min.css" />
    <link rel="stylesheet" href="css/magnific-popup.css" />

    <link rel="stylesheet" href="css/aos.css" />

    <link rel="stylesheet" href="css/ionicons.min.css" />

    <link rel="stylesheet" href="css/flaticon.css" />
    <link rel="stylesheet" href="css/icomoon.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/HomePageStyling.css" />

    <link rel='stylesheet' type='text/css' media='screen' href='css/notification_styling.css' />
    <script src="https://kit.fontawesome.com/867944bbac.js" crossorigin="anonymous"></script>

    <link rel="stylesheet" href="css/sidebar.css" />
    <script src="js/helper.js"></script>

</head>
<body>
    <div class="bg-top navbar-light">
        <div class="container">
            <div class="row no-gutters d-flex align-items-center align-items-stretch">
                <div class="col-md-4 d-flex align-items-center py-4">
                    <a class="navbar-brand" href="Home.aspx">Tuber.</a>
                </div>
                <div class="col-lg-8 d-block">
                    <div class="row d-flex">
                        <div class="col-md d-flex topper align-items-center align-items-stretch py-md-4" id="userBox" runat="server" visible="false">
                            <div class="icon d-flex justify-content-center align-items-center"><span class="icon-user-circle"></span></div>
                            <div class="text">
                                <span id="userType" runat="server"></span>
                                <span id="name" runat="server">Tuber@business.com</span>
                            </div>
                        </div>
                        <div class="col-md d-flex topper align-items-center align-items-stretch py-md-4">
                            <div class="icon d-flex justify-content-center align-items-center"><span class="icon-paper-plane"></span></div>
                            <div class="text">
                                <span>Email</span>
                                <span>Tuber@business.com</span>
                            </div>
                        </div>
                        <div class="col-md d-flex topper align-items-center align-items-stretch py-md-4" visible="false">
                            <div class="icon d-flex justify-content-center align-items-center"><span class="icon-phone2"></span></div>
                            <div class="text">
                                <span>Call</span>
                                <span>Call Us: + 1235 2355 98</span>
                            </div>
                        </div>

                        <div class="col-md topper d-flex align-items-center justify-content-end" id="profile_" runat="server" visible="false">
                            <p class="mb-0">
                                <a href="Profile.aspx" class="btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center">
                                    <span class="icon-user-circle mr-2"></span>Profile
                                </a>
                            </p>
                        </div>

                        <div class="col-md topper d-flex align-items-center justify-content-end" runat="server" id="requestTutor_">
                            <p class="mb-0">
                                <a href="login.aspx" class="btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center">
                                    <span class="icon-location_searching mr-2"></span>Request Tutor
                                </a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <nav class="navbar navbar-expand-lg navbar-dark bg-dark ftco-navbar-light" id="ftco-navbar">
        <div class="container d-flex align-items-center px-4">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#ftco-nav" aria-controls="ftco-nav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="oi oi-menu"></span>Menu
            </button>
            <div class="collapse navbar-collapse" id="ftco-nav">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item active"><a href="Home.aspx" class="nav-link pl-0">Home</a></li>
                    <li class="nav-item" id="requestTutorNavItem" runat="server" visible="false"><a href="RequestTutor.aspx" class="nav-link">Request a Tutor</a></li>
                    <li class="nav-item" id="viewBookedSessions" runat="server" visible="false"><a href="ViewBookedSessions.aspx" visible="true" runat="server" id="sessions" class="nav-link">Booked Sessions</a></li>
                    <li class="nav-item" id="ViewBookingRequests" runat="server" visible="false"><a href="ViewBookingRequest.aspx" class="nav-link">Booking Requests</a></li>
                    <li class="nav-item" runat="server" id="contact"><a href="Contact.aspx" class="nav-link">Contact</a></li>
                    <li class="nav-item" runat="server" id="login" visible="true"><a href="Login.aspx" class="nav-link">Sign In</a></li>
                    <li class="nav-item" runat="server" id="switchAccountProperty"></li>
                    <li class="nav-item" runat="server" id="Modules" visible="false"><a href="Modules.aspx" class="nav-link">Modules</a></li>
                    <li class="nav-item" runat="server" id="ManagerTerminal" visible="false"><a href="SystemTerminal.aspx" class="nav-link">Admninistration Dashboard</a></li>
                    <li class="droupdown nav-item box" id="notiBox" runat="server" visible="false">
                        <li class="nav-item" runat="server" id="logout" visible="false"><a href="Logout.aspx" class="nav-link">Log out</a></li>


                        <a class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <div class="box">
                                <div class="notifications">
                                    <i class="fas fa-bell"></i>
                                    <span class="num" runat="server" id="noti_number">3</span>
                                </div>
                            </div>
                        </a>

                        <!--Notifications-->
                        <div class="dropdown-menu noti_list" aria-labelledby="dropdown01" id="Notifications" runat="server">
                        </div>

                    </li>
                </ul>

            </div>
        </div>
    </nav>

    <div class="wrapper d-flex align-items-stretch">
        <nav id="sidebar" class="active">

            <div class="p-4 pt-5">
                <h1><a href="index.html" class="logo">Your Menu</a></h1>
                <ul class="list-unstyled components mb-5">

                    <li class="active">
                        <a href="#homeSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">Home</a>
                        <ul class="collapse list-unstyled" id="homeSubmenu">
                            <li>
                                <a href="#">Home 1</a>
                            </li>
                            <li>
                                <a href="#">Home 2</a>
                            </li>
                            <li>
                                <a href="#">Home 3</a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="#">About</a>
                    </li>
                    <li>
                        <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">Pages</a>
                        <ul class="collapse list-unstyled" id="pageSubmenu">
                            <li>
                                <a href="#">Page 1</a>
                            </li>
                            <li>
                                <a href="#">Page 2</a>
                            </li>
                            <li>
                                <a href="#">Page 3</a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="#">Portfolio</a>
                    </li>
                    <li>
                        <a href="#">Contact</a>
                    </li>
                </ul>

                <div class="mb-5">
                    <h3 class="h6">Subscribe for newsletter</h3>

                </div>


            </div>
        </nav>

        <!-- Page Content  -->
        <div id="sidebar-sibling" class="p-4 p-md-5 pt-5">

            <h2 class="mb-4">Sidebar #02</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <input type="button" class="btn btn-primary" value="Button Name" onclick="change()" />
            <form runat="server">
                <asp:Button ID="btnToggle" runat="server" Text="Open Modal" class="btn btn-primary" OnClick="btnToggle_Click" data-toggle="modal" data-target="#smallmodal"/>
            </form>

        </div>
    </div>


    <footer class="ftco-footer ftco-bg-dark ftco-section">

        <div class="row">
            <div class="col-md-12 text-center">
                <div class="ftco-footer-widget mb-5 ml-md-4">
                    <h2 class="ftco-heading-2 mb-0">Our Socials</h2>
                    <ul class="ftco-footer-social list-unstyled float-md-none float-lft mt-3">
                        <li class="ftco-animate"><a href="#"><span class="icon-twitter"></span></a></li>
                        <li class="ftco-animate"><a href="#"><span class="icon-facebook"></span></a></li>
                        <li class="ftco-animate"><a href="#"><span class="icon-instagram"></span></a></li>
                    </ul>
                </div>

                <!-- modal small -->
                <div class="modal fade" id="smallmodal" tabindex="-1" role="dialog" aria-labelledby="smallmodalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-sm" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="smallmodalLabel">Small Modal</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>
                                    Email was sent bafo.
                                </p>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                <button type="button" class="btn btn-primary">Confirm</button>
                            </div>
                        </div>
                    </div>
                </div>

                <p>
                    <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
                    Copyright &copy;<script>document.write(new Date().getFullYear());</script>
                    All rights reserved | This template is made with <i class="icon-heart" aria-hidden="true"></i>by <a href="https://colorlib.com" target="_blank">Colorlib</a>
                    <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
                </p>
            </div>
        </div>
    </footer>

    <!-- loader -->
    <div id="ftco-loader" class="show fullscreen">
        <svg class="circular" width="48px" height="48px">
            <circle class="path-bg" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke="#eeeeee" />
            <circle class="path" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke-miterlimit="10" stroke="#F96D00" />
        </svg>
    </div>


    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-migrate-3.0.1.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.easing.1.3.js"></script>
    <script src="js/jquery.waypoints.min.js"></script>
    <script src="js/jquery.stellar.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/jquery.magnific-popup.min.js"></script>
    <script src="js/aos.js"></script>
    <script src="js/jquery.animateNumber.min.js"></script>
    <script src="js/scrollax.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBVWaKrjvy3MaE7SQ74_uJiULgl1JY0H2s&sensor=false"></script>
    <script src="js/google-map.js"></script>
    <script src="js/main.js"></script>
   

</body>

</html>
