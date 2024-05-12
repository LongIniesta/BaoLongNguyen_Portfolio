<%-- 
    Document   : header
    Created on : Jul 13, 2022, 9:30:50 PM
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
            
                <ul>
                    
                    <li><a href="index.jsp">Home</a></li>
                    <li><a href="registration.jsp">Register</a></li>
                    <li><a href="login.jsp" >Login</a></li>
                    <li><a href="mainController?action=viewcart">view cart</a></li>

                    <li><form action="mainController" method="POST" class="formsearch">
                            <input type="text" name="txtsearch" value="<%= (request.getParameter("txtsearch") == null) ? "" : request.getParameter("txtsearch")%>">
                           
                            <input type="radio" id="byname" name="by" value="byname" checked="true">
                                <label for="byname">BY NAME</label>
                                <input type="radio" id="bycate" name="by" value="bycate">
                                <label for="bycate">BY CATE</label>
                            <!--                            <select name=”by” value="byname">
                                                            <option value=”byname”>by name</option>
                                                            <option value=”bycate”>by category</option>
                                                        </select>-->
                            <input type="submit" value="search" name="action" >
                        </form></li>
                </ul>
            
        </header>
        <section>
        </section>
        <footer>
            <p></p>
        </footer>
    </body>
</html>
