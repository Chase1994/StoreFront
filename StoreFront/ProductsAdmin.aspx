<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsAdmin.aspx.cs" Inherits="StoreFront.ProductsAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
            <h1>Shred Skateboard Products: View and Add Products</h1>
            <p class="auto-style1">Admin page for viewing and adding products to the products database.</p>
     </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <p class="lead">View Products</p>
            <asp:GridView ID="ProductGrid" runat="server" AutoGenerateColumns="False" DataSourceID="ProductGridSource" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ProductID" PageSize="20">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ProductID" DataNavigateUrlFormatString="ProductAdminDetails.aspx?ProductID={0}"/>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
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
            <asp:SqlDataSource ID="ProductGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="spGetProducts" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="False" Name="PublishedOnly" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>
        <div class="col-md-4">
             <p class="lead">Add A Product</p>
            <asp:DetailsView ID="ProductDetail" runat="server" AutoGenerateInsertButton="True" AutoGenerateRows="False" CellPadding="4" DataSourceID="ProductDetailSource" ForeColor="#333333" GridLines="None" Height="50px" HorizontalAlign="Center" Width="500px" DefaultMode="Insert" OnItemInserted="ProductGrid_OnInserted">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <EditRowStyle BackColor="#999999" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <Fields>
                    <asp:TemplateField HeaderText="ProductName" SortExpression="ProductName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="AddProductName" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="AddProductNameVal" runat="server" ControlToValidate="AddProductName" Display="Dynamic" ErrorMessage="Enter a Product Name to add." Font-Size="Smaller" Width="180px"></asp:RequiredFieldValidator>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductDescription" SortExpression="ProductDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="AddProductDescription" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="AddProductDescriptionVal" runat="server" ControlToValidate="AddProductDescription" ErrorMessage="Add a product description to submit." Font-Size="Smaller" Width="175px"></asp:RequiredFieldValidator>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
                    <asp:TemplateField HeaderText="Price" SortExpression="Price">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="AddProductPrice" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="ProductPriceRegExpVal" runat="server" ControlToValidate="AddProductPrice" ErrorMessage="Please enter a valid price (ex., 2.99)." Font-Size="Smaller" Width="180px" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="ProductPriceVal" runat="server" ControlToValidate="AddProductPrice" ErrorMessage="Enter a price to submit." Font-Size="Smaller" Width="140px"></asp:RequiredFieldValidator>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            </asp:DetailsView>
            <asp:SqlDataSource ID="ProductDetailSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="SELECT [ProductName], [ProductDescription], [IsPublished], [Price] FROM [Products]" InsertCommand="spAddProduct" InsertCommandType="StoredProcedure">
                <InsertParameters>
                    <asp:Parameter Name="ProductName" Type="String" />
                    <asp:Parameter Name="ProductDescription" Type="String" />
                    <asp:Parameter Name="IsPublished" Type="Boolean" />
                    <asp:Parameter Name="Price" Type="Decimal" />
                </InsertParameters>
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
            text-align: center;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

