<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="FirstView.Admin.MenuManagement.MenuList" %>

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
                        Menu Category
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="ddlMenuCategory" OnSelectedIndexChanged="ddlMenuCategory_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Public"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Artist"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Admin"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlMenuCategory" ControlToValidate="ddlMenuCategory" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" InitialValue="0" ValidationGroup="A" ErrorMessage="Menu Category is a required field"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2">
                        Menu
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="ddlMenu" runat="server">
                            <asp:ListItem Value="0" Text="--All--"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12">
                        <button type="button" id="butSearch" class="btn btn-primary" runat="server" validationgroup="A" style="width: 80px" onserverclick="butSearch_ServerClick">
                            <span class="glyphicon glyphicon-search"></span>Search
                        </button>
                        <button type="button" id="butClearSearch" class="btn btn-default" runat="server" style="width: 80px" onserverclick="butClearSearch_ServerClick">
                            <span class="glyphicon glyphicon-repeat"></span>Clear
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="gvMenu" runat="server" Width="100%" HorizontalAlign="Left" OnRowCommand="gvMenu_RowCommand" AutoGenerateColumns="false" OnRowDataBound="gvMenu_RowDataBound"
            DataKeyNames="Id" CssClass="table table-bordered table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="ParentMenu" HeaderText="Parent Menu Title" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                <asp:BoundField DataField="MenuTitle" HeaderText="Menu Title" ItemStyle-Width="25%" HeaderStyle-Width="25%" />
                <asp:BoundField DataField="Url" HeaderText="Url" ItemStyle-Width="25%" HeaderStyle-Width="25%" />
                <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
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
                            Menu deleted successfully.
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
