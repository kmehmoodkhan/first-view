<%@ Page Title="Artist Work List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.ArtistWork.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup(msg) {
            $("#overlay").hide();
            $("#<%=lblMessage.ClientID%>").text(msg);
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
        $(document).ready(function () {
            $('#MainContent_txtSearchWorkName').keypress(function (e) {
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
        function UpdateArtistWorkExhibition(elem) {
            debugger;
            var id = elem.children[1].id;
            var hdnfArtistWorkID = id.replace('chkExhibitionParticipation', 'hdnfArtistWorkID');
            var artistWorkId = $('#' + hdnfArtistWorkID).val();
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/UpdateArtistWorkExhibitionNo",
                data: JSON.stringify({ ArtistWorkId: artistWorkId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger;
                    if (r.d != undefined && r.d != '') {
                        var objJson = jQuery.parseJSON(r.d);
                        //if (parseInt(objJson) > 0) {
                        //    alert('Artist Work added to current exhibition')
                        //}
                        //if (parseInt(objJson) == 0) {
                        //    alert('Artist Work removed from current exhibition')
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
            <h4>Administration - Manage Artist Work</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <a href="Add.aspx" id="AddNew" runat="server" class="btn btn-primary" style="width: 80px" role="button">Add</a>
            </p>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Search Filters</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="Deleted?"></asp:Label>
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
                        <asp:Label ID="Label1" runat="server" Text="Artist"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ddlArtist" runat="server" Width="420px" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArtist_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label4" runat="server" Text="Search Work Name"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearchWorkName" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="Search Note"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearchNote" CssClass="form-control" Width="420px" runat="server"></asp:TextBox>
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
        <br />
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvArtistWorks" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvArtistWorks_RowDataBound"
                    OnRowCommand="gvArtistWorks_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ArtistWorkID,ArtistID,ImageFileName,IsDeleted,ExhibitionNo,IsAllowed" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvArtistWorks_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Artist Name" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <asp:BoundField DataField="Surname" HeaderText="Artist Surname" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <asp:BoundField DataField="WorkName" HeaderText="Work Name" ItemStyle-Width="15%" HeaderStyle-Width="15%" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Deleted?" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblIsDeleted" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval Status" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblApprovalStatus" runat="server" Text="Label"></asp:Label>
                                <asp:HiddenField ID="hdnfApprovalStatus" runat="server" Value='<%#Eval("ApprovalStatus") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                                <asp:HyperLink ID="hypEdit" Width="80px" CssClass="btn btn-info" runat="server">Edit</asp:HyperLink>
                                <br />
                                <br />
                                <asp:Button ID="butDelete" Width="80px" CssClass="btn btn-info" runat="server" Text="Delete" ToolTip="Delete" CommandName="deleteRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Preview" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:HyperLink ID="butPreview" runat="server" CssClass="btn btn-default" Width="80px">Preview</asp:HyperLink>
                                <div class="marginTop">
                                    <asp:CheckBox ID="chkExhibitionParticipation" runat="server" Text="Exhibition" onchange="UpdateArtistWorkExhibition(this);" TextAlign="Left" Font-Bold="false" />
                                    <asp:HiddenField ID="hdnfArtistWorkID" runat="server" Value='<%#Eval("ArtistWorkID") %>' />
                                </div>
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
