<%-- 
    Document   : header_loginedAdmin
    Created on : Jul 17, 2022, 7:54:35 PM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link href="mycss.css" ref="stylesheet" type="text/css"/>
    </head>
    <body>
        <header>
            <ul>
                <li><a href="mainController?action=manageAccounts">Manage Accounts</a></li>
                <li><a href="mainController?action=manageOrders">Manage Orders</a></li>
                <li><a href="mainController?action=managePlants">Manage Plants</a></li>
                <li><a href="mainController?action=manageCategories">Manage Categories</a></li>
                <li>Welcome ${sessionScope.name} | <a href="mainController?action=logout">logout</a></li>
            </ul>
        </header>
    </body>
</html>
