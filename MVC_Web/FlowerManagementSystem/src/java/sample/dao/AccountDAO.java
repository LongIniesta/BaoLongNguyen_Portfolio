/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import sample.dto.Account;
import sample.utils.DBUtils;

/**
 *
 * @author baolo
 */
public class AccountDAO {

    public static Account getAccount(String email, String password) {
        Connection cn = null;
        Account acc = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select accID,email,password,fullname,phone,status,role\n"
                        + "from dbo.Accounts\n"
                        + "where status = 1 and email = ? and password = ? COLLATE Latin1_General_CS_AS";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, email);
                pst.setString(2, password);
                ResultSet rs = pst.executeQuery();
                if (rs != null && rs.next()) {
                    int AccID = rs.getInt("accID");
                    String Email = rs.getString("email");
                    String Password = rs.getString("password");
                    String Fullname = rs.getString("fullname");
                    String Phone = rs.getString("phone");
                    int Status = rs.getInt("status");
                    int Role = rs.getInt("role");
                    acc = new Account(AccID, Email, Password, Fullname, Status, Phone, Role);
                    
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (cn != null) {
                try {
                    cn.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return acc;
    }

    public static Account getAccount(String token) throws Exception {
        Account acc = null;
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                //step 2: viet sql and execute
                String sql = "select accID,email,password,fullname,phone,status,role\n"
                        + "from dbo.Accounts\n"
                        + "where token=? Collate Latin1_General_CS_AS";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, token);
                ResultSet table = pst.executeQuery();
                //step 3: xu li ket qua cua step 2
                if (table != null && table.next()) {
                    int Accid = table.getInt("accID");
                    String Email = table.getString("email");
                    String Password = table.getString("password");
                    String Fullname = table.getString("fullname");
                    String Phone = table.getString("phone");
                    int Status = table.getInt("status");
                    int Role = table.getInt("role");
                    acc = new Account(Accid, Email, Password, Fullname, Status, Phone, Role);
                }//get if
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (cn != null) {
                try {
                    cn.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return acc;
    }

    public static ArrayList<Account> getAccountsByEmail(String email) {
        ArrayList<Account> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select accID,email,password,fullname,phone,status,role\n"
                        + "from dbo.Accounts where email like ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, "%"+email+"%");
                ResultSet rs = pst.executeQuery();
                while (rs != null && rs.next()) {
                    int AccID = rs.getInt("accID");
                    String Email = rs.getString("email");
                    String Password = rs.getString("password");
                    String Fullname = rs.getString("fullname");
                    String Phone = rs.getString("phone");
                    int Status = rs.getInt("status");
                    int Role = rs.getInt("role");
                    Account acc = new Account(AccID, Email, Password, Fullname, Status, Phone, Role);
                    list.add(acc);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (cn != null) {
                try {
                    cn.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return list;
    }

    public static ArrayList<Account> getAccounts() {
        ArrayList<Account> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select accID,email,password,fullname,phone,status,role\n"
                        + "from dbo.Accounts";
                PreparedStatement pst = cn.prepareStatement(sql);
                ResultSet rs = pst.executeQuery();
                while (rs != null && rs.next()) {
                    int AccID = rs.getInt("accID");
                    String Email = rs.getString("email");
                    String Password = rs.getString("password");
                    String Fullname = rs.getString("fullname");
                    String Phone = rs.getString("phone");
                    int Status = rs.getInt("status");
                    int Role = rs.getInt("role");
                    Account acc = new Account(AccID, Email, Password, Fullname, Status, Phone, Role);
                    list.add(acc);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (cn != null) {
                try {
                    cn.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return list;
    }

    public static boolean updateAccountStatus(String email, int status) throws Exception {
        Connection cn = DBUtils.makeConnection();
        if (cn != null) {
            String sql = "UPDATE Accounts\n"
                    + "SET status = ?\n"
                    + "where email  = ?";
            PreparedStatement pst = cn.prepareStatement(sql);
            pst.setInt(1, status);
            pst.setString(2, email);
            if (pst.executeUpdate() == 0) {
                return false;
            }

        }
        cn.close();

        return true;
    }

    public static boolean updateAccount(String email, String newFullname, String newPhone) throws Exception {
        Connection cn = DBUtils.makeConnection();
        if (cn != null) {
            if (accountIsExist(email)) {
                String sql = "UPDATE Accounts\n"
                        + "SET fullname = ?, phone = ?\n"
                        + "where email  = ?";

                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(3, email);

                pst.setString(1, newFullname);
                pst.setString(2, newPhone);
                if (pst.executeUpdate() == 0) {
                    return false;
                }
            } else {
                return false;
            }
        }
        cn.close();
        return true;
    }

    public static boolean accountIsExist(String email) throws SQLException, Exception {
        Connection cn = DBUtils.makeConnection();
        if (cn != null) {
            String sql = "select accID\n"
                    + "from dbo.Accounts\n"
                    + "where email =? COLLATE Latin1_General_CS_AS";
            PreparedStatement pst = cn.prepareStatement(sql);
            //gan input vao cac dau cham hoi.
            pst.setString(1, email);
            ResultSet table = pst.executeQuery();
            if (table != null) {
                if (table.next()) {
                    return true;
                } else {
                    return false;
                }
            }

        }
        cn.close();
        return false;
    }

    public static boolean insertAccount(String email, String password, String fullname, String phone, int status, int role) throws SQLException, Exception {
        int result = 0;

        Connection cn = DBUtils.makeConnection();
        if (cn != null) {
            String sql = "insert dbo.Accounts(email, password, fullname, phone, status, role)\n"
                    + "values (?,?,?,?,?,?)";
            PreparedStatement pst = cn.prepareStatement(sql);
            pst.setString(1, email);
            pst.setString(2, password);
            pst.setString(3, fullname);
            pst.setString(4, phone);
            pst.setInt(5, status);
            pst.setInt(6, role);
            if (!accountIsExist(email)) {
                result = pst.executeUpdate();
            }

        }
        cn.close();

        if (result == 1) {
            return true;
        } else {
            return false;
        }
    }

    public static void updateToken(String token, String email) {
        Account acc = null;
        int rs = 0;
        //step 1: connection
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                //step 2: viet sql and execute
                String sql = "update Accounts set token=? where email=?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, token);
                pst.setString(2, email);
                rs = pst.executeUpdate();
                cn.close();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
