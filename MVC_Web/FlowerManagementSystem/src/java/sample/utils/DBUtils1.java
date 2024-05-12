/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package sample.utils;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

/**
 *
 * @author Admin
 */
public class DBUtils1 {
    public static Connection getConnection2() {
        String url = "jdbc:sqlserver://localhost;databaseName=HereWeGo;user=sa;password=12345";
        Connection con = null;
        try {
            //Loading a driver
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
            //Creating a connection
            con = DriverManager.getConnection(url);
        } catch (ClassNotFoundException | SQLException ex) {
        }
        return con;
    }
    public static Connection makeConnection() throws ClassNotFoundException, SQLException{
         Connection cn=null;
        String IP="localhost";
        String instanceName="LAPTOP-A7H0KSVJ";
        String port="1433";
        String uid="sa";
        String pwd="12345";
        String db="HereWeGo";
 String driver="com.microsoft.sqlserver.jdbc.SQLServerDriver";
        String url="jdbc:sqlserver://" +IP+"\\"+ instanceName+":"+port
                 +";databasename="+db+";user="+uid+";password="+pwd;
        Class.forName(driver);
        cn = DriverManager.getConnection(url);
        return cn;
    }
}
