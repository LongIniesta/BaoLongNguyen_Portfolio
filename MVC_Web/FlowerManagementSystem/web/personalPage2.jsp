<%-- 
    Document   : personalPage2
    Created on : Jul 16, 2022, 11:06:15 PM
    Author     : baolo
--%>

<%@page import="sample.dao.AccountDAO"%>
<%@page import="sample.dto.Account"%>
<%@page import="sample.dao.OrderDAO"%>
<%@page import="sample.dao.OrderDAO"%>
<%@page import="sample.dto.Order"%>
<%@page import="sample.dto.Order"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.ArrayList"%>
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
            String email = (String) session.getAttribute("email");
            Cookie[] c = request.getCookies();
            boolean login = false;
            if (name == null) {
                String token = "";
                for (Cookie aCookie : c) {
                    if (aCookie.getName().equals("selector")) {
                        token = aCookie.getValue();
                        Account acc = AccountDAO.getAccount(token);
                        if (acc != null) {
                            name = acc.getFullname();
                            email = acc.getEmail();
                            login = true;
                        }
                    }
                }
            } else {
                login = true;
            }
            if (login) {
        %>
        <header>
            <%@include file="header.jsp" %>
        </header>
        <section>
            <h3>Welcome <%= name%> come back </h3>
            <h3><a href="mainController?action=logout">Logout</a></h3>
        </section>
        <section>
            <%  
                ArrayList<Order> list = null;
                if(request.getAttribute("odStatus")== null) {
                    list = OrderDAO.getOrders(email);
                } else if(request.getAttribute("listFromTo")!=null){
                    list = (ArrayList<Order>)request.getAttribute("listFromTo");
                }
                else{
                    String hi = (String) request.getAttribute("odStatus");
                    int Status = Integer.parseInt(hi);
                    list = OrderDAO.getOrders(email, Status);
                } 
                
                String[] status = {"", "processing", "completed", "canceled"};
                if (list != null && !list.isEmpty()) {
                    for (Order ord : list) {
            %>
            <table class="order">
                <tr>
                    <td>Order ID</td>
                    <td>Order Date</td>
                    <td>Ship Date</td>
                    <td>Order's status</td>
                    <td>action</td>
                </tr>
                <tr>
                    <td><%= ord.getOrderID()%></td>
                    <td><%= ord.getOderDate()%></td>
                    <td><%= ord.getShipDate()%></td>

                    <td><%= status[ord.getStatus()]%> <br/> 
                        <% if (ord.getStatus() == 1) {

                        %>
                        <form action="mainController" method="POST">
                            <input type="hidden" name="orderidUpdate" value="<%= ord.getOrderID()%>"/>
                            <input type="submit" name="action" value="Cancel order"/>
                        </form>

                        <%} else if (ord.getStatus() == 3) {
                        %>
                        <form action="mainController" method="POST">
                            <input type="hidden" name="orderidUpdate" value="<%= ord.getOrderID()%>"/>
                            <input type="submit" name="action" value="Order Again"/>
                        </form>
                        <%}%>
                    </td>
                    <td><a href="orderDetail.jsp?orderid=<%= ord.getOrderID()%>">detail</a></td>
                </tr>
            </table>
            <%                    }
            } else { %>
        </section>
        <section>
            <p>You don't have any order</p>
        </section>
        <%    }
            }%>
        <footer>
            <%@include file="footer.jsp" %>
        </footer>
    </body>
</html>
