<%-- 
    Document   : ManageOrders
    Created on : Jul 19, 2022, 5:20:51 AM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link href="mycss.css" rel="stylesheet" type="text/css"/>
    </head>
    <body>
         <c:import url="header_loginedAdmin.jsp"/>
        <form action="mainController" method="POST">
            <input type="text" name="txtSearch"/>
            <input type="submit" value="searchOrder" name="action"/>
        </form>
        <h1>MANAGE ORDER</h1>
        <table class="order" >
            <tr>
                <th>OrderID</th>
                <th>OrderDate</th>
                <th>Shipdate</th>
                <th>status</th>
                <th>AccID</th>              
            </tr>
              <c:forEach var="acc" items="${requestScope.orderList}" >
                <tr>
                    <td><c:out value="${acc.getOrderID()}"></c:out></td>
                    <td><c:out value="${acc.getOderDate()}"></c:out></td>
                    <td><c:out value="${acc.getShipDate()}"></c:out></td>
                    <td>
                        <c:choose>
                        <c:when test="${acc.getStatus() eq 1}">Processing</c:when>
                        <c:when test="${acc.getStatus() eq 2}">Completed</c:when>
                        <c:when test="${acc.getStatus() eq 3}">Canceled</c:when>
                        
                    </c:choose></td>
                    
                    
                    <td><c:out value="${acc.getAccID()}"></c:out></td>
                    
                </tr>
            </c:forEach>
        </table>
    </body>
</html>
