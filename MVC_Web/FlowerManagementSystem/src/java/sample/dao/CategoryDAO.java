/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import sample.dto.Category;
import sample.utils.DBUtils;

/**
 *
 * @author baolo
 */
public class CategoryDAO {
    public static ArrayList<Category> getCategories(){
         ArrayList<Category> list = new ArrayList<>();
        Connection cn = null;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null) {
                String sql = "select * from Categories";
                PreparedStatement pst = cn.prepareStatement(sql);
                
                ResultSet rs = pst.executeQuery();
                if (rs != null) {
                    while (rs.next()) {
                        Category ca = new Category(rs.getInt(1), rs.getString(2));
                        list.add(ca);
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
    
    
}
