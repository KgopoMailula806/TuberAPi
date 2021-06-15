<%@ Page Title="" Language="C#" MasterPageFile="~/Tuber.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="QuadCore_Website.Invoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="ftco-section ftco-no-pt ftco-no-pb contact-section">
			<div class="container">
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 center">
                    <div class="form-group row justify-content-center">
                        <h1>Invoices</h1>
                    </div>
                    <div class="form-group row justify-content-center">
                        <table class="table">
                            <!--Table Headings-->
						    <thead class="thead-primary">
						      <tr>
						        <th>Description</th> 
                                  <th>&nbsp&nbsp&nbsp&nbsp</th>
						        <th>Date</th>
                                <th>Amount</th>
                                <th>&nbsp</th>
                                <th>Action</th>
						      </tr>
						    </thead>
                            <!--Table Contents-->
						    <tbody id="InvoiceEntries" runat="server">
                                <tr >
                                    <td><a href="#">Tutoring Rendered: Signal Processing</a></td>
                                    <td>2020/03/01</td>
                                    <td>R 500.87</td>
                                    <td><a href="PaypalPayOut.aspx" class="btn btn-primary">Invoice Summary</a></td>                                    
                                </tr>                               
						    </tbody>
						  </table>                       
                    </div>
                        <asp:Button ID="btnGenInvoice" class="btn btn-primary" runat="server" Text="Generate PDF Invoice" />
                    <div>
                        </div>
                </div>
            </div>   
          </div>
	</section>

</asp:Content>
