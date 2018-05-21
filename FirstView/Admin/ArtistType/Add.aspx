<%@ Page Title="Artist Type Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FirstView.Admin.ArtistType.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
    function openModalExists() {
        $('#myModalExists').modal('show');
    }
</script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Artist Type - Add</h4>
            <p class="lead">
                <a href="List.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>    
                <asp:Button ID="butSave" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSave_Click" />                
            </p>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Artist Type"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtArtistType" class="form-control" Width="420px" runat="server" MaxLength="50" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtArtistType" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Artist Type is a required field"></asp:RequiredFieldValidator>         
            </div>
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
    <div id="myModalExists" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The Artist Type entered already exists. Please enter another.</p>                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>
