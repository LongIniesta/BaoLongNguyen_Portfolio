/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.filter;

import java.io.IOException;
import java.io.PrintStream;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.List;
import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.annotation.WebFilter;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import sample.dto.Account;

/**
 *
 * @author baolo
 */
@WebFilter(filterName = "AuthenFilter", urlPatterns = {"/*"})
public class AuthenFilter implements Filter {

    private static List<String> USER_FUNCTION;
    private static List<String> ADMIN_FUNCTION;
    private static List<String> NON_AUTHEN_FUNCTION;
    private static final boolean debug = true;

    // The filter configuration object we are associated with.  If
    // this value is null, this filter instance is not currently
    // configured. 
    private FilterConfig filterConfig = null;

    public AuthenFilter() {
        USER_FUNCTION = new ArrayList<>();
        USER_FUNCTION.add("addtoCartServlet");
        USER_FUNCTION.add("contactServlet");
        USER_FUNCTION.add("getPlantServlet");
        USER_FUNCTION.add("loginServlet");
        USER_FUNCTION.add("logoutServlet");
        USER_FUNCTION.add("mainController");
        USER_FUNCTION.add("registerServlet");
        USER_FUNCTION.add("saveShoppingCartServlet");
        USER_FUNCTION.add("searchServlet");
        USER_FUNCTION.add("sendOTP");
        USER_FUNCTION.add("updateCartServlet");
        USER_FUNCTION.add("login.jsp");
        USER_FUNCTION.add("UserIndex.jsp");
        USER_FUNCTION.add("errorpage.jsp");
        USER_FUNCTION.add("index.jsp");
        USER_FUNCTION.add("invalid.html");
        USER_FUNCTION.add("orderDetail.jsp");
        USER_FUNCTION.add("personalPage2");
        USER_FUNCTION.add("registration.jsp");
        USER_FUNCTION.add("viewCart.jsp");
        USER_FUNCTION.add("viewPlant.jsp");

        ADMIN_FUNCTION.add("contactServlet");
        ADMIN_FUNCTION.add("loginServlet");
        ADMIN_FUNCTION.add("logoutServlet");
        ADMIN_FUNCTION.add("mainController");
        ADMIN_FUNCTION.add("registerServlet");
        ADMIN_FUNCTION.add("searchServlet");
        ADMIN_FUNCTION.add("sendOTP");
        ADMIN_FUNCTION.add("login.jsp");
        ADMIN_FUNCTION.add("errorpage.jsp");
        ADMIN_FUNCTION.add("index.jsp");
        ADMIN_FUNCTION.add("invalid.html");
        ADMIN_FUNCTION.add("registration.jsp");
        ADMIN_FUNCTION.add("AdminIndex.jsp");

        NON_AUTHEN_FUNCTION.add("contactServlet");
        NON_AUTHEN_FUNCTION.add("getPlantServlet");
        NON_AUTHEN_FUNCTION.add("loginServlet");
        NON_AUTHEN_FUNCTION.add("mainController");
        NON_AUTHEN_FUNCTION.add("registerServlet");
        NON_AUTHEN_FUNCTION.add("searchServlet");
        NON_AUTHEN_FUNCTION.add("sendOTP");
        NON_AUTHEN_FUNCTION.add("login.jsp");
        NON_AUTHEN_FUNCTION.add("errorpage.jsp");
        NON_AUTHEN_FUNCTION.add("index.jsp");
        NON_AUTHEN_FUNCTION.add("registration.jsp");
        NON_AUTHEN_FUNCTION.add("viewPlant.jsp");

    }

    private void doBeforeProcessing(ServletRequest request, ServletResponse response)
            throws IOException, ServletException {
        if (debug) {
            log("AuthenFilter:DoBeforeProcessing");
        }

        // Write code here to process the request and/or response before
        // the rest of the filter chain is invoked.
        // For example, a logging filter might log items on the request object,
        // such as the parameters.
        /*
	for (Enumeration en = request.getParameterNames(); en.hasMoreElements(); ) {
	    String name = (String)en.nextElement();
	    String values[] = request.getParameterValues(name);
	    int n = values.length;
	    StringBuffer buf = new StringBuffer();
	    buf.append(name);
	    buf.append("=");
	    for(int i=0; i < n; i++) {
	        buf.append(values[i]);
	        if (i < n-1)
	            buf.append(",");
	    }
	    log(buf.toString());
	}
         */
    }

    private void doAfterProcessing(ServletRequest request, ServletResponse response)
            throws IOException, ServletException {
        if (debug) {
            log("AuthenFilter:DoAfterProcessing");
        }

        // Write code here to process the request and/or response after
        // the rest of the filter chain is invoked.
        // For example, a logging filter might log the attributes on the
        // request object after the request has been processed. 
        /*
	for (Enumeration en = request.getAttributeNames(); en.hasMoreElements(); ) {
	    String name = (String)en.nextElement();
	    Object value = request.getAttribute(name);
	    log("attribute: " + name + "=" + value.toString());

	}
         */
        // For example, a filter might append something to the response.
        /*
	PrintWriter respOut = new PrintWriter(response.getWriter());
	respOut.println("<P><B>This has been appended by an intrusive filter.</B>");
         */
    }

    /**
     *
     * @param request The servlet request we are processing
     * @param response The servlet response we are creating
     * @param chain The filter chain we are processing
     *
     * @exception IOException if an input/output error occurs
     * @exception ServletException if a servlet error occurs
     */
    @Override
    public void doFilter(ServletRequest request, ServletResponse response,
            FilterChain chain)
            throws IOException, ServletException {
        try {
            HttpServletRequest req = (HttpServletRequest) request;
            HttpServletResponse res = (HttpServletResponse) response;
            String uri = req.getRequestURI();
            int index = uri.lastIndexOf("/");
            String resource = uri.substring(index + 1);
            if (uri.contains(".jpg") || uri.contains(".png") || NON_AUTHEN_FUNCTION.contains(resource)) {
                chain.doFilter(request, response);
            } else {
                HttpSession session = req.getSession();
                if (session == null || session.getAttribute("user") == null) {
                    res.sendRedirect("login.jsp");
                } else {
                    Account acc = (Account) session.getAttribute("user");
                    int role = acc.getRole();
                    if (role == 1 && ADMIN_FUNCTION.contains(resource)) {
                        chain.doFilter(request, response);
                    } else if (role == 0 && USER_FUNCTION.contains(resource)) {
                        chain.doFilter(request, response);
                    } else {
                        res.sendRedirect("login.jsp");
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Return the filter configuration object for this filter.
     */
    public FilterConfig getFilterConfig() {
        return (this.filterConfig);
    }

    /**
     * Set the filter configuration object for this filter.
     *
     * @param filterConfig The filter configuration object
     */
    public void setFilterConfig(FilterConfig filterConfig) {
        this.filterConfig = filterConfig;
    }

    /**
     * Destroy method for this filter
     */
    public void destroy() {
    }

    /**
     * Init method for this filter
     */
    public void init(FilterConfig filterConfig) {
        this.filterConfig = filterConfig;
        if (filterConfig != null) {
            if (debug) {
                log("AuthenFilter:Initializing filter");
            }
        }
    }

    /**
     * Return a String representation of this object.
     */
    @Override
    public String toString() {
        if (filterConfig == null) {
            return ("AuthenFilter()");
        }
        StringBuffer sb = new StringBuffer("AuthenFilter(");
        sb.append(filterConfig);
        sb.append(")");
        return (sb.toString());
    }

    private void sendProcessingError(Throwable t, ServletResponse response) {
        String stackTrace = getStackTrace(t);

        if (stackTrace != null && !stackTrace.equals("")) {
            try {
                response.setContentType("text/html");
                PrintStream ps = new PrintStream(response.getOutputStream());
                PrintWriter pw = new PrintWriter(ps);
                pw.print("<html>\n<head>\n<title>Error</title>\n</head>\n<body>\n"); //NOI18N

                // PENDING! Localize this for next official release
                pw.print("<h1>The resource did not process correctly</h1>\n<pre>\n");
                pw.print(stackTrace);
                pw.print("</pre></body>\n</html>"); //NOI18N
                pw.close();
                ps.close();
                response.getOutputStream().close();
            } catch (Exception ex) {
            }
        } else {
            try {
                PrintStream ps = new PrintStream(response.getOutputStream());
                t.printStackTrace(ps);
                ps.close();
                response.getOutputStream().close();
            } catch (Exception ex) {
            }
        }
    }

    public static String getStackTrace(Throwable t) {
        String stackTrace = null;
        try {
            StringWriter sw = new StringWriter();
            PrintWriter pw = new PrintWriter(sw);
            t.printStackTrace(pw);
            pw.close();
            sw.close();
            stackTrace = sw.getBuffer().toString();
        } catch (Exception ex) {
        }
        return stackTrace;
    }

    public void log(String msg) {
        filterConfig.getServletContext().log(msg);
    }

}
