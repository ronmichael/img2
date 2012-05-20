<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        full size
        <br />
        <img runat="server" src="/images/dog.png" alt='my dog'  />
        <br />
        <br />
        proprtional resize to 150 width
        <br /> 
        <img runat="server" src="/images/dog.png" width=150 alt='again my dog' />
        <br />
        <br />
        svg
        <br />
        <img runat="server" src="/images/test.svg" altsrc="/images/dog.png" width=100 alt='some logos'  />
    </div>
    </form>
</body>
</html>
