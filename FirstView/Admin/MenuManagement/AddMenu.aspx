<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMenu.aspx.cs" Inherits="FirstView.Admin.MenuManagement.AddMenu" %>

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
                window.location.href = "MenuList.aspx";
            }
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Menu - Add</h4>
            <p class="lead">
                <a href="MenuList.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="btnSave" class="btn btn-primary" ValidationGroup="save" runat="server" Width="80px" Text="Save" OnClick="btnSave_Click" />
            </p>
        </div>
        <asp:UpdatePanel ID="upnlParentMenu" runat="server">
            <ContentTemplate>
                <div class="row marginTop">
                    <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                        Menu Category
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                        <asp:DropDownList class="form-control" ID="ddlMenuCategory" OnSelectedIndexChanged="ddlMenuCategory_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Public"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Artist"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Admin"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlMenuCategory" ValidationGroup="save" ControlToValidate="ddlMenuCategory" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" InitialValue="0" ErrorMessage="Menu Category is a required field"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2">
                        Parent Menu
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="ddlMenu" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                        Menu Title
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                        <asp:TextBox ID="txtMenuTitle" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtMenuTitle" ValidationGroup="save" ControlToValidate="txtMenuTitle" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                        Sort Order
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                        <asp:TextBox ID="txtSortOrder" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtSortOrder" ValidationGroup="save" ControlToValidate="txtSortOrder" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Sort order is a required field"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-2">
                        Linked Page
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList class="form-control" ID="ddlPages" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlPages" ValidationGroup="save" ControlToValidate="ddlPages" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" InitialValue="--Select--" ErrorMessage="A page must be linked to menu."></asp:RequiredFieldValidator>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlMenuCategory" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
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
