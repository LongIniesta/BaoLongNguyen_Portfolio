<%-- 
    Document   : login
    Created on : Jul 13, 2022, 9:34:17 PM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <title>TODO supply a title</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
    </head>
    <body>
        <header>
            <%@include file="header.jsp" %>
        </header>
        <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
        <section>
            <form action="mainController" method="post" class="formlogin">
                  <font style='color: red;'><%=  (request.getAttribute("Warning")==null)?"":(String)request.getAttribute("Warning") %></font>
                <table>
                    <tr><td>email</td><td><input type="text" name="txtemail" /></td></tr>
                    <tr><td>password</td><td><input type="password" name="txtpassword" /></td></tr>
                    <tr><td colspan="2"><input type="submit" value="login" name="action" /></td></tr>
                     <tr><td colspan="2"><input type="checkbox" value="savelogin" name="savelogin">Stayed signed in</td></tr>
                </table>
            </form>
        </section>
        <footer>
            <%@include file="footer.jsp" %>
        </footer>
    </body>
</html>

