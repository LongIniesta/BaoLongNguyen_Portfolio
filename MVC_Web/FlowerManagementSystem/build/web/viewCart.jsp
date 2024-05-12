<%-- 
    Document   : viewCart
    Created on : Jul 17, 2022, 2:15:32 PM
    Author     : baolo
--%>

<%@page import="sample.dao.PlantDAO"%>
<%@page import="sample.dto.Plant"%>
<%@page import="java.util.Date"%>
<%@page import="java.util.Set"%>
<%@page import="java.util.HashMap"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link rel="stylesheet" href="mycss.css" type="text/css" />
    </head>
    <body>
        <header>
            <%@include file="header.jsp" %>
        </header>
        <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
        <section>
            <%
                String name = (String) session.getAttribute("name");
                if (name != null) {
            %>
            <h3>Welcome <%= session.getAttribute("name")%></h3>
            <h3><a href="mainController?action=logout">Logout</a></h3>
            <h3><a href="personalPage2.jsp">Personal Page</a></h3>
            <%    }%>
            <font style='color: red;'><%= (request.getAttribute("Warning") == null) ? "" : (String) request.getAttribute("Warning")%></font> 
            <table width="100%" class="shopping">
                <tr><td>Product id</td><td>Image</td><td>price</td><td>quantity</td><td>action</td></tr>
                <%  
                    int total = 0;
                    HashMap<String, Integer> cart = (HashMap<String, Integer>) session.getAttribute("cart");
                    if (cart != null) {
                        Set<String> pids = cart.keySet();
                        for (String pid : pids) {
                            Plant plant = PlantDAO.getPlant(Integer.parseInt(pid));
                            int quantity = cart.get(pid);
                            total += plant.getPrice()*quantity;
                %>
                <form action="mainController" method="post">
                    <tr>
                        <td><input type="hidden" value="<%= pid%>" name="pid"><a href="getPlantServlet?pid=<%= pid%>"><%= pid%></a></td>
                        <td><img src='<%= plant.getImgpath() %>' class='plantimg'/></td>
                        <td><%= plant.getPrice() %></td>
                        <td><input type="number" value="<%= quantity%>" name="quantity"</td>
                        <td><input type="submit" value="update" name="action">
                            <input type="submit" value="delete" name="action"></td>
                    </tr>
                </form>
                <%  }
                } else {
                %>
                <tr><td>Your cart is empty</td></tr>
                <%  }%>
                <tr><td>Total money: <%= total %> $</td></tr>
                <tr><td>Order Date: <%= (new Date()).toString()%></td></tr>
                <tr><td>Ship Date: N/A</td></tr>
            </table>
        </section>
        <section>
            <form action="mainController" method="post">
                <input type="submit" value="SaveOrder" name="action" class="submitorder">
            </form>
        </section>
    </body>
</html>
