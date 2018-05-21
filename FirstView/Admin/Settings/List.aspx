<%@ Page Title="Settings List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.Settings.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Settings</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>
                <asp:Button ID="butSave" runat="server" Text="Save" class="btn btn-primary"  style="width:80px" OnClick="butSave_Click" />
            </p>
        </div>        
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="System Email Address"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSystemEmailAddress" class="form-control" Width="420px" TextMode="Email" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSystemEmailAddress" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="System Email Address is a required field"></asp:RequiredFieldValidator>                   
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="SMTP Client Host"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtsmtpClientHost" class="form-control" Width="420px" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsmtpClientHost" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="SMTP Client Host is a required field"></asp:RequiredFieldValidator>                   
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="SMTP Client Port"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtsmtpClientPort" class="form-control" Width="420px" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsmtpClientPort" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="SMTP Client Port is a required field"></asp:RequiredFieldValidator>                   
            </div>
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
                        <p class="text-success"><span class="glyphicon glyphicon-ok"></span>
                            The data has been saved successfully.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>            
    </div>
</asp:Content>
