<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditReport.aspx.cs" Inherits="FirstView.Admin.ArtistWork.AuditReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .row marginTop div {
            text-align: right;
            padding-top: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h4>Artist Audit Report</h4>
    </div>
    <div class="row">
        <div>
            <div class="row marginTop">
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox ID="txtFromDate" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox ID="txtToDate" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <asp:Button ID="buttonBack" class="btn btn-default" Width="80px" runat="server" CausesValidation="false" Text="Back" />
                    <asp:Button ID="buttonExport" class="btn btn-primary" Width="80px" runat="server" Text="Export" />
                    <asp:Button ID="buttonReport" class="btn btn-default" Width="80px" runat="server" CausesValidation="false" Text="Report" />
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvUsers" runat="server" Width="100%" HorizontalAlign="Left" AutoGenerateColumns="false" AllowPaging="true"
                        CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvUsers_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="WorkName" HeaderText="WorkName" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                            <asp:BoundField DataField="OriginalFileName" HeaderText="File Name" ItemStyle-Width="15%" HeaderStyle-Width="15%" />                            
                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                            <asp:BoundField DataField="Height" HeaderText="Height" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
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
                    <asp:Label ID="lblStatus" runat="server" CssClass="text-danger" Text="" Visible="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
