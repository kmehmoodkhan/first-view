<%@ Page Title="" EnableViewStateMac="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FirstView.Users.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalLogin() {
            $('#myModalLogin').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Login</h4>
            <p>Please enter your login details below:  <span class="pull-right"><asp:Label ID="lblStatus" CssClass="text-info" runat="server" Text="" Visible="false"></asp:Label>.</span></p>
           
            
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtUsername" MaxLength="50" class="form-control" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="txtUsername" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Username is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" MaxLength="50" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ControlToValidate="txtPassword" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Password is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row marginTop">
            <div class="col-md-2" style="text-align: left">
                &nbsp;
            </div>
            <div class="col-md-4">
                <p class="lead">
                    <asp:Button ID="butLogin" class="btn btn-primary" runat="server" Width="80px" Text="Login" OnClick="butLogin_Click" />
                </p>
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-2" style="text-align: left">
                &nbsp;
            </div>
            <div class="col-md-4">
                <a href="ForgotPassword.aspx" class="btn-link" style="width: 80px" role="button" title="Click here to reset your Password">Forgot Password</a>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p class="text-success">The data has been saved successfully.</p>
                    <span class="glyphicon glyphicon-ok"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalLogin" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The Login details that you have entered are incorrect. Please try again.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
