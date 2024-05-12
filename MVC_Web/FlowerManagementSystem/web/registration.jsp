<%-- 
    Document   : registration
    Created on : Jul 13, 2022, 9:35:03 PM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <title>TODO supply a title</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link rel="stylesheet" href="mycss.css" type="text/css" />
    </head>
    <body>
        <header>
            <%@include  file="header.jsp" %>
        </header>
        <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
        <section>
            <form action="mainController" method="post" class="form">
                <h1>Register</h1>
                <table>
                    <tr><td>email</td><td><input type="text" name="txtemail" required="" pattern="^(\\w)+@(a-zA-Z]+([.](\\w+){1,2}" value="<%= (request.getAttribute("txtemail")==null)?"":request.getAttribute("txtemail") %>"/></td></tr>
                    <tr><td>fullname</td><td><input type="text" name="txtfullname" required="" value="<%= (request.getAttribute("txtfullname")==null)?"":request.getAttribute("txtfullname") %>"/></td></tr>
                    <tr><td>password</td><td><input type="text" name="txtpassword" required="" /></td></tr>
                    <tr><td>phone</td><td><input type="text" name="txtphone" required="" <%= (request.getAttribute("txtphone")==null)?"":request.getAttribute("txtphone") %>"/>
                        <%= (request.getAttribute("Error")==null)?"":request.getAttribute("Error") %></td></tr>
                    <tr><td colspan="2"><input type="submit" value="register" name="action"/></td></tr>
                </table>
                
            </form>
        </section>
        <footer>
            <%@include  file="footer.jsp"%>
        </footer>
    </body>
</html>
