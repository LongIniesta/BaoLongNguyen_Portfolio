/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.servlet;

import java.util.ArrayList;
import sample.dao.PlantDAO;
import sample.dto.Plant;


/**
 *
 * @author baolo
 */
public class test {
    public static void main(String[] args) throws Exception {
       ArrayList<Plant> list = PlantDAO.getPlants("h", "byname");
        for (Plant plant : list) {
            System.out.println(plant.getName());
        }
        
        Plant pl = PlantDAO.getPlant(1);
        System.out.println("test: " + pl.getName());
    }
}
