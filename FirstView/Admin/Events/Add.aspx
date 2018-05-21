<%@ Page Title="Events Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FirstView.Admin.Events.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/dropzone.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalNoImage() {
            $('#myModalNoImage').modal('show');
        }
        function openModalError(msg) {
            $("#dvErrorMessage").text(msg);
            $('#dvModalError').modal('show');
        }
        $(document).ready(function () {
            $("#txtStartDate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#txtEndDate").datepicker({ dateFormat: 'dd/mm/yy' });
            BindEditEventDetails();
        });
        function BindEditEventDetails() {
            var eventId = "<%=Convert.ToString(Request.QueryString["EventId"])%>";
            var IsCopy = "<%=Convert.ToString(Request.QueryString["isCopy"])%>";
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
                        if (r.d != undefined && r.d != '') {
                            var objJson = jQuery.parseJSON(r.d);
                            var img = '<img class="marginTop img-thumbnail responsive" src="../../Uploads/Thumbnails/' + objJson[0].NewFileName + '">'
                            
                            $("#txtStartDate").val(objJson[0].StartDate);
                            $("#txtEndDate").val(objJson[0].EndDate);
                            $("#ddlStartTime").val(objJson[0].StartTimeId);
                            $("#ddlEndTime").val(objJson[0].EndTimeId);
                            $("#txtArtistGroup").val(objJson[0].ArtistGroup);
                            if (IsCopy == "true") {
                                $("#txtTitle").val("Copy of " + objJson[0].Title);
                            } else {
                                $("#txtTitle").val(objJson[0].Title);
                            }
                            $("#MainContent_hidUniqueID").val(objJson[0].NewFileName);
                            $("#dvImage").html(img);
                            $("#txtSummary").html(objJson[0].Summary);
                            $("#txtDetails").html(objJson[0].EventDetails);
                            $("#ddlIsExhibition").val(objJson[0].IsExhibition);
                            $("#txtExhibitionNo").val(objJson[0].ExhibitionNo);
                            $("#ddlCurrent").val(objJson[0].IsCurrent);
                            showhide(objJson[0].IsExhibition);
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
        }
        function openCalendar(targetID) {
            $("#" + targetID).focus();
        }
        function SaveEvents() {
            debugger;
            var StartDate = $("#txtStartDate").val();
            var EndDate = $("#txtEndDate").val();
            var StartTimeId = $("#ddlStartTime").val();
            var EndTimeId = $("#ddlEndTime").val();

            var ArtistGroup = $("#txtArtistGroup").val();
            var Title = $("#txtTitle").val();

            var Summary = $("#txtSummary").val();
            var EventDetails = $("#txtDetails").val();
            var UniqueID = $("#MainContent_hidUniqueID").val();
            var eventId = "<%=Convert.ToString(Request.QueryString["EventId"])%>";
            var IsCopy = "<%=Convert.ToString(Request.QueryString["isCopy"])%>";
            var IsExhibition = $("#ddlIsExhibition").val();
            var IsCurrent = $("#ddlCurrent").val();
            var ExhibitionNo = $("#txtExhibitionNo").val();
            eventId = IsCopy == "true" ? 0 : eventId;

            if (StartDate != undefined && StartDate != null && StartDate != '') {
                if (Title != undefined && Title != null && Title != '') {
                    var oenEvent = {
                        "EventId": eventId == null ? 0 : eventId == '' ? 0 : eventId,
                        "StartDate": StartDate == '' ? null : StartDate,
                        "EndDate": EndDate == '' ? null : EndDate,
                        "StartTimeId": StartTimeId == '' ? 30 : StartTimeId,
                        "EndTimeId": EndTimeId == '' ? 30 : EndTimeId,
                        "ArtistGroup": ArtistGroup == '' ? null : ArtistGroup,
                        "Title": Title == '' ? null : Title,
                        "Summary": Summary,
                        "UserId":0,
                        "EventDetails": EventDetails,
                        "IsExhibition": IsExhibition,
                        "IsCurrent": IsCurrent,
                        "ExhibitionNo": ExhibitionNo == '' ? 0:ExhibitionNo,
                        "UniqueID": UniqueID,
                        "numericStatus":0
                    };
                    $.ajax({
                        type: "POST",
                        url: "/FirstViewService.asmx/SaveEvents",
                        data: JSON.stringify({ objEvent: oenEvent }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {

                            if (r.d.indexOf("success") > -1) {
                                $('#myModal').modal('show');
                                setTimeout(function () { window.location.href = "/admin/events/list.aspx?eventId=" + eventId; }, 2000);

                            }
                            else if (r.d == '"-1"')
                            {
                                openModalError("Exhibition number already exists.")
                            }
                            else if (r.d.indexOf("failed") > -1) {
                                openModalError("some error occurred.")
                            } else if (r.d.indexOf("required") > -1) {
                                openModalError("Start Date and Title are mandatory fields..")
                            } else if (r.d.indexOf("unauthorized") > -1) {
                                openModalError("Session Expired.")
                            }
                        },
                        error: function (r) {
                            console.log('error=' + r.responseText);
                        },
                        failure: function (r) {
                            console.log('failure=' + r.responseText);
                        }
                    });
                } else {
                    alert('Start Date and Title are mandatory.')
                }
            } else {
                alert('Start Date and Title are mandatory.')
            }
        }
        function ShowHideIsCurrent() {
            var IsExhibition = $("#ddlIsExhibition").val();
            showhide(IsExhibition);
        }
        function showhide(IsExhibition) {
            if (IsExhibition == '1') {
                $("#dvCurrent").show();
                $("#dvExhibition").show();
            } else {
                $("#dvCurrent").hide();
                $("#dvExhibition").hide();
            }
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Event - Add</h4>
            <p class="lead">
                <a href="list.aspx" id="btnBack" runat="server" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <button type="button" class="btn btn-primary" onclick="SaveEvents();">
                    <span class="glyphicon glyphicon-save"></span>Save</button>
            </p>
        </div>
        <div class="row">
            <div class="col-md-2 col-lg-2 col-sm-4 col-xs-12 marginTop align-middle">
                <label for="subject">Is Exhibition</label>
                <div class="input-group">
                    <select id="ddlIsExhibition" onchange="ShowHideIsCurrent();" class="form-control">
                        <option value="0" selected>No</option>
                        <option value="1">Yes</option>
                    </select>
                </div>
            </div>

            <div class="col-md-2 col-lg-2 col-sm-4 col-xs-12 marginTop align-middle" id="dvExhibition" style="display: none;">
                <label for="subject">Exhibition No</label>
                <div class="input-group">
                    <input type="text" name="ExhibitionNo" title="Exhibition No" class="form-control" role="textbox" id="txtExhibitionNo">
                </div>
            </div>
            <div class="col-md-2 col-lg-2 col-sm-4 col-xs-12 marginTop align-middle" id="dvCurrent" style="display: none;">
                <label for="subject">Is Current</label>
                <div class="input-group">
                    <select id="ddlCurrent" class="form-control">
                        <option value="1">Yes</option>
                        <option value="0" selected="selected">No</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-lg-2 col-sm-5 col-xs-12 marginTop align-middle">
                <label for="subject">Start Date</label>
                <div class="input-group">
                    <input type="text" name="StartDate" readonly="readonly" title="Start Date" class="form-control" role="textbox" id="txtStartDate">
                    <span class="input-group-addon">
                        <img src="../../Images/calendar.png" onclick="openCalendar('txtStartDate');" /></span>
                </div>
            </div>
            <div class="col-md-2 col-lg-2 col-sm-5 col-xs-12 marginTop align-middle">
                <label for="subject">Start Time</label>
                <div class="input-group">
                    <select id="ddlStartTime" class="form-control">
                        <option value="30">select</option>
                        <option value="1">8:00 AM</option>
                        <option value="2">8:30 AM</option>
                        <option value="3">9:00 AM</option>
                        <option value="4">9:30 AM</option>
                        <option value="5">10:00 AM</option>
                        <option value="6">10:30 AM</option>
                        <option value="7">11:00 AM</option>
                        <option value="8">11:30 AM</option>
                        <option value="9">12:00 PM</option>
                        <option value="10">12:30 PM</option>
                        <option value="11">1:00 PM</option>
                        <option value="12">1:30 PM</option>
                        <option value="13">2:00 PM</option>
                        <option value="14">2:30 PM</option>
                        <option value="15">3:00 PM</option>
                        <option value="16">3:30 PM</option>
                        <option value="17">4:00 PM</option>
                        <option value="18">4:30 PM</option>
                        <option value="19">5:00 PM</option>
                        <option value="20">5:30 PM</option>
                        <option value="21">6:00 PM</option>
                        <option value="22">6:30 PM</option>
                        <option value="23">7:00 PM</option>
                        <option value="24">7:30 PM</option>
                        <option value="25">8:00 PM</option>
                        <option value="26">8:30 PM</option>
                        <option value="27">9:00 PM</option>
                        <option value="28">9:30 PM</option>
                        <option value="29">10:00 PM</option>
                    </select>
                </div>
            </div>
            <div class="col-md-2 col-lg-2 col-sm-5 col-xs-12 marginTop">
                <label for="subject">End Date</label>

                <div class="input-group">
                    <input type="text" name="EndDate" title="End Date" readonly="readonly" class="form-control" role="textbox" id="txtEndDate">
                    <span class="input-group-addon">
                        <img src="../../Images/calendar.png" onclick="openCalendar('txtEndDate');" /></span>
                </div>
            </div>
            <div class="col-md-2 col-lg-2 col-sm-5 col-xs-12 marginTop">
                <label for="subject">End Time</label>
                <div class="input-group">
                    <select id="ddlEndTime" class="form-control">
                        <option value="30">select</option>
                        <option value="1">8:00 AM</option>
                        <option value="2">8:30 AM</option>
                        <option value="3">9:00 AM</option>
                        <option value="4">9:30 AM</option>
                        <option value="5">10:00 AM</option>
                        <option value="6">10:30 AM</option>
                        <option value="7">11:00 AM</option>
                        <option value="8">11:30 AM</option>
                        <option value="9">12:00 PM</option>
                        <option value="10">12:30 PM</option>
                        <option value="11">1:00 PM</option>
                        <option value="12">1:30 PM</option>
                        <option value="13">2:00 PM</option>
                        <option value="14">2:30 PM</option>
                        <option value="15">3:00 PM</option>
                        <option value="16">3:30 PM</option>
                        <option value="17">4:00 PM</option>
                        <option value="18">4:30 PM</option>
                        <option value="19">5:00 PM</option>
                        <option value="20">5:30 PM</option>
                        <option value="21">6:00 PM</option>
                        <option value="22">6:30 PM</option>
                        <option value="23">7:00 PM</option>
                        <option value="24">7:30 PM</option>
                        <option value="25">8:00 PM</option>
                        <option value="26">8:30 PM</option>
                        <option value="27">9:00 PM</option>
                        <option value="28">9:30 PM</option>
                        <option value="29">10:00 PM</option>
                    </select>
                </div>
            </div>
            <div class="col-md-4 col-lg-4 col-sm-6 col-xs-12 marginTop">
                <label for="subject">Artist or Group</label>
                <input type="text" name="artistGroup" title="Artist Group" class="form-control" role="textbox" id="txtArtistGroup">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-lg-12 col-sm-6 col-xs-12 marginTop">
                <label for="subject">Title</label>
                <input type="text" name="eventTitle" title="Event Title" class="form-control" role="textbox" id="txtTitle">
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 col-lg-8 col-sm-6 col-xs-12 marginTop">
                <label for="subject">Summary</label>
                <textarea rows="3" id="txtSummary" class="form-control" cols="50"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 col-lg-8 col-sm-6 col-xs-12 marginTop">
                <label for="subject">Details</label>
                <textarea rows="10" id="txtDetails" class="form-control" cols="50"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-lg-6 col-sm-12 col-xs-12 marginTop">
                <label for="subject">Image</label>
                <iframe id="ifrmUpload" runat="server" width="400" height="200" style="border: none;"></iframe>
            </div>
            <div class="col-md-4 col-lg-6 col-sm-12 col-xs-12 marginTop" id="dvImage">
            </div>
        </div>
        <asp:HiddenField ID="hidUniqueID" runat="server" />
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
                        <span class="glyphicon glyphicon-ok"></span>The data has been saved successfully.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalNoImage" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger">
                        <span class="glyphicon glyphicon-remove"></span>&nbsp;
                        Please select a valid image file to upload.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
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
</asp:Content>
