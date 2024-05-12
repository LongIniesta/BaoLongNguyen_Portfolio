<%-- 
    Document   : ManagePlants
    Created on : Jul 19, 2022, 6:01:25 AM
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
            <input type="submit" value="searchPlant" name="action"/>
        </form>
        <h1>MANAGE PLANT</h1>
        <table class="order" >
            <tr>
                <th>Plant ID</th>
                <th>Plant Name</th>
                <th>Price</th>
                <th>Image</th>
                <th>Description</th>
                <th>Status</th>   
                <th>Cate ID</th>
                <th>Cate Name</th>       
                
            </tr>
              <c:forEach var="acc" items="${requestScope.plantList}" >
                <tr>
                    <td><c:out value="${acc.getId()}"></c:out></td>
                    <td><c:out value="${acc.getName()}"></c:out></td>
                    <td><c:out value="${acc.getPrice()}"></c:out></td>
                    <td><img src="${acc.getImgpath()}" class='plantimg'/> </td>
                    <td><c:out value="${acc.getDescription()}"></c:out></td>
                    <td>
                        <c:choose>
                        <c:when test="${acc.getStatus() eq 1}">Het hang</c:when>
                        <c:when test="${acc.getStatus() eq 2}">Con hang</c:when>
                       
                        
                    </c:choose></td>
                    
                    
                    <td><c:out value="${acc.getCateid()}"></c:out></td>
                    <td><c:out value="${acc.getCatename()}"></c:out></td>
                    
                </tr>
            </c:forEach>
        </table>
    </body>

</html>
