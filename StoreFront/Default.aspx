<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StoreFront._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
            <h1>Shred Skateboard Supplies</h1>
            <p class="lead">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; You need skateboard supplies. We sell them. Decks? Got it. Wheels? Got it. Bearings? Got it. Trucks? Got them too! Get ready to shred with some new gear!</p>
     </div>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-4">
            <h2>Our Mission</h2>
            <p>
                We believe that skateboarders shouldn't have to break their bank when they break their boards.
                Our prices reflect our beliefs in that aspect. If you want to shred, we are here to help.
                With 24/7 customer support we can assist you in anyway, at anytime, to help you get the gear you 
                need, whenever you need it. 
            </p>
        </div>
        <div class="col-md-5">
            <h2>Our Products</h2>
            <p>
                Here at Shred Skateboard Supplies we provide everything you need to shred the concrete ocean. 
                We carry the latest designs of decks, completes, trucks, bearings, grip tape, and wheels.
                If you need it, we have it, and we would love to exchange it to you in return for currency!
            </p>
            <p>
                <a class="btn btn-default" href="Products.aspx">View Our Products Here &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
