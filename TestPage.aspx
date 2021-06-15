<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="QuadCore_Website.TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <asp:Button ID="btnSend" runat="server" Text="Send Email" class="btn btn-primary" OnClick="btnSend_Click" />
    </form>
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

    <script>

        function mailFailed() {

            $('#smallmodal').show();
        }

        function mailSuccess() {

            $('#smallmodal').data.toggle('modal');

        }

    </script>

</asp:Content>

