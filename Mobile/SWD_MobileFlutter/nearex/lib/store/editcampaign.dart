import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/campaigndetail.dart';
import 'package:nearex/store/addcampaigndetail.dart';
import 'package:http/http.dart' as http;
import '../model/campaign.dart';
import '../model/store.dart';

class EditCampaignScreen extends StatefulWidget {
  EditCampaignScreen({super.key, required this.cam, required this.store});
  Store store;
  Campaign cam;
  @override
  State<EditCampaignScreen> createState() =>
      _EditCampaignScreenState(store, cam);
}

class _EditCampaignScreenState extends State<EditCampaignScreen> {
  DateTime dateEnd = DateTime.now();
  Store store;
  Campaign cam;
  _EditCampaignScreenState(this.store, this.cam);

  @override
  void initState() {
    super.initState();
    dateEnd = cam.endDate!;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          color: Color.fromRGBO(233, 248, 249, 1),
          child: Stack(
            children: [
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
                alignment: Alignment.topCenter,
                child: Container(
                  margin: EdgeInsets.only(top: 65),
                  child: Text(
                    'Chỉnh sửa đợt giảm giá',
                    style: GoogleFonts.outfit(
                        fontSize: 25,
                        fontWeight: FontWeight.bold,
                        color: Color.fromARGB(255, 15, 14, 36)),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.center,
                child: Container(
                  width: double.maxFinite,
                  height: 650,
                  child: SingleChildScrollView(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Container(
                          margin: EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Ngày kết thúc',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: EdgeInsets.only(left: 20, right: 20),
                            padding: EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: InkWell(
                              onTap: () {
                                _showDatePicker();
                              },
                              child: TextField(
                                enabled: false,
                                keyboardType: TextInputType.phone,
                                cursorColor: Colors.black,
                                decoration: InputDecoration(
                                  hintText:
                                      '${dateEnd.day}/${dateEnd.month}/${dateEnd.year}',
                                  border: InputBorder.none,
                                ),
                              ),
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(
                              top: 10, left: 10, right: 10, bottom: 20),
                          height: 600,
                          width: double.maxFinite,
                          child: SingleChildScrollView(
                            child: Column(
                              children: cam.listCampaignDetail!.map((camdt) {
                                if (!camdt.isSave) {
                                  return Container(
                                    margin: EdgeInsets.all(20),
                                    padding: EdgeInsets.all(10),
                                    width: double.maxFinite,
                                    decoration: BoxDecoration(
                                        color:
                                            Color.fromARGB(255, 231, 255, 124),
                                        borderRadius:
                                            BorderRadius.circular(10)),
                                    child: Column(
                                      children: [
                                        Container(
                                          width: double.maxFinite,
                                          child: Row(
                                            children: [
                                              Container(
                                                margin:
                                                    EdgeInsets.only(left: 20),
                                                width: 200,
                                                child: Text(
                                                  'Bắt đầu áp dụng từ',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                width: 100,
                                                child: Text(
                                                  '${camdt.dateApply!.day}/${camdt.dateApply!.month}/${camdt.dateApply!.year}',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
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
                                                margin: EdgeInsets.only(
                                                    left: 20, top: 10),
                                                width: 200,
                                                child: Text(
                                                  'Giá giảm còn',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                margin:
                                                    EdgeInsets.only(top: 10),
                                                width: 100,
                                                child: Text(
                                                  '${camdt.discount} đ',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
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
                                                margin: EdgeInsets.only(
                                                    left: 20, top: 10),
                                                width: 200,
                                                child: Text(
                                                  'Số lượng đặt mua tối thiểu',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                margin:
                                                    EdgeInsets.only(top: 10),
                                                width: 100,
                                                child: Text(
                                                  '${camdt.minQuantity}',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
                                                    fontSize: 15,
                                                  ),
                                                ),
                                              ),
                                            ],
                                          ),
                                        ),
                                      ],
                                    ),
                                  );
                                } else {
                                  return Container(
                                    margin: EdgeInsets.all(20),
                                    padding: EdgeInsets.all(10),
                                    width: double.maxFinite,
                                    decoration: BoxDecoration(
                                        color: const Color.fromARGB(
                                            255, 175, 204, 255),
                                        borderRadius:
                                            BorderRadius.circular(10)),
                                    child: Column(
                                      children: [
                                        Container(
                                          width: double.maxFinite,
                                          child: Row(
                                            children: [
                                              Container(
                                                margin:
                                                    EdgeInsets.only(left: 20),
                                                width: 200,
                                                child: Text(
                                                  'Bắt đầu áp dụng từ',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                width: 100,
                                                child: Text(
                                                  '${camdt.dateApply!.day}/${camdt.dateApply!.month}/${camdt.dateApply!.year}',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
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
                                                margin: EdgeInsets.only(
                                                    left: 20, top: 10),
                                                width: 200,
                                                child: Text(
                                                  'Giá giảm còn',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                margin:
                                                    EdgeInsets.only(top: 10),
                                                width: 100,
                                                child: Text(
                                                  '${camdt.discount} đ',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
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
                                                margin: EdgeInsets.only(
                                                    left: 20, top: 10),
                                                width: 200,
                                                child: Text(
                                                  'Số lượng đặt mua tối thiểu',
                                                  style: GoogleFonts.outfit(
                                                    fontSize: 17,
                                                    fontWeight: FontWeight.bold,
                                                  ),
                                                ),
                                              ),
                                              Container(
                                                margin:
                                                    EdgeInsets.only(top: 10),
                                                width: 100,
                                                child: Text(
                                                  '${camdt.minQuantity}',
                                                  style: GoogleFonts.outfit(
                                                    color: const Color.fromARGB(
                                                        255, 0, 0, 0),
                                                    fontSize: 15,
                                                  ),
                                                ),
                                              ),
                                            ],
                                          ),
                                        ),
                                      ],
                                    ),
                                  );
                                }
                              }).toList(),
                            ),
                          ),
                        )
                      ],
                    ),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.bottomCenter,
                child: InkWell(
                  onTap: () async{
                    
                    await updateCampaign();
                  },
                  child: Container(
                    margin: EdgeInsets.only(bottom: 10),
                    alignment: Alignment.center,
                    height: 50,
                    width: 300,
                    decoration: BoxDecoration(
                        color: const Color.fromARGB(255, 117, 191, 252),
                        borderRadius: BorderRadius.circular(10)),
                    child: Text(
                      'Lưu',
                      style: GoogleFonts.outfit(
                          fontSize: 20,
                          fontWeight: FontWeight.bold,
                          color: const Color.fromARGB(255, 255, 255, 255)),
                    ),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.bottomRight,
                child: InkWell(
                  onTap: () {
                    Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                            builder: (context) => AddCampaignDetailScreen(
                                  cam: cam,
                                  store: store,
                                )));
                  },
                  child: Container(
                    margin: EdgeInsets.only(right: 10, bottom: 90),
                    alignment: Alignment.center,
                    height: 50,
                    width: 50,
                    decoration: BoxDecoration(
                      shape: BoxShape.circle,
                      color: Colors.black,
                    ),
                    child: Icon(
                      Icons.add,
                      color: Colors.white,
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

  void _showDatePicker() {
    showDatePicker(
      context: context,
      initialDate: dateEnd,
      firstDate: DateTime(1950),
      lastDate: DateTime(2025),
    ).then((value) {
      setState(() {
        dateEnd = value!;
      });
    });
  }

  String getStringDate(DateTime date){
    String monthstr =date.month.toString();
      if (date.month < 10 ) monthstr = '0${date.month}';

      String daystr =date.day.toString();
      if (date.day < 10 ) daystr = '0${date.day}';


      String dateString = '${date.year}-$monthstr-$daystr';
      return dateString;
  }

  Future<void> updateCampaign() async {
    if (cam.endDate != dateEnd) {
      cam.endDate = dateEnd;
      String dateString = getStringDate(cam.endDate!);

      var url = Uri.parse(
            'https://swd-nearex.azurewebsites.net/api/campaigns/${cam.id}');
        Map<String, dynamic> body = {
          "endDate": dateString,
          "quantity": cam.quantity,
          "createCampaignDetailRequest": null
        };


        print('Bearer '+ store.token!);
        print(cam.id);
        print(body.toString());


        try {
          http.Response response = await http.put(
            url,
            headers: {
              'Content-Type': 'application/json',
              'Authorization': 'Bearer ${store.token}'
            },
            body: jsonEncode(body),
          );

          if (response.statusCode == 200) {
            // Request thành công, xử lý response
            print('Response body: ${response.body}');
            // ignore: use_build_context_synchronously
          } else {
            print(response.statusCode.toString());
          }
        } catch (e) {
          // Xử lý lỗi khi gửi request
          print('Error sending POST request: $e');
        }
    }

    List<CampaignDetail> listAdd = [];
    for (CampaignDetail camdt in cam.listCampaignDetail!) {
      if (!camdt.isSave) {
        listAdd.add(camdt);
      }
    }
    if (listAdd.isEmpty) {
      return;
    } else {
      for (CampaignDetail camdtAdd in listAdd) {
        
        String endDate = getStringDate(cam.endDate!);
        String dateApply = getStringDate(camdtAdd.dateApply!);
        var url = Uri.parse(
            'https://swd-nearex.azurewebsites.net/api/campaigns/${cam.id}');
        Map<String, dynamic> body = {
          "endDate": endDate,
          "quantity": cam.quantity,
          "createCampaignDetailRequest": {
            "dateApply": dateApply,
            "percentDiscount": (100-(camdtAdd.discount! / cam.price!*100)).ceil(),
            "minQuantity": camdtAdd.minQuantity
          }
        };
        print('Bearer '+ store.token!);
        print(cam.id);
        print(body.toString());

        try {
          http.Response response = await http.put(
            url,
            headers: {
              'Content-Type': 'application/json',
              'Authorization': 'Bearer ${store.token}'
            },
            body: jsonEncode(body),
          );

          if (response.statusCode == 200) {
            // Request thành công, xử lý response
            print('Response body: ${response.body}');
            cam.listCampaignDetail![cam.listCampaignDetail!.indexOf(camdtAdd)].isSave = true;
            // ignore: use_build_context_synchronously
          } else {
            print(response.statusCode.toString());
          }
        } catch (e) {
          // Xử lý lỗi khi gửi request
          print('Error sending POST request: $e');
        }
      }
      setState(() {
        cam.listCampaignDetail = cam.listCampaignDetail;
      });
    }
  }
}
