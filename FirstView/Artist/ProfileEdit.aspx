<%@ Page Title="Page Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileEdit.aspx.cs" Inherits="FirstView.Artist.ProfileEdit" %>

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
            <h4>Manage My Profile</h4>
            <p class="text-success" id="registration" runat="server" style="display: none">
                Your registration information has been saved successfully. Please complete your information below and submit for Approval. You will be notified via email once your request has been reviewed.
            </p>
            <p class="lead">
                <a href="Menu.aspx" class="btn btn-default" role="button" style="width: 80px">Back</a>
                <asp:Button ID="butSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSave_Click" />
                <asp:Button ID="butPreview" class="btn btn-default" runat="server" Width="80px" Text="Preview" OnClick="butPreview_Click" />
            </p>
        </div>
        <div class="row marginTop">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label2" runat="server" Text="Surname"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtSurname" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSurname" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label5" runat="server" Text="Artist Type"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:DropDownList ID="ddlArtistType" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlArtistType" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Artist Type is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label6" runat="server" Text="Artist Price"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <div style="float: left;">
                    <asp:CheckBox ID="checkBoxArtistPrice" runat="server" /></div>
                <div style="float: left; color:red;margin-left:5px">
                    * Most artist do not select this option
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label3" runat="server" Text="CV"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtCV" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCV" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="CV is a required field"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 marginTop">
                <asp:Label ID="Label4" runat="server" Text="Photo"></asp:Label>
            </div>
            <div class="col-md-5 marginTop">
                <iframe id="ifrmUpload" runat="server" width="400" height="200" style="border: none;"></iframe>
            </div>
            <div class="col-md-3 marginTop" style="display: flex; align-items: center;">
                <asp:Image ID="imgArtist" runat="server" Style="display: block; max-width: 150px; max-height: 150px; margin: auto;" CssClass="img-thumbnail" />
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
