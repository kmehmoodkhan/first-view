<%@ Page Title="Artist Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="FirstView.Admin.Artists.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalFileSize() {
            $('#myModalFileSize').modal('show');
        }
        function openModalNoImage() {
            $('#myModalNoImage').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Artist - Edit</h4>
            <p class="lead">
                <a href="List.aspx" class="btn btn-default" role="button" style="width: 80px">Back</a>
                <asp:Button ID="butSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSave_Click" />
            </p>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtName" class="form-control" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Surname"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSurname" class="form-control" Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSurname" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="CV"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtCV" runat="server" Text="  " class="form-control" Width="320px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCV" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="CV is a required field"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" Text="Artist Type"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlArtistType" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlArtistType" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Artist Type is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row marginTop">
            <div class="col-md-2">
                <asp:Label ID="Label4" runat="server" Text="Photo"></asp:Label>
            </div>
            <div class="col-md-5">
                <iframe id="ifrmUpload" runat="server" width="400" height="200" style="border: none;"></iframe>
            </div>
            <div class="col-md-4 ArtistWorkItemContainer">
                <asp:Image ID="imgArtist" runat="server" CssClass="ArtistWorkItem  img-thumbnail" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblIsActive" runat="server" Text="Is Active"></asp:Label>
            </div>
             <div class="col-md-5">
                <asp:CheckBox ID="chkIsActive" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="hidUniqueID" runat="server" />
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
                    <p class="text-success">
                        <span class="glyphicon glyphicon-ok"></span>
                        The data has been saved successfully.                                                
                    </p>
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
                    <h4 class="modal-title">Error</h4>
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
