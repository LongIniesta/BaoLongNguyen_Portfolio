/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package sample.filter;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import sample.utils.DBUtils;
import sample.utils.DBUtils1;

/**
 *
 * @author baolo
 */
public class test {
    public static void main(String[] args) throws ClassNotFoundException {
        Connection cn = null;
       
        try {
            cn = DBUtils1.getConnection2();
            if (cn != null) {
                String sql = "select * from [User] where userId = ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setLong(1, 1);
                ResultSet rs = pst.executeQuery();
                if (rs != null && rs.next()) {
                    System.out.println(rs.getString(2));
                }
                
            }
        } catch (SQLException e) {
        } finally {
            if (cn != null) {
                try {
                    cn.close();
                } catch (SQLException e) {
                }
            }
        }
        System.out.println("1");
        
    }
}
