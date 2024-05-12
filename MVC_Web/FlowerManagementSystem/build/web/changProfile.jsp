<%-- 
    Document   : changProfile
    Created on : Jul 19, 2022, 3:27:24 AM
    Author     : baolo
--%>

<%@page import="sample.dto.Account"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
    </head>
    <body>
        
    <head>
        <title>TODO supply a title</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link rel="stylesheet" href="mycss.css" type="text/css" />
    </head>
    <body>
        <header>
            <%@include  file="header_loginedUser.jsp" %>
        </header>
        <%
            Account acc = (Account) session.getAttribute("user");
        %>
        <section>
            <form action="mainController" method="post" class="form">               
                <h1>CHANGE PROFILE</h1>
                <table>
                    <tr><td>email</td><td><input type="text" name="txtemail" required="" value="<%= acc==null?"":acc.getEmail() %>"/>can't change email</td></tr>
                    <tr><td>fullname</td><td><input type="text" name="txtfullname" required="" value="<%= acc==null?"":acc.getFullname() %>"/></td></tr>
                    <tr><td>password</td><td><input type="text" name="txtpassword" required="" value="can't change password"/></td></tr>
                    <tr><td>phone</td><td><input type="text" name="txtphone" required="" value="<%= acc==null?"":acc.getPhone() %>"/><br/>
                        <%= (request.getAttribute("changeProfile")==null)?"":request.getAttribute("changeProfile") %></td></tr>
                    <tr><td colspan="2"><input type="submit" value="changeProfile" name="action"/></td></tr>
                </table>
                        
                
            </form>
        </section>
        <footer>
            <%@include  file="footer.jsp"%>
        </footer>
    </body>
    </body>
</html>
