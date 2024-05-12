import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/campaign.dart';
import 'package:nearex/store/editcampaign.dart';

import '../model/campaigndetail.dart';
import '../model/store.dart';

class DetailCampaignScreen extends StatefulWidget {
  DetailCampaignScreen({super.key, required this.campaign, required this.store});
  Campaign campaign;
  Store store;
  @override
  State<StatefulWidget> createState() {
    return DetailCampaignScreenState(campaign, store);
  }
}

class DetailCampaignScreenState extends State<DetailCampaignScreen> {
  DetailCampaignScreenState( this.cam, this.store);
  Store store;
  Campaign cam;
  CampaignDetail? camDetail;
  @override
  void initState() {
    cam.listCampaignDetail!.forEach((camdt){
      if (camdt.dateApply!.isBefore(DateTime.now())){
        camDetail = camdt;
      }
    });

    if (camDetail == null){
      camDetail = cam.listCampaignDetail!.first;
    }
    super.initState();
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          child: Stack(
            children: [
               if (cam.image != null)
                Align(
                  alignment: Alignment.topCenter,
                  child: Container(
                      width: double.maxFinite,
                      height: 300,
                      child: Image.network(
                        cam.image!,
                        fit: BoxFit.cover,
                      )),
                )
              else
                Align(
                  alignment: Alignment.topCenter,
                  child: Container(
                      width: double.maxFinite,
                      height: 300,
                      child: Image.asset(
                        'images/product.png',
                        width: 150,
                        height: double.maxFinite,
                        fit: BoxFit.contain,
                      )),
                ),
              Align(
                alignment: Alignment.topLeft,
                child: InkWell(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: Container(
                    margin: EdgeInsets.only(top: 50, left: 10),
                    width: 50,
                    height: 50,
                    decoration: BoxDecoration(
                        shape: BoxShape.circle,
                        color: Color.fromARGB(95, 206, 206, 206)),
                    child: Icon(
                      Icons.chevron_left,
                      color: Colors.black,
                      size: 40,
                    ),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.bottomCenter,
                child: Container(
                  height: 650,
                  width: double.maxFinite,
                  decoration: BoxDecoration(
                      borderRadius:
                          BorderRadius.vertical(top: Radius.circular(25)),
                      color: const Color.fromARGB(255, 243, 243, 243)),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Container(
                        width: double.maxFinite,
                        margin: EdgeInsets.only(top: 20, left: 20, right: 20),
                        child: Text(
                          cam.productName.toString(),
                          style: GoogleFonts.sourceSansPro(
                              fontSize: 30, fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        margin: EdgeInsets.only(top: 10, left: 20),
                        child: Text(
                          '${camDetail!.discount}  đ',
                          style: GoogleFonts.outfit(
                              color: Color.fromRGBO(65, 109, 212, 1),
                              fontSize: 30,
                              fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        margin: EdgeInsets.only(left: 20),
                        child: Text(
                          '${cam.price} đ',
                          style: GoogleFonts.outfit(
                              color: Color.fromRGBO(144, 170, 231, 1),
                              decoration: TextDecoration.lineThrough,
                              decorationColor: Color.fromRGBO(103, 103, 103, 1),
                              decorationThickness: 1,
                              fontSize: 25,
                              fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        child: Row(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'Chuong trinh ap dung tu:',
                                style: GoogleFonts.outfit(
                                  fontSize: 17,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 20),
                              width: 170,
                              child: Text(
                                '${cam.startDate!.day}/${cam.startDate!.month}/${cam.startDate!.year} - ${cam.endDate!.day}/${cam.endDate!.month}/${cam.endDate!.year}',
                                style: GoogleFonts.outfit(
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        child: Row(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'Han su dung san pham: ',
                                style: GoogleFonts.outfit(
                                  fontSize: 17,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 20),
                              width: 170,
                              child: Text(
                                '${cam.exp!.day}/${cam.exp!.month}/${cam.exp!.year}',
                                style: GoogleFonts.outfit(
                                  color: Colors.red,
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        child: Row(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'So luong dat mua toi thieu:',
                                style: GoogleFonts.outfit(
                                  fontSize: 17,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 20),
                              width: 170,
                              child: Text(
                                '${camDetail!.minQuantity}',
                                style: GoogleFonts.outfit(
                                  color: const Color.fromARGB(255, 0, 0, 0),
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        child: Row(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'So luong con lai:',
                                style: GoogleFonts.outfit(
                                  fontSize: 17,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 20),
                              width: 170,
                              child: Text(
                                '${cam.quantity}',
                                style: GoogleFonts.outfit(
                                  color: const Color.fromARGB(255, 0, 0, 0),
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        child: Row(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'So luong da duoc dat:',
                                style: GoogleFonts.outfit(
                                  fontSize: 17,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 20),
                              width: 170,
                              child: Text(
                                'dang tinh',
                                style: GoogleFonts.outfit(
                                  color: const Color.fromARGB(255, 0, 0, 0),
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Align(
                alignment: Alignment.bottomCenter,
                child: InkWell(
                  onTap: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => EditCampaignScreen(store: store
                            , cam: cam)));
                  },
                  child: Container(
                    margin: EdgeInsets.only(bottom: 20),
                    alignment: Alignment.center,
                    height: 50,
                    width: 300,
                    decoration: const BoxDecoration(
                      borderRadius: BorderRadius.all(Radius.circular(10)),
                      color: Color.fromRGBO(82, 127, 231, 1),
                    ),
                    child: Text(
                      'Chỉnh sửa',
                      style: GoogleFonts.outfit(
                        fontSize: 20,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
