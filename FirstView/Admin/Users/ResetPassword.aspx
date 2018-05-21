<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="FirstView.Admin.Users.ResetPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
    .VeryPoor
    {
        background-color:red;
        }

        .Weak
    {
        background-color:orange;
        }

        .Average
        {
        background-color:  #A52A2A
        }
        .Excellent
        {
        background-color:yellow;
        }
        .Strong
        {
        background-color:green;
        }
        .border
        {
        border:medium #000000;
        width:200px;                
        }
    </style>
 <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
    function openModalNoImage() {
        $('#myModalNoImage').modal('show');
    }
    function CheckNumeric(obj) {
        try {
            if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        catch (err) {
            // Do nothing
        }
    }
</script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Users - Reset Password</h4>
            <p class="lead">
                <a href="Main.aspx" id="btnBack" runat="server" class="btn btn-default" style="width:80px" role="button">Back</a>    
                <asp:Button ID="butSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSave_Click" />        
            </p>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" MaxLength="50"  Width="420px" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password is a required field"></asp:RequiredFieldValidator>                   
                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" TargetControlID="txtPassword" PrefixText="Stength:" Enabled="true"  RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" PreferredPasswordLength="6" TextStrengthDescriptions="VeryPoor; Weak; Average; Strong; Excellent"  StrengthStyles="VeryPoor; Weak; Average; Excellent; Strong;"  CalculationWeightings="25;25;15;35" BarBorderCssClass="border" HelpStatusLabelID="lblPass" />
                <asp:Label ID="lblPass" runat="server" ></asp:Label>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label8" runat="server" Text="Confirm Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" MaxLength="50" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirm Password is a required field"></asp:RequiredFieldValidator>                   
                <asp:CompareValidator ID="cvPass" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="The Passwords entered do not match."></asp:CompareValidator>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="Would you like to email the new password to the User?"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="chkSendEmail" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="hidEmailAddress" runat="server" />
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
</asp:Content>
