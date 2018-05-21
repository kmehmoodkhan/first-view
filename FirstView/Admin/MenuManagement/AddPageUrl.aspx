<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPageUrl.aspx.cs" Inherits="FirstView.Admin.MenuManagement.AddPageUrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function redirect() {
            var val = document.getElementById("<%=lblMessage.ClientID%>").value;
            debugger;
            if (val.indexOf("Error") == -1) {
                window.location.href = "PagesList.aspx";
            }
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Pages - Add</h4>
            <p class="lead">
                <a href="PagesList.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="btnSave_Click" />
            </p>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Page Title
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPageTitle" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPageTitle" ControlToValidate="txtPageTitle" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row" id="fileUploadRow" runat="server">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Upload
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:FileUpload ID="fpFileUpload" runat="server" />
            </div>
        </div>
        <div class="row" id="Div1" runat="server">
            <div class="col-md-6 col-sm-6 col-xs-12 text-center marginTop">
                OR            
            </div>
        </div>
        <div class="row" id="pageUrlRow" runat="server">
            <div class="col-md-2 col-sm-6 col-xs-12 marginTop">
                Page Url
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12 marginTop">
                <asp:TextBox ID="lblPageUrl" class="form-control" Width="420px" runat="server"></asp:TextBox>
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
                    <p class="text-success" id="pmesage" runat="server">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </p>
                    <span class="glyphicon glyphicon-ok"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="redirect();">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
