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
import sample.dto.Plant;
import sample.utils.DBUtils;

/**
 *
 * @author baolo
 */
public class PlantDAO {
    public static ArrayList<Plant> getPlants(String keyword, String searchby) {
        ArrayList<Plant> list = new ArrayList<>();
        Connection cn;
        try {
            cn = DBUtils.makeConnection();
            if (cn != null && searchby != null) {
                String sql = "select PID, PName, price, imgPath, description, status, Plants.CateID as 'CateID', CateName\n"
                        + "from Plants join Categories on Plants.CateID=Categories.CateID\n";
                if(searchby.equalsIgnoreCase("byname"))
                    sql = sql + "where Plants.PName like ?";
                else sql = sql + "where CateName like ?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setString(1, "%"+keyword + "%");
                ResultSet rs = pst.executeQuery();
                if(rs != null){
                    while (rs.next()){
                        int id = rs.getInt("PID");
                        String Name = rs.getString("PName");
                        int price = rs.getInt("price");
                        String imgpath = rs.getString("imgPath");
                        String description = rs.getString("description");
                        int status = rs.getInt("status");
                        int cateid = rs.getInt("CateID");
                        String catename = rs.getString("CateName");
                        Plant plant = new Plant(id, Name, price, imgpath, description, status, cateid, catename);
                        list.add(plant);
                    }
                }
            }
        } catch (Exception e){e.printStackTrace();}
        return list;
    }
    public static Plant getPlant(int pid) throws Exception{
        Plant p = null;
        Connection cn = null;
        try{
            cn = DBUtils.makeConnection();
            if (cn != null){
                String sql = "select PID,PName,price,imgPath,description,status,Plants.CateID as cateID,CateName\n"
                        +"from Plants, Categories\n"
                        +"where Plants.CateID=Categories.CateID and PID=?";
                PreparedStatement pst = cn.prepareStatement(sql);
                pst.setInt(1, pid);
                ResultSet rs = pst.executeQuery();
                if (rs != null && rs.next()){
                    pid = rs.getInt("PID");
                    String pname = rs.getString("PName");
                    int price = rs.getInt("price");
                    String imgpath = rs.getString("imgPath");
                    String des = rs.getString("description");
                    int status = rs.getInt("status");
                    int cateid = rs.getInt("cateID");
                    String catename = rs.getString("CateName");
                    p = new Plant(pid, pname, price, imgpath, des, status, cateid, catename);
                }    
            }
        } catch (Exception ex){
            ex.printStackTrace();
        }
        finally{
            try{
               
                if(cn!= null) cn.close();
                
            } catch (Exception e){
                e.printStackTrace();
            }
        }
        return p;
    }
}
