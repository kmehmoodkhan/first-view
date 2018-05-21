<%@ Page Title="Events List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FirstView.Admin.Events.List" %>

<asp:Content ContentPlaceHolderID="HeaderContent" ID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        $(document).ready(function () {
            $("#txtStartDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#txtEndDate").datepicker({ dateFormat: 'dd/mm/yy' });

            ShowEvents();
            var eventId = "<%=Convert.ToString(Request.QueryString["EventId"])%>";
            if (eventId != null && eventId > 0) {
                //PreviewEvent(eventId);
            }
        });
        function openModalWarning(type) {
            var msg = "";
            if (type == 1) {
                msg = "You can not edit a deleted record!";
            } else if (type == 2) {
                msg = "You can not deleted an already deleted record!";
            }
            $("#dvErrorMessage").text(msg);
            $('#dvModalError').modal('show');
        }
        function PreviewEvent(eventId) {
            $("#overlay").show();
            if (eventId != null && eventId > 0) {
                var oenEvent = {
                    "EventId": eventId == null ? 0 : eventId == '' ? 0 : eventId
                };
                $.ajax({
                    type: "POST",
                    url: "/FirstViewService.asmx/SearchEventList",
                    data: JSON.stringify({ objEvent: oenEvent }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $("#overlay").hide();
                        if (r.d != undefined && r.d != '') {
                            var objJson = jQuery.parseJSON(r.d);
                            var img = '../../Uploads/Resized/' + objJson[0].NewFileName
                            var endDateTxt = " - " + objJson[0].EndDateText;
                            var artistGroup = " - " + objJson[0].ArtistGroup;
                            var title = " - " + objJson[0].Title;
                            var belowTitle = "<br />" + objJson[0].StartDateText + endDateTxt + artistGroup + title;
                            $("#modalhdnTitle").html(belowTitle);
                            $("#dvBelowTitle").html(objJson[0].Summary);
                            $("#modalbodyeventDetail").html(objJson[0].EventDetails);
                            $("#modalEventImage").attr("src", img);
                            $('#myModalEvent').modal('show');
                        }
                    },
                    error: function (r) {
                        $("#overlay").hide();
                        console.log('error=' + r.responseText);
                    },
                    failure: function (r) {
                        $("#overlay").hide();
                        console.log('failure=' + r.responseText);
                    }
                });
            }
        }
        function ConfirmDelete(Id, elem) {
            $("#overlay").show();
            if (confirm("Are you sure you want to delete this item?") == true) {
                $.ajax({
                    type: "POST",
                    url: "/FirstViewService.asmx/DeleteEvent",
                    data: JSON.stringify({ Id: Id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $("#overlay").hide();
                        $("#myModal").modal('show');
                        $('#ddlStatus').val("1");
                        ShowEvents();
                    },
                    error: function (r) {
                        $("#overlay").hide();
                        console.log('error=' + r.responseText);
                    },
                    failure: function (r) {
                        $("#overlay").hide();
                        console.log('failure=' + r.responseText);
                    }
                });
            }
            else {
                return false;
            }
        }
        function openCalendar(targetID) {
            $("#" + targetID).focus();
        }
        function openModalEvent() {
            $('#myModalEvent').modal('show');
        }
        function ShowEvents() {
            $("#overlay").show();
          
            var startDate = $("#txtStartDate").val();
            var endDate = $("#txtEndDate").val();
            var artistGroup = $("#txtArtistGroup").val();
            var eventTitle = $("#txtEventTitle").val();
            var status = $('#ddlStatus').val();
            var oenEvent = {
                "StartDate": startDate == '' ? null : startDate,
                "EndDate": endDate == '' ? null : endDate,
                "ArtistGroup": artistGroup == '' ? null : artistGroup,
                "Title": eventTitle == '' ? null : eventTitle,
                "numericStatus": status
            };
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/SearchEventList",
                data: JSON.stringify({ objEvent: oenEvent }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#overlay").hide();
                    var table = $("#tblEvents")[0];
                    for (var i = table.rows.length - 1; i > 0; i--) {
                        table.deleteRow(i);
                    }

                    if (r.d != undefined && r.d != '') {
                        $("#dvNoRecord").hide();
                        var objJson = jQuery.parseJSON(r.d);
                        for (var i = 0; i < objJson.length; i++) {
                            var newrow = table.insertRow(table.rows.length);
                            var cell0 = newrow.insertCell(0);
                            var cell1 = newrow.insertCell(1);
                            var cell2 = newrow.insertCell(2);
                            var cell3 = newrow.insertCell(3);
                            var cell4 = newrow.insertCell(4);
                            var cell5 = newrow.insertCell(5);
                            var cell6 = newrow.insertCell(6);
                            var cell7 = newrow.insertCell(7);
                            var img = '<img class="img-thumbnail responsive" src="../../Uploads/Thumbnails/' + objJson[i].NewFileName + '">'
                            var title = '<div class="dvFake">' + objJson[i].Title + '</div>';
                            var editLink = "";
                            var copyLink = "";
                            var deleteLink = "";
                            if (objJson[i].IsActive.indexOf('Yes') > -1) {
                                editLink = '<a title="Edit" class="btn btn-info link" href="javascript:void(0);" onclick="return openModalWarning(1);">Edit</a>';
                                copyLink = '<a title="Edit" class="marginTop btn btn-info link" href="Add.aspx?EventId=' + objJson[i].EventId + '&isCopy=true">Copy</a>';
                                deleteLink = '<a title="Delete" class="btn btn-info link" href="javascript:void(0);" onclick="return openModalWarning(2);">Delete</a>';
                            } else {
                                editLink = '<a title="Edit" class="btn btn-info link" href="Add.aspx?EventId=' + objJson[i].EventId + '">Edit</a>';
                                deleteLink = '<a title="Delete" class="marginTop btn btn-info link" href="javascript:void(0);" onclick="return ConfirmDelete(' + objJson[i].EventId + ',this);">Delete</a>';
                                copyLink = '<a title="Edit" class="marginTop btn btn-info link" href="Add.aspx?EventId=' + objJson[i].EventId + '&isCopy=true">Copy</a>';
                            }
                            var previewLink = '<a class="btn btn-info link" title="Preview" href="javascript:void(0);" onclick="PreviewEvent(' + objJson[i].EventId + ')">Preview</a>';
                            var exhibitionNo = objJson[i].IsExhibition == '0' || objJson[i].ExhibitionNo == undefined ? '' : objJson[i].ExhibitionNo;
                            console.log('Event-' + objJson[i].IsExhibition);
                            console.log('Is exhibition-' + objJson[i].IsExhibition);
                            console.log('Exhibition No-' + objJson[i].ExhibitionNo);

                            var spnexhibitionNo = '';
                            if (exhibitionNo != '') {
                                spnexhibitionNo = '<div class="marginTop" style="border:1px dashed black;">Exbn No-' + exhibitionNo + '&nbsp;</div>';
                            }
                            console.log('spn Exhibition No-' + spnexhibitionNo);
                            cell0.innerHTML = objJson[i].StartDateText + ',' + objJson[i].StartTime;
                            cell1.innerHTML = objJson[i].EndDateText + ',' + objJson[i].EndTime;
                            cell2.innerHTML = objJson[i].ArtistGroup;
                            cell3.innerHTML = title;
                            cell4.innerHTML = img;
                            cell5.innerHTML = objJson[i].IsActive;
                            cell6.innerHTML = editLink + copyLink + deleteLink;
                            cell7.innerHTML = previewLink + spnexhibitionNo;
                        }
                    } else {
                        $("#overlay").hide();
                        $("#dvNoRecord").show();
                    }
                },
                error: function (r) {
                    $("#overlay").hide();
                    console.log('error=' + r.responseText);
                },
                failure: function (r) {
                    $("#overlay").hide();
                    console.log('failure=' + r.responseText);
                }
            });

        }
        function OnSuccess(response) {
            console.log(response);
            alert(response.d);
        }
    </script>
    <div class="container">
        <div class="page-header">

            <h4>Administration - Manage Events</h4>
            <p class="lead">

                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <a href="Add.aspx" id="AddNew" runat="server" class="btn btn-primary" style="width: 80px" role="button">Add</a>

            </p>

        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Search Filters
                <div style="float: right;">
                </div>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-5 col-xs-12 marginTop">
                        <label for="Status">Status</label>
                        <select class="form-control" onchange="ShowEvents();" id="ddlStatus">
                            <option value="0">All</option>
                            <option value="1" selected>Active</option>
                            <option value="2">Deleted</option>
                        </select>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-5 col-xs-12 marginTop">
                        <label for="StartDate">Start Date</label>
                        <div class="input-group">
                            <input type="text" name="StartDate" readonly="readonly" title="Start Date" class="form-control" role="textbox" id="txtStartDate">
                            <span class="input-group-addon">
                                <img src="../../Images/calendar.png" onclick="openCalendar('txtStartDate');" /></span>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-5 col-xs-12 marginTop">
                        <label for="EndDate">End Date</label>
                        <div class="input-group">
                            <input type="text" name="EndDate" title="End Date" readonly="readonly" class="form-control" role="textbox" id="txtEndDate">
                            <span class="input-group-addon">
                                <img src="../../Images/calendar.png" onclick="openCalendar('txtEndDate');" /></span>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 marginTop">
                        <label for="ArtistGroup">Artist or Group </label>
                        <input type="text" name="artistGroup" title="Artist Group" onkeyup="ShowEvents();" class="form-control" role="textbox" id="txtArtistGroup">
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 marginTop">
                        <label for="EventTitle">Event Title</label>
                        <input type="text" name="eventTitle" title="Event Title" onkeyup="ShowEvents();" class="form-control" role="textbox" id="txtEventTitle">
                    </div>
                </div>
                <div class="row marginTop  pull-right">
                    <div class="marginRight">
                        <button type="button" class="btn btn-primary" onclick="ShowEvents();">
                            <span class="glyphicon glyphicon-search"></span>Search</button>
                        <button type="button" class="btn btn-primary" onclick="Clear();">
                            <span class="glyphicon glyphicon-repeat"></span>Clear  
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="table-responsive">
            <table class="table table-bordered table-striped table-hover" cellspacing="0" align="Left" rules="all" border="1" id="tblEvents" style="width: 100%; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <th scope="col" style="width: 10%;">Start Date</th>
                        <th scope="col" style="width: 10%;">End Date</th>
                        <th scope="col" style="width: 15%;">Artist Group</th>
                        <th scope="col" style="width: 25%;">Event Title</th>
                        <th align="center" scope="col" style="width: 15%;">Event Photo</th>
                        <th align="center" scope="col" style="width: 5%;">Deleted?</th>
                        <th align="center" scope="col" style="width: 5%;">Action</th>
                        <th align="center" scope="col" style="width: 5%;">Preview</th>
                    </tr>
                    <tr>
                        <td style="width: 15%;"></td>
                        <td style="width: 15%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 10%;"></td>
                        <td align="center" style="width: 5%;"></td>
                        <td align="center" style="width: 15%;"></td>
                        <td align="center" style="width: 5%;">
                            <a title="Edit" class="btn btn-info link" href="Edit.aspx?ArtistID=1" style="">Edit</a>
                        </td>
                        <td align="center" valign="top" style="width: 5%;">
                            <input class="btn btn-info link" type="button" value="Delete" href="javascript:void(0);" onclick="return ConfirmDelete();" title="Delete">
                        </td>
                        <td align="center" valign="top" style="width: 5%;">
                            <a class="btn btn-default link" href="javascript:void(0);" onclick="return Preview(this);">Preview</a>
                        </td>
                    </tr>

                </tbody>
            </table>
            <div id="dvNoRecord" style="display: none;">No record found for selected filter criteria.</div>
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
                        <p class="text-success">
                            <span class="glyphicon glyphicon-ok"></span>
                            The event has been deleted successfully.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalEvent" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="modalhdnTitle"></h4>
                        <div id="dvBelowTitle" style="font-style: italic;"></div>
                    </div>
                    <div class="modal-body">
                        <div class="text">
                            <div id="modalbodyeventDetail"></div>
                            For information call First-View 01747 840747 <a href="ContactUs.aspx">Contact Us</a><br />
                            <br />
                            <img id="modalEventImage" class="img-thumbnail responsive" src="">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvModalError" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Error</h4>
                    </div>
                    <div class="modal-body">
                        <div class="text-danger">
                            <span class="glyphicon glyphicon-remove"></span>&nbsp;
                            <div id="dvErrorMessage"></div>
                        </div>
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
