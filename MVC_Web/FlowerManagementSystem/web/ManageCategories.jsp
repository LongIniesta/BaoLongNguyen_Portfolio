<%-- 
    Document   : ManageCategories
    Created on : Jul 19, 2022, 6:24:05 AM
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
        <h1>MANAGE CATEGORIES</h1>
        <table class="order" >
            <tr>
                <th>Category ID</th>
                <th>Name</th>
                            
            </tr>
              <c:forEach var="acc" items="${requestScope.categoryList}" >
                <tr>
                    <td><c:out value="${acc.getId()}"></c:out></td>
                    <td><c:out value="${acc.getName()}"></c:out></td>                   
                </tr>
            </c:forEach>
        </table>
    </body>
</html>
