<%@ Page Title="Find Us" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="FindUs.aspx.cs" Inherits="FirstView.FindUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="http://maps.googleapis.com/maps/api/js"></script>
    <script>
    function initialize() {
       var mapProp = {
         center:new google.maps.LatLng(51.104796,-2.320447),
         zoom:16,
         mapTypeId:google.maps.MapTypeId.Map
       };
       var map=new google.maps.Map(document.getElementById("googleMap"),mapProp);
     }
     google.maps.event.addDomListener(window, 'load', initialize);
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Find Us</h4>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                We are on National Trust's Stourhead Estate, there are signposts from many miles around directing to the Property.
                <br /><br />
                When arriving at Stourhead ignore the main car park signs and continue down the hill. The Gallery car park is on the left immediatley before a stone building with a castelated tower.
            </div>
            <div class="col-md-6">
                <div id="googleMap" style="width:100%;height:380px;"></div>
                <p class="text-info">First-View Gallery, Spread Eagle Courtyard, Stourton, Wiltshire BA12 6QE T:01747 840747</p>
            </div>
        </div>
    </div>
</asp:Content>
