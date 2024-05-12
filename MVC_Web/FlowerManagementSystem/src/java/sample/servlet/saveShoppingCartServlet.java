/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.HashMap;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import sample.dao.OrderDAO;

/**
 *
 * @author baolo
 */
public class saveShoppingCartServlet extends HttpServlet {

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
            throws ServletException, IOException, Exception {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            HttpSession sess = request.getSession(false);
            if (sess != null) {
                String name = (String) sess.getAttribute("name");
                String email = (String) sess.getAttribute("email");
                HashMap<String, Integer> cart = (HashMap<String, Integer>) sess.getAttribute("cart");
                if (cart != null && !cart.isEmpty()) {
                    if (name == null || name.equals("")) {
                        request.setAttribute("Warning", "Login to finish the shopping");
                        request.getRequestDispatcher("viewCart.jsp").forward(request, response);
                    } else {
                        boolean rs = OrderDAO.insertOrder(email, cart);
                        if (rs) {
                            sess.setAttribute("cart", null);
                            request.setAttribute("Warning", "Save successfully");
                            request.getRequestDispatcher("viewCart.jsp").forward(request, response);
                        } else {
                            request.setAttribute("Warning", "These products are out of stock");
                            request.getRequestDispatcher("viewCart.jsp").forward(request, response);
                        }
                    }
                } else {
                    request.setAttribute("Warning", "Your cart is empty!");
                    request.getRequestDispatcher("viewCart.jsp").forward(request, response);
                }
            } else {
                response.sendRedirect("index.jsp");
            }
        } catch (Exception e){
            e.printStackTrace();
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
        try {
            processRequest(request, response);
        } catch (Exception ex) {
            Logger.getLogger(saveShoppingCartServlet.class.getName()).log(Level.SEVERE, null, ex);
        }
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
        try {
            processRequest(request, response);
        } catch (Exception ex) {
            Logger.getLogger(saveShoppingCartServlet.class.getName()).log(Level.SEVERE, null, ex);
        }
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
