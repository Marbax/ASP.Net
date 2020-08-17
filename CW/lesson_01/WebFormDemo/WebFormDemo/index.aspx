<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="WebFormDemo.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <form id="form1" runat="server">
        <div>Hello World again
            <asp:Button ID="Button1" runat="server" Height="76px" Text="Button" Width="104px" />
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        </form>
</body>
</html>
