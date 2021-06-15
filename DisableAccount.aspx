<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="DisableAccount.aspx.cs" Inherits="QuadCore_Website.DisableAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form2" runat="server">

         <div class="container" id="success" runat="server" visible="false">
           <div class="alert alert-success" role="alert">
              <h1 class="alert-heading">Account disabled successfully!</h1>
            </div>
         </div>

   <div class="form-group row justify-content-center">
                            <h1>Disable Account</h1>
                        </div>
<div class="overflow-auto" style="max-height: 400px; max-width:2000px;">
  <div class="row">
    <div class="col-12">
      <table class="table table-bordered">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Surname</th>
            <th scope="col">Email Address</th>
            <th scope="col">User type</th>
          </tr>
        </thead>
        <tbody id="tableContent" runat="server">
          

        </tbody>
      </table>
    </div>
  </div>
</div>

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">

              

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 ">

                        

                            <div class="form-group">
                                <label class="font-weight-bold">User email</label>
                                <input type="email" id="email" placeholder="" class="form-control" runat="server">
                            </div>
					

                        <div class="form-group">
                            <label class="font-weight-bold text-center">Reason for disabling the account (Mandatory)</label>
                            <textarea id="Reason" class="form-control" cols="20" runat="server"></textarea>
                        </div>

                            <asp:Button class="btn btn-primary" ID="btnDisableAcc" runat="server" Text="Submit" OnClick="btnDisableAcc_Click" />

                        
                       
                        </div>
                    

                        
                    </div>
                </div>
	</section>
    
    </form>
    
</asp:Content>
