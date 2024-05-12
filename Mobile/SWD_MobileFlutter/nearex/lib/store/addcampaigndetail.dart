import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../model/campaign.dart';
import '../model/campaigndetail.dart';
import '../model/store.dart';
import 'editcampaign.dart';

class AddCampaignDetailScreen extends StatefulWidget {
  AddCampaignDetailScreen({super.key, required this.cam, required this.store});
  Store store;
  Campaign cam;

  @override
  State<AddCampaignDetailScreen> createState() =>
      _AddCampaignDetailScreenState(cam, store);
}

class _AddCampaignDetailScreenState extends State<AddCampaignDetailScreen> {
  DateTime dateStart = DateTime.now();
  Store store;
  Campaign cam;
  String error = '';
  double discount = 0;
    @override
  void initState() {
    super.initState();
    dateStart = cam.listCampaignDetail!.last.dateApply!.add(const Duration(days: 1));
  }
  int quantity = 0;
  _AddCampaignDetailScreenState(this.cam, this.store);
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
                    Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                            builder: (context) =>
                                EditCampaignScreen(store: store, cam: cam)));
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
                    'Thêm mức giảm giá',
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
                            'Ngày triển khai mức giá',
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
                                      '${dateStart.day}/${dateStart.month}/${dateStart.year}',
                                  border: InputBorder.none,
                                ),
                              ),
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Giá giảm còn',
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
                            child: TextField(
                              keyboardType: TextInputType.number,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Mức giá',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                discount = double.parse(value);
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Số lượng đặt mua tối thiểu',
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
                            child: TextField(
                              keyboardType: TextInputType.number,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Số lượng',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                quantity = int.parse(value);
                              },
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.center,
                child: Text(
                  error,
                  style: GoogleFonts.outfit(color: Colors.red),
                ),
              ),
              Align(
                alignment: Alignment.bottomCenter,
                child: InkWell(
                  onTap: () {
                    print('ádsad');
                    addCamDetail();
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
                      'Thêm',
                      style: GoogleFonts.outfit(
                          fontSize: 20,
                          fontWeight: FontWeight.bold,
                          color: const Color.fromARGB(255, 255, 255, 255)),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void _showDatePicker() {
    showDatePicker(
      context: context,
      initialDate: dateStart,
      firstDate: DateTime(1950),
      lastDate: DateTime(2025),
    ).then((value) {
      setState(() {
        dateStart = value!;
      });
    });
  }

  void addError(String e) {
    if (error != '') {
      error += ' | ';
    }
    error += e;
  }

  void addCamDetail() {
    error = '';
    if (quantity <= 0) {
      addError('Số lượng tối thiểu phải lớn hơn 0');
    }
    if (discount >= cam.listCampaignDetail!.last.discount! || discount <= 0) {
      addError(
          'Giá giảm đợt tiếp theo phải nhỏ hơn ${cam.listCampaignDetail!.last.discount!} vnđ và lớn hon 0');
    }
    if (dateStart.isBefore(cam.listCampaignDetail!.last.dateApply!.add(const Duration(days: 1)))) {
      addError(
          'Giá giảm đợt tiếp theo phải sau ngày ${cam.listCampaignDetail!.last.dateApply!.day}/${cam.listCampaignDetail!.last.dateApply!.month}/${cam.listCampaignDetail!.last.dateApply!.year}');
    }
    if (dateStart.isAfter(cam.endDate!)) {
      addError(
          'Giá giảm đợt tiếp theo phải trước ngày ${cam.endDate!.day}/${cam.endDate!.month}/${cam.endDate!.year}');
    }
    if (error == '') {
      CampaignDetail camadd = CampaignDetail(
          id: 0,
          dateApply: dateStart,
          discount: discount,
          minQuantity: quantity,
          percentDiscount: 0);
          camadd.isSave = false;
      cam.listCampaignDetail!.add(camadd);
      Navigator.pushReplacement(
          context,
          MaterialPageRoute(
              builder: (context) =>
                  EditCampaignScreen(store: store, cam: cam)));
    }
     setState(() {
        error = error;
      });
  }
}
