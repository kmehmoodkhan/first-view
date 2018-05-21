<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterVerify.aspx.cs" Inherits="FirstView.Users.RegisterVerify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
    function openModalUsernameExists() {
        $('#myModalUsernameExists').modal('show');
    }
</script>
    <div class="container">
        <div class="page-header" id="reg1" runat="server">
            <h4>Registration Verification</h4>
            <p class="text-warning">Before you can login, you are required to verify your account:</p>
            <p class="text-warning">
                An email will be sent to your shortly containing instructions on how to verify your account.                 
                Once your account has been verified you can log into the system.
            </p>
            <br /><br />
            <p><b>Why do I need to do this?</b></p>
            <p>This process assists us to ensure the validity of users on our site. Thank you for your understanding.</p>
            <p ><b>I did not receive the email?</b></p>
            <p>Please check your junk/spam folders for the email, the sender will be admin@first-view.uk </p>
            <p ><b>Still having problems logging in?</b></p>
            <p>Please contact us at admin@first-view.uk , we will be glad to assist you.</p>
        </div>
        <div class="page-header" id="reg2" runat="server">
            <h4>Registration Verification</h4>
            <p class="text-success">Thank you, your account has been successfully verified:</p>
            <br /><br />
            <p><a href="Login.aspx">Please click here to Login</a></p>
        </div>
        <div class="page-header" id="reg3" runat="server">
            <h4>Registration Verification</h4>
            <p class="text-success">Your account has already been verified:</p>
            <br /><br />
            <p><a href="Login.aspx">Please click here to Login</a></p>
        </div>
    </div>
</asp:Content>
