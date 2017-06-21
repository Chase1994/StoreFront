<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductAdminDetails.aspx.cs" Inherits="StoreFront.ProductAdminDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
            <h1>Update Product Records Here</h1>
            <p class="auto-style1">Update or Delete current Product Record.</p>
     </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <p class="lead">Current Record</p>
            <asp:GridView ID="ProductView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ProductID" DataSourceID="ProductGrid" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True" SortExpression="ProductID" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="ProductDescription" HeaderText="ProductDescription" SortExpression="ProductDescription" />
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
            <asp:SqlDataSource ID="ProductGrid" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" SelectCommand="spGetProduct" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ProductID" QueryStringField="ProductID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>
        <div class="col-md-4">
            <p class="lead">Update/Delete Product></p>
                <asp:DetailsView ID="ProductUpdateDetail" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="ProductID" DataSourceID="ProductDetailSource" DefaultMode="Edit" ForeColor="#333333" GridLines="None" Height="50px" HorizontalAlign="Center" Width="125px" OnItemUpdated="ProductView_OnUpdated" OnItemDeleted="BackToProducts_OnDeleted">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <EditRowStyle BackColor="#999999" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <Fields>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True" SortExpression="ProductID" />
                    <asp:TemplateField HeaderText="ProductName" SortExpression="ProductName">
                        <EditItemTemplate>
                            <asp:TextBox ID="UpdateProductName" runat="server" CausesValidation="True" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UpdateProductNameVal" runat="server" ControlToValidate="UpdateProductName" ErrorMessage="Enter a ProductName to submit." Font-Size="Smaller" Width="150px"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductDescription" SortExpression="ProductDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="UpdateProductDetail" runat="server" CausesValidation="True" Text='<%# Bind("ProductDescription") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UpdateProductDetailVal" runat="server" ControlToValidate="UpdateProductDetail" ErrorMessage="Enter product details to submit." Font-Size="Smaller" Width="150px"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
                    <asp:TemplateField HeaderText="Price" SortExpression="Price">
                        <EditItemTemplate>
                            <asp:TextBox ID="UpdateProductPrice" runat="server" CausesValidation="True" Text='<%# Bind("Price") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UpdateProductPriceVal" runat="server" ControlToValidate="UpdateProductPrice" ErrorMessage="Enter a price to submit." Font-Size="Smaller" Width="120px"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UpdateProductPrice" ErrorMessage="Enter a valid price (ex., 49.99)." Font-Size="Smaller" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$" Width="150px"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
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
            <asp:FileUpload ID="ProductImageUpload" runat="server" CssClass="container" />
            <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" />
                <asp:SqlDataSource ID="ProductDetailSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreFrontDBConnectionString %>" DeleteCommand="spDeleteProduct" DeleteCommandType="StoredProcedure" SelectCommand="spGetProduct" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateProduct" UpdateCommandType="StoredProcedure">
                    <DeleteParameters>
                        <asp:Parameter Name="ProductID" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="ProductID" QueryStringField="ProductID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ProductID" Type="Int32" />
                        <asp:Parameter Name="ProductName" Type="String" />
                        <asp:Parameter Name="ProductDescription" Type="String" />
                        <asp:Parameter Name="IsPublished" Type="Boolean" />
                        <asp:Parameter Name="Price" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            <br />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
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

