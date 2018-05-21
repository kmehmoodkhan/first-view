<%@ Page Title="Events" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="FirstView.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ShowEvents();
        });
        function PreviewEvent(eventId) {
            if (eventId != null && eventId > 0) {
                $('#myModalEvent').modal('show');
                debugger;
                var oenEvent = {
                    "EventId": eventId == null ? 0 : eventId == '' ? 0 : eventId
                };
                $.ajax({
                    type: "POST",
                    url: "/FirstViewService.asmx/PublicAccessEvents",
                    data: JSON.stringify({ objEvent: oenEvent }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        if (r.d != undefined && r.d != '') {
                            var objJson = jQuery.parseJSON(r.d);
                            var img = '/Uploads/Resized/' + objJson[0].NewFileName
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
                        console.log('error=' + r.responseText);
                    },
                    failure: function (r) {
                        console.log('failure=' + r.responseText);
                    }
                });
            }
        }
        function ShowEvents() {
            var oenEvent = {
                "numericStatus": 1
            };
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/PublicAccessEvents",
                data: JSON.stringify({ objEvent: oenEvent }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    if (r.d != undefined && r.d != '') {
                        var objJson = jQuery.parseJSON(r.d);
                        for (var i = 0; i < objJson.length; i++) {
                            var endDateTxt = " - " + objJson[i].EndDateText;
                            var artistGroup = " - " + objJson[i].ArtistGroup;
                            var title = " - " + objJson[i].Title;
                            var belowTitle = "<br />" + objJson[i].StartDateText + endDateTxt + artistGroup + title;
                            var elem = $("#dvEvents").html();
                            elem = elem+ '<div class="panel panel-primary">';
                            elem = elem + '<div class="panel-heading">' + belowTitle + '</div>';
                            elem = elem + '<div class="panel-body">' + objJson[i].Summary + ' <a href="#" onclick="PreviewEvent(' + objJson[i].EventId + ');">More Info</a></div>';
                            elem = elem + '</div>';
                            $("#dvEvents").html(elem);
                        }
                    } else {
                        $("#dvNoRecord").show();
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
            <h4>Events</h4>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                The Gallery hosts many different events through the year.
                Some, held in The Studio, are hosted by people or organsisations who have hired the space for their own event.
                <br />
                Information First-View 01747 840747 or <a href="ContactUs.aspx">Contact Us</a>
            </div>
        </div>
        <br />
        <div id="dvEvents">
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
</asp:Content>
