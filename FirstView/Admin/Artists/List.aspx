<%@ Page Title="Artist List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.Artists.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup(msg) {
            $("#<%=lblMessage.ClientID%>").text(msg);
            $("#overlay").hide();
            $('#myModal').modal('show');

        }
        function ConfirmDelete(deleteMessage) {
            if (confirm(deleteMessage) == true) {
                return true;
                $("#overlay").show();
            }
            else {
                return false;
            }
        }
        $(document).ready(function () {
            $('#MainContent_txtSearchName').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#MainContent_butSearch').click();
                    return false;
                }
            });
        });
        function AddArtistToExhibition(elem) {
            debugger;
            var id = elem.children[1].id;
            var hdnfArtistWorkID = id.replace('chkExhibitionParticipation', 'hdnfArtistID');
            var artistId = $('#' + hdnfArtistWorkID).val();
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/AddArtistToExhibitionNo",
                data: JSON.stringify({ ArtistId: artistId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger;
                    if (r.d != undefined && r.d != '') {
                        var objJson = jQuery.parseJSON(r.d);
                        //if (parseInt(objJson) > 0) {
                        //    alert('Artist added to current exhibition')
                        //}
                        //if (parseInt(objJson) == 0) {
                        //    alert('Artist removed from current exhibition')
                        //}
                    }
                },
                error: function (r) {
                    console.log('error=' + r.responseText);
                },
                failure: function (r) {
                    console.log('failure=' + r.responseText);
                }
            });
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Manage Artists</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <a href="Add.aspx" runat="server" class="btn btn-primary" style="width: 80px" role="button">Add</a>
            </p>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Search Filters</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Deleted?"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <asp:RadioButton ID="radArtistAll" Text="All" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="radArtistAll_CheckedChanged" />
                        <asp:RadioButton ID="radArtistActive" Text="Active" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" Checked="true" OnCheckedChanged="radArtistActive_CheckedChanged" />
                        <asp:RadioButton ID="radArtistDeleted" Text="Deleted" runat="server" CssClass="radio-inline" GroupName="radArtist" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="radArtistDeleted_CheckedChanged" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="Artist Type"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlArtistType" CssClass="form-control" runat="server" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ddlArtistType_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
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
                <asp:GridView ID="gvArtists" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvArtists_RowDataBound"
                    OnRowCommand="gvArtists_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ArtistID,ImageFileName,IsDeleted,ArtistType,ExhibitionNo" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvArtists_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <%--<asp:BoundField DataField="CV" HeaderText="CV" ItemStyle-Width="20%" HeaderStyle-Width="20%"/>--%>
                        <asp:BoundField DataField="ArtistType" HeaderText="Artist Type" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Photo" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="imgArtistPhoto" runat="server" CssClass="img-thumbnail" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deleted?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsDeleted" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval Status" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblApprovalStatus" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnfApprovalStatus" runat="server" Value='<%#Eval("ApprovalStatus") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hypEdit" CssClass="btn btn-info" Width="80px" runat="server">Edit</asp:HyperLink>
                                <asp:Button ID="butDelete" CssClass="btn btn-info" runat="server" Width="80px" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>
                                <div class="marginTop">
                                    <asp:HyperLink NavigateUrl='<%# Eval("ArtistID", "~/ArtistWork/ExhibitionForm.aspx?artistid={0}")%>' runat="server" Text="Exhibition Form"></asp:HyperLink>
                                </div>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Preview" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:HyperLink ID="butPreview" runat="server" CssClass="btn btn-default" Width="80px">Preview</asp:HyperLink>
                                <div class="marginTop">
                                    <asp:CheckBox ID="chkExhibitionParticipation" runat="server" Text="Exhibition" onchange="AddArtistToExhibition(this);" TextAlign="Left" Font-Bold="false" />
                                    <asp:HiddenField ID="hdnfArtistID" runat="server" Value='<%#Eval("ArtistID") %>' />

                                </div>
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
