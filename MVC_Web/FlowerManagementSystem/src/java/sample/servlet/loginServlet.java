/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import sample.dao.AccountDAO;
import sample.dto.Account;

/**
 *
 * @author baolo
 */
public class loginServlet extends HttpServlet {

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
        String email = request.getParameter("txtemail");
            String password = request.getParameter("txtpassword");
            String save = request.getParameter("savelogin");
            Account acc = null;
        try (PrintWriter out = response.getWriter()) {
            if ( email == null || email.equals("") || password == null || password.equals("")){
                    Cookie[] c = request.getCookies();
                    String token = "";
                    if (c != null){
                        for (Cookie aCookie : c){
                            if (aCookie.getName().equals("selector"))
                                token = aCookie.getValue();
                        }
                    }
                    if (!token.equals("")) response.sendRedirect("personalPage2.jsp");
                    else response.sendRedirect("errorpage.html");
                } else {
                    acc = AccountDAO.getAccount(email, password);
                    if (acc != null) {
                        if (acc.getRole() == 1) {
                            HttpSession session = request.getSession(true);
                            if(session != null){
                                session.setAttribute("name", acc.getFullname());
                                session.setAttribute("email", email);
                                session.setAttribute("user", acc);
                                if (save != null){
                                    String token = "SAVED";//chi la vi du
                                    AccountDAO.updateToken(token, email);
                                    Cookie cookie = new Cookie("selector", token);
                                    cookie.setMaxAge(50*2);
                                    response.addCookie(cookie);
                                }
                                request.getRequestDispatcher("AdminIndex.jsp").forward(request, response);
                            }
                        } else {
                            //response.sendRedirect("welcome.html");
                            HttpSession sess = request.getSession(true);
                                if (sess != null) {
                                sess.setAttribute("name", acc.getFullname());;
                                sess.setAttribute("email", email);
                                sess.setAttribute("user", acc);
                                //response.sendRedirect("personalPage.jsp");
                                if (save != null){
                                    String token = "SAVED";//chi la vi du
                                    AccountDAO.updateToken(token, email);
                                    Cookie cookie = new Cookie("selector", token);
                                    cookie.setMaxAge(50*2);
                                    response.addCookie(cookie);
                                }
                                request.getRequestDispatcher("UserIndex.jsp").forward(request, response);
                            }
                        }
                    } else response.sendRedirect("errorpage.html");
                    }
        } catch (Exception e) {
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
