<%@ Page Title="Page Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileAdd.aspx.cs" Inherits="FirstView.Artist.ProfileAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
    function openModalNoImage() {
        $('#myModalNoImage').modal('show');
    }
</script>
    <div class="container">
        <div class="page-header">
            <h4>My Page</h4>
            <p class="lead">
                <a href="Main.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>    
                <asp:Button ID="butSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSave_Click" />                
            </p>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtName" class="form-control" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Surname"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSurname" class="form-control"  Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ControlToValidate="txtSurname" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="CV"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtCV" runat="server" class="form-control" Width="320px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-danger" ControlToValidate="txtCV" SetFocusOnError="true" Display="Dynamic" ErrorMessage="CV is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" Text="Artist Type"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlArtistType" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger" ControlToValidate="ddlArtistType" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Artist Type is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label4" runat="server" Text="Photo"></asp:Label>
            </div>
            <div class="col-md-8">    
                <iframe id="ifrmUpload" runat="server" width="400" height="200" style="border:none;">
                </iframe>   
            </div>
        </div>
        <br />
        <asp:HiddenField ID="hidUniqueID" runat="server" />
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
    <div id="myModalNoImage" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;Please select a valid image file to upload.</p>                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>     
</asp:Content>
