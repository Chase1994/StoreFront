<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomersAdmin.aspx.cs" Inherits="StoreFront.CustomersAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
            <h1>Access User Records Here: Add or Edit User Records</h1>
            <p class="lead">Access User Records via the User Records table, to add a new User Record, type in a UserName and an EmailAddress and click insert to add the User Record. If you wish to update an existing users data, simply click on &quot;Edit&quot; next to that user&#39;s record. </p>
     </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <p class="auto-style3">User Records</p>
            <div class="auto-style2">
            <asp:GridView ID="UserGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="UserGridSource" AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="CustomerAdminDetails.aspx?UserID={0}"/>
                    <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                    <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
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
                </div>
                <asp:SqlDataSource ID="UserGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="spGetUsers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </div>
        <div class="col-md-4">
                 <p class="auto-style3">Add A User Record</p>
                 <div class="auto-style2">
                <asp:DetailsView ID="AddUserDetailsView" runat="server" AutoGenerateInsertButton="True" AutoGenerateRows="False" DataKeyNames="UserID" DataSourceID="AddUserSource" DefaultMode="Insert" Height="134px" Width="265px" HorizontalAlign="Center" OnItemInserted="UserGrid_ItemInserting" CellPadding="4" ForeColor="#333333" GridLines="None" CellSpacing="4">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <EditRowStyle BackColor="#999999" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <Fields>
                        <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                        <asp:TemplateField HeaderText="UserName" SortExpression="UserName" ValidateRequestMode="Enabled">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="AddUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AddUserNameVal" runat="server" ControlToValidate="AddUserName" ErrorMessage="Username is required to submit." Font-Size="Smaller" style="text-align: left" Width="170px"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailAddress" SortExpression="EmailAddress" ValidateRequestMode="Enabled">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="AddEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailAddresRegExpVal" runat="server" ControlToValidate="AddEmailAddress" ErrorMessage="Enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="Smaller" Width="140px"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="AddEmailAddressValid" runat="server" ControlToValidate="AddEmailAddress" ErrorMessage="Add an email address to submit." Font-Size="Smaller" Width="160px"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                </asp:DetailsView>
                 </div>
                <asp:SqlDataSource ID="AddUserSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" InsertCommand="spAddUser" InsertCommandType="StoredProcedure" SelectCommand="spGetUsers" SelectCommandType="StoredProcedure">
                    <InsertParameters>
                        <asp:Parameter Name="UserName" Type="String" />
                        <asp:Parameter Name="EmailAddress" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="Stylesheets">
    <link href="/Content/Site.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        .auto-style2 {
            text-align: justify;
        }
        .auto-style3 {
            font-size: 21px;
            font-weight: 200;
            line-height: 1.4;
            text-align: justify;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

