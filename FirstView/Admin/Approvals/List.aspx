<%@ Page Title="Approval List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.Approvals.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }
        function ShowPopupApprove() {
            $('#myModalApprove').modal('show');
        }
        function ShowPopupReject() {
            $('#myModalReject').modal('show');
        }
        function ShowPopupReset() {
            $('#myModalReset').modal('show');
        }
        function ConfirmDelete() {
            if (confirm("Are you sure you want to delete this item?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmApprove() {
            if (confirm("Are you sure you want to approve this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmReject() {
            if (confirm("Are you sure you want to reject this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmReset() {
            if (confirm("Are you sure you want to reset this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        $(document).ready(function () {
            $('#MainContent_txtSearchPieceName').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#MainContent_butSearch').click();
                    return false;
                }
            });
        });
        $(document).ready(function () {
            $('#MainContent_txtSearchNote').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#MainContent_butSearch').click();
                    return false;
                }
            });
        });
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Approvals</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>
            </p>
        </div>
        <div class="panel panel-default">
          <div class="panel-heading">Search Filters</div>
          <div class="panel-body">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Artist"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="ddlArtist" runat="server" width="420px" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArtist_SelectedIndexChanged"></asp:DropDownList>                
                </div>
            </div>
            <br />
           <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Search Approval Status"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlApprovalStatus" runat="server" width="350px" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalStatus_SelectedIndexChanged"></asp:DropDownList>                
                </div>
                <div>
                    <button type="button" id="butSearch" class="btn btn-primary" runat="server" style="width:80px" OnServerClick="butSearch_ServerClick">
                      <span class="glyphicon glyphicon-search"></span> Search
                    </button>
                    <button type="button" id="butClearSearch" class="btn btn-default" runat="server" style="width:80px" OnServerClick="butClearSearch_ServerClick">
                      <span class="glyphicon glyphicon-repeat"></span> Clear
                    </button>
                </div>
            </div>
          </div>
        </div>
        <br />
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvApprovals" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvApprovals_RowDataBound" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ArtistID,ApprovalStatus,ApprovedDate" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvApprovals_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Artist Name" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="Surname" HeaderText="Artist Surname" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                            </ItemTemplate>                                
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Approval Status" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblApproved" runat="server" Text=""></asp:Label>
                            </ItemTemplate>                                
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Preview" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                            <ItemTemplate>
                                <asp:HyperLink ID="butPreview" runat="server" CssClass="btn btn-default" Width="80px">Preview</asp:HyperLink>
                            </ItemTemplate>                                
                        </asp:TemplateField>                                                                   
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblStatus" runat="server" CssClass="text-danger" Text="" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                            The item has been deleted successfully.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalApprove" class="modal fade">
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
                            The artist page has been approved.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalReject" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-danger"><span class="glyphicon glyphicon-ok"></span>
                            The artist page has been rejected.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalReset" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-info"><span class="glyphicon glyphicon-ok"></span>
                            The artist page has been reset.</p>
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
