<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerAdminDetails.aspx.cs" Inherits="StoreFront.CustomerAdminDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Customer Admin Details</h1>
        <p class="lead">Use this page to update customer details. Type in the new UserName and EmailAddress and click "Update" to change the info.
            You can also delete the user by clicking the button labeled "Delete".
        </p>
</div>
   
<div class="row">
    <div class="col-md-1"></div>
    <div class="col-md-3">
        <p class="auto-style1">User Selected</p>
         <asp:DetailsView ID="UserInfoDisplayGrid" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="UserID" DataSourceID="DisplayUserInfo" ForeColor="#333333" GridLines="None" Height="115px" HorizontalAlign="Center" Width="251px">
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" BorderStyle="None" />
             <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="DisplayUserInfo" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="spGetUser" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="UserID" QueryStringField="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
    <div class="col-md-3">
        <p class="auto-style1">Modify User Records</p>
        <asp:DetailsView ID="UserUpdateView" runat="server" AutoGenerateRows="False" DataKeyNames="UserID" DataSourceID="UserInfo" Height="55px" Width="110px" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" DefaultMode="Edit" OnItemUpdated="UserInfoDisplayGrid_ItemUpdate" OnItemDeleted="BackToUsers_OnDeleted">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <EditRowStyle BackColor="#999999" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <Fields>
                <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                    <EditItemTemplate>
                        <asp:TextBox ID="EditUserName" runat="server" Text='<%# Bind("UserName") %>' ValidateRequestMode="Enabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EditUserNameVal" runat="server" ControlToValidate="EditUserName" ErrorMessage="Enter a UserName to submit." Font-Size="Smaller" Width="150px"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmailAddress" SortExpression="EmailAddress">
                    <EditItemTemplate>
                        <asp:TextBox ID="EditEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>' ValidateRequestMode="Enabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailAddressVal" runat="server" ControlToValidate="EditEmailAddress" ErrorMessage="Enter an EmailAddress." Font-Size="Smaller" Width="130px"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EditEmailAddress" ErrorMessage="Enter a valid EmailAddress." Font-Size="Smaller" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="150px"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
            <FooterTemplate>
                <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                <asp:Button ID="btnDelete" runat="server" CausesValidation="True" CommandName="Delete" Text="Delete" />
            </FooterTemplate>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        </asp:DetailsView>
        <asp:SqlDataSource ID="UserInfo" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" DeleteCommand="spDeleteUser" DeleteCommandType="StoredProcedure" SelectCommand="spGetUser" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateUser" UpdateCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="UserID" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="UserID" QueryStringField="UserID" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserID" Type="Int32" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="EmailAddress" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    <div class="col-md-3">
        <p class="auto-style1">User Address Record<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="AddressID" DataSourceID="UserAddressSource" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="AddressID" HeaderText="AddressID" InsertVisible="False" ReadOnly="True" SortExpression="AddressID" />
                <asp:BoundField DataField="Address1" HeaderText="Address1" SortExpression="Address1" />
                <asp:BoundField DataField="Address2" HeaderText="Address2" SortExpression="Address2" />
                <asp:BoundField DataField="Address3" HeaderText="Address3" SortExpression="Address3" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="StateName" HeaderText="StateName" SortExpression="StateName" />
                <asp:BoundField DataField="ZipCode" HeaderText="ZipCode" SortExpression="ZipCode" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </p>
        <asp:SqlDataSource ID="UserAddressSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="spGetUserAddresses" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="UserID" QueryStringField="UserID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</div>    
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="Stylesheets">
    <link href="/Content/Site.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        .auto-style1 {
            font-size: 21px;
            font-weight: 200;
            line-height: 1.4;
            text-align: justify;
            margin-bottom: 20px;
        }
        </style>
</asp:Content>

