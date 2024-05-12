<%-- 
    Document   : header_loginedUser.jsp
    Created on : Jul 16, 2022, 9:27:38 PM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link rel="stylesheet" href="mycss.css" type="text/css"/>
    </head>
    <body>
        <header>
            <ul>
                <li><a href="index.jsp">Home</a></li>
                <li><a href="changProfile.jsp">change profile</a></li>
                <li><a href="mainController?action=comOD">completed orders</a></li>
                <li><a href="mainController?action=canOD">Canceled orders</a></li>
                <li><a href="mainController?action=proOD">processing orders</a></li>
                <li><form action="mainController" method="POST">
                        from  <input type="date" name="from" value="2021-01-01"/>  to  <input type="date" name="to" value="2021-01-01"/>
                        <input type="submit" value="searchODfromto" name="action">     
                    </form>

                </li>

            </ul>
            </header>
                </body>
                </html>
