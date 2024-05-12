/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.dto;

/**
 *
 * @author baolo
 */
public class OrderDetail {
    private int detailID;
    private int orderID;
    private int fID;
    private String fName;
    private int price;
    private String ImgPath;
    private int quantity;

    public OrderDetail() {
    }

    public OrderDetail(int detailID, int orderID, int fID, String fName, int price, String ImgPath, int quantity) {
        this.detailID = detailID;
        this.orderID = orderID;
        this.fID = fID;
        this.fName = fName;
        this.price = price;
        this.ImgPath = ImgPath;
        this.quantity = quantity;
    }

    public int getDetailID() {
        return detailID;
    }

    public void setDetailID(int detailID) {
        this.detailID = detailID;
    }

    public int getOrderID() {
        return orderID;
    }

    public void setOrderID(int orderID) {
        this.orderID = orderID;
    }

    public int getfID() {
        return fID;
    }

    public void setfID(int fID) {
        this.fID = fID;
    }

    public String getfName() {
        return fName;
    }

    public void setfName(String fName) {
        this.fName = fName;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public String getImgPath() {
        return ImgPath;
    }

    public void setImgPath(String ImgPath) {
        this.ImgPath = ImgPath;
    }

    public int getQuantity() {
        return quantity;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

   
}
