<%@ Page Title="Artist Types List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.ArtistType.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopupDeleteSuccess() {
            $("#overlay").hide();
            $('#myModalDeleteSuccess').modal('show');
        }
        function ShowPopupDeleteError() {
            $("#overlay").hide();
            $('#myModalDeleteError').modal('show');
        }        
        function ConfirmDelete() {
            if (confirm("Are you sure you want to delete this item?") == true) {
                $("#overlay").show();
                return true;
            }
            else {
                return false;
            }
        }
        $(document).ready(function () {
            $('#MainContent_txtSearchArtistType').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#MainContent_butSearch').click();
                    return false;
                }
            });
        });
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Artist Types</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>
                <a href="Add.aspx" id="AddNew" runat="server" class="btn btn-primary"  style="width:80px" role="button">Add</a>                
            </p>
        </div>
        <div class="panel panel-default">
          <div class="panel-heading">Search Filters</div>
          <div class="panel-body">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Search Artist Type"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearchArtistType" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
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
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
            <asp:GridView ID="gvArtistTypes" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvArtistTypes_RowDataBound"
                OnRowCommand="gvArtistTypes_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                DataKeyNames="ArtistTypeID" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvArtistTypes_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="80%" HeaderStyle-Width="80%"/>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="hypEdit" CssClass="btn btn-info" Width="80px" runat="server">Edit</asp:HyperLink>
                        
                            <asp:Button ID="butDelete" CssClass="btn btn-info" runat="server" Width="80px" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false">                        
                            </asp:Button>                    
                        </ItemTemplate>                                
                    </asp:TemplateField>                                                 
                </Columns>
            </asp:GridView>
                <asp:Label ID="lblStatus" runat="server" CssClass="text-danger" Visible="false" Text=""></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="myModalDeleteSuccess" class="modal fade">
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
                            The Artist Type has been deleted successfully.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div> 
        <div id="myModalDeleteError" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Error</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The Artist Type is currently in use and cannot be deleted. First remove all references to this Artist Type and try again.</p>                    
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
