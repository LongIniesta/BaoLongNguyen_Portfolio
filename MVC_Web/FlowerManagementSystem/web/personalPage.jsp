<%-- 
    Document   : personalPage
    Created on : Jul 16, 2022, 9:33:01 PM
    Author     : baolo
--%>

<%@page import="sample.dto.Account"%>
<%@page import="javax.xml.registry.infomodel.User"%>
<%@page import="sample.dto.Order"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.ArrayList"%>
<%@page import="sample.dao.OrderDAO"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link rel="stylesheet" href="mycss.css" type="text/css"/>
    </head>
    <body>
    
        <%
            String name = (String) session.getAttribute("name");
            if (name == null) {
        %>
        <p><font color="red">you must login to view personal page</font></p>
        <p></p>
        <%} else {
        %>
        <header>
            <%@include file="header_loginedUser.jsp"%>
        </header>
        <section>
            <h3>Welcome <%=name%> comeback </h3>
            <h3><a href="mainController?action=logout">Logout</a></h3>
        </section>
        <section>
            <%
                String email = (String) session.getAttribute("email");
                ArrayList<Order> list = OrderDAO.getOrders(email);
                String[] status = {"", "processing", "complete", "canceled"};
                if (list != null && !list.isEmpty()) {
                    for (Order ord : list) {%>
            <table class="order">
                <tr><td>OrderID</td><td>OrderDate</td><td>ShipDate</td><td>Order's status</td><td>action</td></tr>
                <tr>
                    <td><%= ord.getOrderID()%></td>
                    <td><%= ord.getOderDate()%></td>
                    <td><%= ord.getShipDate()%></td>
                    <td><%= status[ord.getStatus()] %>
                        <br/><% if (ord.getStatus() == 1) %> <a href="#">cancel order</a>
                    </td>
                    <td><a href="orderDetail.jsp?orderid=<%= ord.getOrderID() %>">detail</a></td>
                </tr>
            </table>                                
            <%
                }
            }
else{

            %>
            <p>You don't have any order</p> <%}%>
        </section>
        <footer>
            <%@include file="footer.jsp" %>
        </footer>
        <%}%>
    </body>
</html>
