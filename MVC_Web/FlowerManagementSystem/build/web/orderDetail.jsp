<%-- 
    Document   : orderDetail
    Created on : Jul 16, 2022, 11:23:11 PM
    Author     : baolo
--%>

<%@page import="sample.dao.OrderDAO"%>
<%@page import="sample.dto.OrderDetail"%>
<%@page import="java.util.ArrayList"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link rel="stylesheet" href="mycss.css" type="text/css" />
    </head>
    <body>
       
        <%
            String name = (String) session.getAttribute("name");
            if (name == null){
        %>
        <p><font color='red'>You must login to view personal page</font></p>
        <p></p>
        <%
            } else {
        %>
        <header>
            <%@include file="header_loginedUser.jsp" %>
        </header>
        <section>
            <h3>Welcome <%= name %> come back</h3>
            <h3>Logout</h3>
        </section>
            <section>
                <%
                    String orderid = request.getParameter("orderid");
                    if (orderid != null){
                        int orderID = Integer.parseInt(orderid.trim());
                        ArrayList<OrderDetail> list = OrderDAO.getOrderDetail(orderID);
                        if (list != null && !list.isEmpty()){
                            int m = 0;
                            for (OrderDetail detail: list){
                %>
                <table class="order">
                    <tr>
                        <td>Order ID</td>
                        <td>Plant ID</td>
                        <td>Plant Name</td>
                        <td>Image</td>
                        <td>quantity</td>
                    </tr>
                    <tr>
                        <td><%= detail.getOrderID() %></td>
                        <td><%= detail.getfID() %></td>
                        <td><%= detail.getfName() %></td>
                        <td><img src='<%= detail.getImgPath() %>' class='plantimg'/><br/><%= detail.getPrice() %></td>     
                        <td><%= detail.getQuantity() %></td>
                        <% m = m + detail.getPrice() * detail.getQuantity(); %>
                    </tr>
                </table>
                <%      } %>
                <h3>Total money: <%= m %></h3>                        }
                <%    } else { %>
                <p>You don't have any order</p>
                <% }
                } %>
            </section>
        <footer>
            <%@include file="footer.jsp" %>
        </footer>
        <%}%>
    </body>
</html>
