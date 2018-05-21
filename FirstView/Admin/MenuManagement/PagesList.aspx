<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagesList.aspx.cs" Inherits="FirstView.Admin.MenuManagement.PagesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Pages</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="buttonAdd" runat="server" Text="Add" class="btn btn-primary" Style="width: 80px" OnClick="buttonAdd_Click" />
            </p>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Search Filters</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        Page Title
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtPageTitle" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <button type="button" id="butSearch" class="btn btn-primary" runat="server" style="width: 80px" onserverclick="butSearch_ServerClick">
                            <span class="glyphicon glyphicon-search"></span>Search
                        </button>
                        <button type="button" id="butClearSearch" class="btn btn-default" runat="server" style="width: 80px" onserverclick="butClearSearch_ServerClick">
                            <span class="glyphicon glyphicon-repeat"></span>Clear
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvPageUrls" runat="server" Width="100%" HorizontalAlign="Left" OnRowCommand="gvPageUrls_RowCommand" AutoGenerateColumns="false" OnRowDataBound="gvPageUrls_RowDataBound"
                    DataKeyNames="Id" CssClass="table table-bordered table-striped table-hover">
                    <Columns>
                        <asp:BoundField DataField="PageTitle" HeaderText="Title" ItemStyle-Width="40%" HeaderStyle-Width="40%" />
                        <asp:BoundField DataField="PageUrl" HeaderText="Url" ItemStyle-Width="40%" HeaderStyle-Width="40%" />
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button ID="butEdit" CssClass="btn btn-info" runat="server" Width="80px" Text="Edit" ToolTip="Edit" CommandName="editRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>
                                <asp:Button ID="butDelete" CssClass="btn btn-info" runat="server" Width="80px" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblStatus" runat="server" CssClass="text-danger" Visible="false" Text=""></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                            Page deleted successfully.
                        </p>
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
