<%@ Page Title="User List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.Users.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup(msg) {
            $("#<%=lblMessage.ClientID%>").text(msg);
            $("#overlay").hide();
            $('#myModal').modal('show');
        }
        function ConfirmDelete(deleteMessage) {
            if (confirm(deleteMessage) == true) {
                $("#overlay").show();
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Users</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <a href="Add.aspx" id="AddNew" runat="server" class="btn btn-primary" style="width: 80px" role="button">Add</a>
            </p>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Search Filters</div>
            <div class="panel-body">
                <div class="row marginTop">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Deleted?"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <asp:RadioButton ID="radArtistAll" Text="All" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="radArtistAll_CheckedChanged" />
                        <asp:RadioButton ID="radArtistActive" Text="Active" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" Checked="true" OnCheckedChanged="radArtistActive_CheckedChanged" />
                        <asp:RadioButton ID="radArtistDeleted" Text="Deleted" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="radArtistDeleted_CheckedChanged" />
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="Search Name"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearchName" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
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
                <asp:GridView ID="gvUsers" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvUsers_RowDataBound"
                    OnRowCommand="gvUsers_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="UserID,IsActive,IsAdmin,IsArtist,IsVerified" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvUsers_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="Email" HeaderText="Email Address" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Is Active?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsActive" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Admin?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsAdmin" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Artist?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsArtist" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Verified?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsVerified" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hypEdit" CssClass="btn btn-info" Width="80px" runat="server">Edit</asp:HyperLink>
                               
                                <asp:Button ID="butDelete" CssClass="btn btn-info" runat="server" Width="80px" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reset Password" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:HyperLink ID="butResetPass" runat="server" CssClass="btn btn-warning" Width="120px">Reset Password</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
