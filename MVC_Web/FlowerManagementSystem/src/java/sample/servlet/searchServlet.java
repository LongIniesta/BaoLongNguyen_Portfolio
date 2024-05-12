/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import javax.servlet.ServletConfig;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import sample.dao.PlantDAO;
import sample.dto.Plant;

/**
 *
 * @author baolo
 */
public class searchServlet extends HttpServlet {

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {

            String keyword = request.getParameter("txtsearch");
            String searchby = request.getParameter("searchby");
            System.out.println(request.getParameter("searchby"));
            ArrayList<Plant> list = PlantDAO.getPlants(keyword, "byname");

            /* TODO output your page here. You may use following sample code. */
            if (list != null && !list.isEmpty()) {
                out.println("<!DOCTYPE html>");
                out.println("<html>");
                out.println("<head>");
                out.println("<title>Servlet searchServlet</title>");
                
                out.println("<link rel='stylesheet' href='mycss.css' type='text/css' />");
                out.println("</head>");
                out.println("<body><section>");
                ServletContext context = getServletContext();
                String tmp =context.getInitParameter("countryName");
                out.println("<p>the website deploying in "+tmp+"</p>");
                
                ServletConfig servletconfig = getServletConfig();
                String tmp2 = servletconfig.getInitParameter("language");
                out.println("<p>Language is used only in this page: "+tmp2+"</p>");
    
                
                out.println("<table class='producttable'");
                out.println("<tr><td>product id</td><td>name</td><td>price</td><td>image</td><td>detail</td><td>action</td></tr>");
                for (Plant plant : list) {
                    out.println("<tr>");
                    out.println("<td>" + plant.getId() + "</td>");
                    out.println("<td>" + plant.getName() + "</td>");
                    out.println("<td>" + plant.getPrice() + "</td>");
                    out.println("<td><img src='" + plant.getImgpath() + "' class = 'product' /></td>");
                    out.println("<td><a href= '#'>view detail</a></td>");
                    out.println("<td><a href = '#'> add to your cart</a></td>");
                    out.println("</tr>");
                }
                out.println("</table></section>");
                out.println("</body>");
                out.println("</html>");
            } else System.out.println(keyword +"        " + searchby);

        }
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
