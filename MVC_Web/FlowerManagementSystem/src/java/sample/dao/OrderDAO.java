/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.dao;

import java.sql.Connection;
import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Set;
import sample.dto.Order;
import sample.dto.OrderDetail;
import sample.dto.Plant;
import sample.utils.DBUtils;

/**
 *
 * @author baolo
 */
public class OrderDAO {

    public static Order getOrdersbyID(int id) {
        Order od = null;
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select * from Orders\n"
                        + "where OrderID = ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setInt(1, id);
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        od = new Order(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4), rs.getInt(5));

                    }
                    rs.close();
                    pst.close();
                    cn.close();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return od;
    }

    public static ArrayList<Order> getOrders(String email) {
        ArrayList<Order> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null && email != null) {
                String sql = "select od.OrderID,od.OrdDate,od.shipdate,od.status,od.AccID from Orders as od,Accounts as ac \n"
                        + "where od.AccID=ac.accID and email like ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, email);
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        Order od = new Order(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4), rs.getInt(5));
                        list.add(od);
                    }
                    rs.close();
                    pst.close();
                    cn.close();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }
    public static ArrayList<Order> getOrders(String email, int Status) {
        ArrayList<Order> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null && email != null) {
                String sql = "select od.OrderID,od.OrdDate,od.shipdate,od.status,od.AccID from Orders as od,Accounts as ac \n"
                        + "where od.AccID=ac.accID and email like ? and od.status = ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, email);
                pst.setInt(2, Status);
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        Order od = new Order(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4), rs.getInt(5));
                        list.add(od);
                    }
                    rs.close();
                    pst.close();
                    cn.close();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
        
    }
    public static ArrayList<Order> getOrders(String email, String from, String to) {
        ArrayList<Order> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null && email != null) {
                String sql = "select od.OrderID,od.OrdDate,od.shipdate,od.status,od.AccID from Orders as od,Accounts as ac\n" +
"where od.AccID=ac.accID and email like ? and od.OrdDate between ? and ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, email);
                pst.setString(2, from);
                pst.setString(3, to);
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        Order od = new Order(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4), rs.getInt(5));
                        list.add(od);
                    }
                    rs.close();
                    pst.close();
                    cn.close();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
        
    }
    public static ArrayList<Order> searchOrders(String email) {
        ArrayList<Order> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null && email != null) {
                String sql = "select od.OrderID,od.OrdDate,od.shipdate,od.status,od.AccID from Orders as od,Accounts as ac \n"
                        + "where od.AccID=ac.accID and email like ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, "%"+email+"%");
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        Order od = new Order(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4), rs.getInt(5));
                        list.add(od);
                    }
                    rs.close();
                    pst.close();
                    cn.close();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }

    public static ArrayList<OrderDetail> getOrderDetail(int orderID) {
        Connection cn = null;
        ArrayList<OrderDetail> list = new ArrayList<>();
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select DetailId,OrderID,PID,PName,price,imgPath,quantity \n"
                        + "from OrderDetails,Plants\n"
                        + "where OrderID = ? and OrderDetails.FID = Plants.PID";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setInt(1, orderID);
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        int detailID = rs.getInt("DetailID");
                        int PlanID = rs.getInt("PID");
                        String PlantName = rs.getString("PName");
                        int price = rs.getInt("price");
                        String imgpath = rs.getString("imgPath");
                        int quantity = rs.getInt("quantity");
                        OrderDetail orddetail = new OrderDetail(detailID, orderID, PlanID, PlantName, price, imgpath, quantity);
                        list.add(orddetail);
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                if (cn != null) {
                    cn.close();
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return list;

    }

    public static boolean insertOrder(String email, HashMap<String, Integer> cart) throws Exception {
        Connection cn = null;
        boolean rs = false;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                int accid = 0;
                int orderid = 0;
                cn.setAutoCommit(false);//tat autocommit
                //lay account bang email
                String sql = "select accID from Accounts where email=?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, email);
                ResultSet rss = pst.executeQuery();
                if (rss != null && rss.next()) {
                    accid = rss.getInt("accID");
                }
                System.out.println("Account ID: " + accid);
                Date d = new Date(System.currentTimeMillis());
                System.out.println("Order Date: " + d);
                sql = "insert into Orders(OrdDate, status, AccID) values(?,?,?)";
                pst = cn.prepareStatement(sql);
                pst.setDate(1, d);
                pst.setInt(2, 1);
                pst.setInt(3, accid);
                pst.executeUpdate();
                sql = "select top 1 OrderID from Orders order by OrderID desc";
                pst = cn.prepareStatement(sql);
                rss = pst.executeQuery();
                if (rss != null && rss.next()) {
                    orderid = rss.getInt("orderID");
                }
                //insert order detail
                System.out.println("Order ID: " + orderid);
                Set<String> pids = cart.keySet();
                for (String pid : pids) {
                    sql = "insert into OrderDetails values(?,?,?)";
                    pst = cn.prepareStatement(sql);
                    pst.setInt(1, orderid);
                    pst.setInt(2, Integer.parseInt(pid.trim()));
                    pst.setInt(3, cart.get(pid));
                    pst.executeUpdate();
                    cn.commit();
                    cn.setAutoCommit(true);
                }
                return true;
            } else {
                System.out.println("Cannot insert order!");
            }
        } catch (Exception e) {
            try {
                if (cn != null) {
                    cn.rollback();
                }
            } catch (SQLException ex) {
                ex.printStackTrace();
            }
            e.printStackTrace();
            rs = false;
        } finally {
            try {
                if (cn != null) {
                    cn.close();
                }
            } catch (Exception ee) {
                ee.printStackTrace();
            }
        }
        return rs;
    }

    public static boolean UpdateStatus(int id) throws ClassNotFoundException, SQLException {
        Connection cn = DBUtils.makeConnection();
        Order od = getOrdersbyID(id);
        if (cn != null) {
            if (od.getStatus() == 1) {
                String sql = "UPDATE Orders\n"
                        + "SET status = ?\n"
                        + "where OrderID = ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setInt(1, 3);
                pst.setInt(2, id);
                if (pst.executeUpdate() == 0) {
                    return false;
                }
            } else if (od.getStatus() == 3) {
                String sql = "UPDATE Orders\n"
                        + "SET status = ?\n"
                        + "where OrderID = ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setInt(1, 1);
                pst.setInt(2, id);
                if (pst.executeUpdate() == 0) {
                    return false;
                }
            } 

        }
        cn.close();

        return true;
    }
}
