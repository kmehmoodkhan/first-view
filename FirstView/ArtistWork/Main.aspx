<%@ Page Title="Artist Work List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" EnableEventValidation="true" Inherits="FirstView.ArtistWork.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }

        function ConfirmDelete() {
            if (confirm("Are you sure you want to delete this item?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Manage My Work</h4>
            <p class="lead">
                <a href="../Artist/Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <a href="Add.aspx" id="AddNew" runat="server" class="btn btn-primary" style="width: 80px" role="button">Add</a>
                <asp:Button ID="butPreview" class="btn btn-default" runat="server" Width="80px" Text="Preview" OnClick="butPreview_Click" />
            </p>
        </div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvArtistWorks" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvArtistWorks_RowDataBound"
                    OnRowCommand="gvArtistWorks_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ArtistWorkID,ArtistID,ImageFileName,IsDeleted,Width,Height,ExhibitionNo" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvArtistWorks_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="WorkName" HeaderText="Work Name" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="Medium" HeaderText="Medium" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="Price" HeaderText="Price (&pound;)" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Size (WxH)" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Image" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="imgArtistWork" runat="server" CssClass="img-thumbnail" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hypEdit" CssClass="btn btn-info" Width="80px" runat="server">Edit</asp:HyperLink>
                                <asp:Button ID="butDelete" CssClass="btn btn-info marginTop" runat="server" Width="80px" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>
                                <asp:Panel ID="pnlExhibtion" runat="server">
                                    <div class="marginTop" style="border: 1px dashed black;">
                                        <asp:Label ID="lblExbhition" Text="Exbn No-" runat="server"></asp:Label>
                                        <asp:Label ID="lblExhibitionNo" Width="25px" BackColor="White" ForeColor="Black" Text="Exbn No-" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblStatus" CssClass="text-danger" runat="server" Text="" Visible="false"></asp:Label>
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
                            The item has been deleted successfully.
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
