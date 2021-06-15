<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleTutorAppPopup.aspx.cs" Inherits="QuadCore_Website.SingleTutorAppPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    
    <link href="https://fonts.googleapis.com/css?family=Poppins:200,300,400,500,600,700,800,900&display=swap" rel="stylesheet"/>

    <link rel="stylesheet" href="css/open-iconic-bootstrap.min.css"/>
    <link rel="stylesheet" href="css/animate.css"/>
    
    <link rel="stylesheet" href="css/owl.carousel.min.css"/>
    <link rel="stylesheet" href="css/owl.theme.default.min.css"/>
    <link rel="stylesheet" href="css/magnific-popup.css"/>

    <link rel="stylesheet" href="css/aos.css"/>

    <link rel="stylesheet" href="css/ionicons.min.css"/>
    
    <link rel="stylesheet" href="css/flaticon.css"/>
    <link rel="stylesheet" href="css/icomoon.css"/>
    <link rel="stylesheet" href="css/style.css"/>
    <link rel="stylesheet" href="css/HomePageStyling.css"/>

    <link rel='stylesheet' type='text/css' media='screen' href='css/notification_styling.css'/>
    <script src="https://kit.fontawesome.com/867944bbac.js" crossorigin="anonymous"></script>

    <script src="js/helper.js"></script>
    <link rel="stylesheet" href="css/popup.css"/>

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
					    	<div class="icon d-flex justify-content-center align-items-center" ><span class="icon-user-circle"></span></div>
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
					    		<a href="Profile.aspx" class="btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center" >
					    			<span class="icon-user-circle mr-2"></span> Profile
					    		</a>
					    	</p>
					    </div>

                        <div class="col-md topper d-flex align-items-center justify-content-end" runat="server" id="requestTutor_">
					    	<p class="mb-0">
					    		<a href="login.aspx" class="btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center" >
					    			<span class="icon-location_searching mr-2"></span> Request Tutor
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
	        <span class="oi oi-menu"></span> Menu
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



     <section class="ftco-section ftco-no-pt ftco-no-pb contact-section" id="play_ground">

   
			<div class="container" id="blurry_content">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                        <div class="form-group row justify-content-center">
                            <h1>Applicant Details</h1>
                        </div>

                     <div class="form-group row justify-content-center">
                        <table class="table">
                                <thead class="thead-primary">
                                    <tr class="text-center">
                                        <th>Assessment</th>
                                        <th>Applicant Details</th>
                                        <th>Applicant Image</th>
                                        <th>Documents</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <tr class="text-center" runat="server">
                                        <td>
                                          
                                            <p id ="PoliceTag" runat="server"> Is police clearance valid?</p>
                                               

                                            <p>&nbsp</p>
                                            <p>Is CV impressive?</p>
                                               

                                            <p>&nbsp</p>
                                            <p>Academic Record Average:</p>
                                                
                                        </td>
                                        <td>
                                            <div class="col-md-6 col-lg-4">
                                                <div class="form-group" id="TutorInfor" runat="server">
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                             <asp:Image ID="TutorImage" runat="server" width="400" height="450" />
                                        </td>
                                        <td id="documents" runat="server">
                                             
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                         <div class="form-group row justify-content-center">

                             <input type="button" value="Set Up Meeting" id="meeting" class="btn btn-primary" onclick="openForm()" />
                             &nbsp
                             &nbsp
                             &nbsp
                             &nbsp
                             <input type="button" value="Add To Shortlist" id="shortlist" class="btn btn-primary"/>
                              &nbsp
                             &nbsp
                             &nbsp
                             &nbsp
                             <input type="button" value="Reject" id="rejection" class="btn btn-primary"/>
                         </div> 
                      </div>   
                    </div> </div></div>
               
            <div class="bg-popup" id="myForm">
            <div class="popup-content bg-white">
                <div class="close" onclick="closeForm()" style="color:orangered;">+</div>
                <form action="#" runat="server">
                    <div class="form-group row justify-content-center">
                        <h5 class="font-weight-bold">Set Meeting Details</h5>
                    </div>
                        
                        <label>Meeting Date:</label>
                        <input type="date" id="date" onfocus="(this.type='date')" min="" runat="server"/>
                        <label>Meeting Time:</label>
                        <input type="time" id="appt" min="09:00" max="18:00" value="09:00"/>
                        
                        <label>Meeting Venue:</label>

                        <asp:DropDownList ID="Venue"  style="bottom:20px;" runat="server">
                            <asp:ListItem Enabled="true" Text="Select Meeting Venue" Value="-1"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="Sotra" Value="1"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="Vaal" Value="2"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="Khaya Litsha" Value="3"></asp:ListItem>
                        </asp:DropDownList>

                        <label>&nbsp</label>
                        <asp:Button ID="btnSubMeeting" runat="server" Text="Submit Meeting Request" class="btn btn-primary" OnClick="btnSubMeeting_Click" />
                    </form>
                </div>   
            </div>
	</section>

    

  <div id="ftco-loader" class="show fullscreen"><svg class="circular" width="48px" height="48px"><circle class="path-bg" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke="#eeeeee"/><circle class="path" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke-miterlimit="10" stroke="#F96D00"/></svg></div>

</body>
  
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


</html>
