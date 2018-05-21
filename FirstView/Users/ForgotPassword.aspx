
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="FirstView.Users.ForgotPassword" %>
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
            <h4>Forgot Password</h4>
            <p>Password Recovery. Please enter the Email Address that you used when registering on the site. You will receive an email with further instructions on how to to reset your password.</p>
            <p class="lead">
                <a href="Login.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>    
                <asp:Button ID="butSubmit" class="btn btn-primary" runat="server" Width="80px" Text="Submit" OnClick="butSubmit_Click" />                
            </p>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                Username
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtUsername" MaxLength="50" class="form-control" Width="420px" runat="server"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="rfvtxtUsername" runat="server" CssClass="text-danger" ControlToValidate="txtUsername" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Username is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                Email Address
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmailAddress" MaxLength="50" TextMode="Email" class="form-control" Width="420px" runat="server"></asp:TextBox> 
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ControlToValidate="txtEmailAddress" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Email Address is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
    </div>
    <div id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p class="text-war">The data has been saved successfully.</p>
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
                    <h4 class="modal-title">
                        Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The login details that you have entered are incorrect. Please try again.</p>                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>     
</asp:Content>
