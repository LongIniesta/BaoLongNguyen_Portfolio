<%-- 
    Document   : viewPlant
    Created on : Jul 17, 2022, 5:06:46 PM
    Author     : baolo
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
        <link rel="stylesheet" href="mycss.css" type="text/css" />
    </head>
    <body>
         <jsp:useBean id="plantobj" class="sample.dto.Plant" scope="request"/>
        <table>
            <tr><td rowspan="8"><img src="<jsp:getProperty name="plantobj" property="imgpath"></jsp:getProperty>"</td></tr>
            <tr><td>ID<jsp:getProperty name="plantobj" property="id"></jsp:getProperty></td></tr>
            <tr><td>Product name<jsp:getProperty name="plantobj" property="name"></jsp:getProperty></td></tr>
            <tr><td>Price<jsp:getProperty name="plantobj" property="price"></jsp:getProperty></td></tr>
            <tr><td>Description<jsp:getProperty name="plantobj" property="description"></jsp:getProperty></td></tr>
            <tr><td>Status<jsp:getProperty name="plantobj" property="status"></jsp:getProperty></td></tr>
            <tr><td>Cate ID<jsp:getProperty name="plantobj" property="cateid"></jsp:getProperty></td></tr>
            <tr><td>Category<jsp:getProperty name="plantobj" property="catename"></jsp:getProperty></td></tr>
        </table>
        <table>
            <tr><td rowspan="8"><img src="${plantobj.imgpath}"></td></tr>
            <tr><td>ID:${plantobj.id}</td></tr>
            <tr><td>Product name:${plantobj.name}</td></tr>
            <tr><td>Price:${plantobj.price}</td></tr>
            <tr><td>Description:${plantobj.description}</td></tr>
            <tr><td>Status:${plantobj.status}</td></tr>
            <tr><td>Cate ID:${plantobj.cateid}</td></tr>
            <tr><td>Cate name:${plantobj.catename}</td></tr>
        </table>
    </body>
</html>
