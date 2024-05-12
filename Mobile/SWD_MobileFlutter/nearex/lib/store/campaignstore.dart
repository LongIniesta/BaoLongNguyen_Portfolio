import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/campaign.dart';
import 'package:nearex/model/campaigndetail.dart';
import 'package:nearex/model/category.dart';
import 'package:nearex/store/campaigndetail.dart';
import 'package:nearex/store/notificationstore.dart';
import 'package:firebase_storage/firebase_storage.dart' as firebase_storage;
import 'package:http/http.dart' as http;

import '../model/store.dart';
import 'addnewcampaign.dart';

class CapaignStoreScreen extends StatefulWidget {
  CapaignStoreScreen({super.key, required this.store, required this.catList});
  Store store;
  List<CategoryProduct> catList;

  @override
  State<StatefulWidget> createState() {
    return CapaignStoreScreenState(store, catList);
  }
}

class CapaignStoreScreenState extends State<CapaignStoreScreen> {
  CapaignStoreScreenState(this.store, this.catList);
  Store store;
  bool isProccessing = true;
  int maxpage = 0;
  List<Campaign> resultList = [];
  List<Campaign> listShow = [];
  List<CategoryProduct> catList;
  List<CatCampaign> catCampaign = [];
  ScrollController _scrollController = ScrollController();
  int idCat = 0;
  int page = 1;
  String search = '';

  @override
  void initState() {
    catCampaign.add(CatCampaign(id: 0, name: 'Tất cả'));
    catCampaign.add(CatCampaign(id: 1, name: 'Còn hàng'));
    catCampaign.add(CatCampaign(id: 2, name: 'Hết hàng'));
    catCampaign.add(CatCampaign(id: 3, name: 'Đã kết thúc'));
    catCampaign.add(CatCampaign(id: 4, name: 'Chưa bắt đầu'));
    loadCampaign();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    _scrollController.addListener(() async {
      if (_scrollController.position.pixels ==
          _scrollController.position.maxScrollExtent) {
        print('dung');
        if (maxpage > page) {
          setState(() {
            page++;
          });
          //await loadMore();
        }
      }
    });
    return Center(
      child: Container(
        width: double.maxFinite,
        height: double.maxFinite,
        decoration:
            const BoxDecoration(color: Color.fromRGBO(233, 248, 249, 1)),
        child: Stack(
          children: [
            AbsorbPointer(
              absorbing: isProccessing,
              child: Column(
                children: [
                  Container(
                    width: double.maxFinite,
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Container(
                          margin: const EdgeInsets.only(top: 50, left: 20),
                          child: Align(
                            alignment: Alignment.topLeft,
                            child: Text('Xin chao ${store.storeName}',
                                style: GoogleFonts.sourceSansPro(
                                  color: Colors.black,
                                  fontSize: 25,
                                )),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 50, right: 20),
                          child: Align(
                            alignment: Alignment.topRight,
                            child: InkWell(
                              onTap: () {
                                Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                        builder: (context) =>
                                            NotificationStoreScreen()));
                              },
                              child: Image.asset(
                                'images/notificationbig.png',
                                width: 25,
                                height: 25,
                                fit: BoxFit.contain,
                              ),
                            ),
                          ),
                        )
                      ],
                    ),
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Container(
                        margin:
                            const EdgeInsets.only(top: 10, left: 20, right: 5),
                        padding: const EdgeInsets.only(
                          left: 10,
                        ),
                        height: 45,
                        width: 330,
                        decoration: BoxDecoration(
                          color: Colors.white,
                          border: Border.all(
                            width: 0.5,
                            color: Colors.black,
                          ),
                          borderRadius: const BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        child: TextFormField(
                          decoration: InputDecoration(
                            hintText: 'Tim kiem',
                            suffixIcon: GestureDetector(
                              onTap: () async {
                                loadCat();
                              },
                              child: const Icon(Icons.search),
                            ),
                            border: InputBorder.none,
                          ),
                          onChanged: (value) {
                            search = value;
                          },
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) =>
                                      AddNewCampaignScreen(store: store,)));
                        },
                        child: Container(
                          height: 40,
                          width: 40,
                          decoration: BoxDecoration(
                              color: Color.fromARGB(255, 21, 23, 41),
                              borderRadius:
                                  BorderRadius.all(Radius.circular(10))),
                          margin: const EdgeInsets.only(
                              top: 10, left: 5, right: 20),
                          child: Icon(
                            Icons.add,
                            color: const Color.fromARGB(255, 255, 255, 255),
                          ),
                        ),
                      ),
                    ],
                  ),
                  Container(
                    margin: const EdgeInsets.only(top: 10, left: 20, right: 20),
                    height: 50,
                    width: double.maxFinite,
                    child: SingleChildScrollView(
                      scrollDirection: Axis.horizontal,
                      child: Row(
                        children: catCampaign.map((cat) {
                          if (cat.id == idCat) {
                            return Container(
                              margin: EdgeInsets.only(left: 10, right: 10),
                              alignment: Alignment.center,
                              height: 40,
                              width: 70,
                              decoration: const BoxDecoration(
                                borderRadius:
                                    BorderRadius.all(Radius.circular(10)),
                                color: Color.fromRGBO(24, 24, 35, 1),
                              ),
                              child: Text(
                                cat.name.toString(),
                                style: GoogleFonts.sourceSansPro(
                                    color: Colors.white, fontSize: 15),
                              ),
                            );
                          } else {
                            return InkWell(
                              onTap: () {
                                setState(() {
                                  idCat = cat.id!;
                                });
                                loadCat();
                              },
                              child: Container(
                                margin: EdgeInsets.only(left: 10, right: 10),
                                alignment: Alignment.center,
                                height: 40,
                                width: 70,
                                decoration: const BoxDecoration(
                                  borderRadius:
                                      BorderRadius.all(Radius.circular(10)),
                                  color: Color.fromRGBO(255, 255, 255, 1),
                                ),
                                child: Text(
                                  cat.name.toString(),
                                  style: GoogleFonts.sourceSansPro(
                                      color: Color.fromARGB(255, 11, 13, 29),
                                      fontSize: 15),
                                ),
                              ),
                            );
                          }
                        }).toList(),
                      ),
                    ),
                  ),
                  Container(
                      margin: const EdgeInsets.only(
                          top: 10, left: 20, right: 20, bottom: 20),
                      height: 600,
                      width: double.maxFinite,
                      child: SingleChildScrollView(
                        child: Column(
                          children: listShow.map((cam) {
                            return Container(
                              padding: EdgeInsets.all(10),
                              width: double.maxFinite,
                              height: 200,
                              decoration: const BoxDecoration(
                                borderRadius: BorderRadius.all(
                                  Radius.circular(10),
                                ),
                                color: Color.fromARGB(255, 255, 255, 255),
                              ),
                              child: Container(
                                width: double.maxFinite,
                                child: Stack(
                                  children: [
                                    Align(
                                      alignment: Alignment.bottomRight,
                                      child: InkWell(
                                        onTap: () {
                                          Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                  builder: (context) =>
                                                      DetailCampaignScreen(
                                                        campaign: cam,
                                                        store: store,
                                                      )));
                                        },
                                        child: Icon(Icons.edit, size: 35),
                                      ),
                                    ),
                                    Row(
                                      children: [
                                        if (cam.image == null)
                                          Image.asset(
                                            'images/product.png',
                                            width: 150,
                                            height: double.maxFinite,
                                            fit: BoxFit.contain,
                                          )
                                        else
                                          Image.network(
                                            cam.image!,
                                            width: 150,
                                            height: double.maxFinite,
                                            fit: BoxFit.contain,
                                          ),
                                        Container(
                                          margin: EdgeInsets.only(left: 10),
                                          width: 200,
                                          child: Column(
                                            crossAxisAlignment:
                                                CrossAxisAlignment.start,
                                            mainAxisAlignment:
                                                MainAxisAlignment.center,
                                            children: [
                                              Text(
                                                '${cam.startDate!.day}/${cam.startDate!.month}/${cam.startDate!.year} - ${cam.endDate!.day}/${cam.endDate!.month}/${cam.endDate!.year}',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 15),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                cam.productName.toString(),
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.bold),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                'HSD: ${cam.exp!.day}/${cam.exp!.month}/${cam.exp!.year}',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                  fontSize: 15,
                                                ),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                '${cam.price} đ',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.w700),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              if (cam.startDate!
                                                  .isAfter(DateTime.now()))
                                                Text(
                                                  'Chưa triển khai',
                                                  style:
                                                      GoogleFonts.sourceSansPro(
                                                          fontSize: 20,
                                                          fontWeight:
                                                              FontWeight.bold,
                                                          color: Color.fromARGB(
                                                              255,
                                                              247,
                                                              131,
                                                              131)),
                                                )
                                              else if (cam.endDate!
                                                  .isBefore(DateTime.now()))
                                                Text(
                                                  'Kết Thúc',
                                                  style:
                                                      GoogleFonts.sourceSansPro(
                                                          fontSize: 20,
                                                          fontWeight:
                                                              FontWeight.bold,
                                                          color: Color.fromARGB(
                                                              255,
                                                              247,
                                                              131,
                                                              131)),
                                                )
                                              else if (cam.quantity == 0)
                                                Text(
                                                  'Hết Hàng',
                                                  style:
                                                      GoogleFonts.sourceSansPro(
                                                          fontSize: 20,
                                                          fontWeight:
                                                              FontWeight.bold,
                                                          color: Color.fromARGB(
                                                              255,
                                                              247,
                                                              131,
                                                              131)),
                                                )
                                              else if (cam.quantity! > 0)
                                                Text(
                                                  'Còn hàng',
                                                  style:
                                                      GoogleFonts.sourceSansPro(
                                                          fontSize: 20,
                                                          fontWeight:
                                                              FontWeight.bold,
                                                          color: Color.fromARGB(
                                                              255,
                                                              110,
                                                              148,
                                                              244)),
                                                )
                                            ],
                                          ),
                                        )
                                      ],
                                    ),
                                  ],
                                ),
                              ),
                            );
                          }).toList(),
                        ),
                      ))
                ],
              ),
            ),
            if (isProccessing)
              Align(
                  alignment: Alignment.center,
                  child: const CircularProgressIndicator()),
          ],
        ),
      ),
    );
  }

  Future<Uint8List?> getImageFromFirebase(String imageUrl) async {
    try {
      // Tạo một Firebase Storage instance
      // firebase_storage.FirebaseStorage storage =
      //     firebase_storage.FirebaseStorage.instance;

      // // Lấy reference đến tệp hình ảnh trong Firebase Storage
      // firebase_storage.Reference ref = storage.refFromURL(imageUrl);

      // // Tải xuống URL của tệp hình ảnh
      // String downloadUrl = await ref.getDownloadURL();

      // Tải xuống dữ liệu hình ảnh
      http.Response response = await http.get(Uri.parse(imageUrl));

      // Kiểm tra xem tải xuống thành công hay không
      if (response.statusCode == 200) {
        // Chuyển đổi dữ liệu hình ảnh thành Uint8List
        Uint8List imageData = response.bodyBytes;

        // Tạo một Widget hình ảnh từ dữ liệu Uint8List
        return imageData;
      } else {
        // Trả về một hình ảnh giả để hiển thị khi không tải xuống được
        return null;
      }
    } catch (e) {
      // Xử lý lỗi nếu có
      print(e.toString());
      return null;
    }
  }

  void loadCat() {
    print('vo');
    print(idCat);
    listShow.clear();
    if (idCat == 4) {
      setState(() {
        for (Campaign cam in resultList) {
          if (cam.startDate!.isAfter(DateTime.now())) listShow.add(cam);
        }
      });
    } else if (idCat == 3) {
      for (Campaign cam in resultList) {
        if (cam.endDate!.isBefore(DateTime.now())) listShow.add(cam);
      }
    } else if (idCat == 2) {
      for (Campaign cam in resultList) {
        if (cam.quantity == 0 &&
            !cam.startDate!.isAfter(DateTime.now()) &&
            !cam.endDate!.isBefore(DateTime.now())) listShow.add(cam);
      }
    } else if (idCat == 1) {
      for (Campaign cam in resultList) {
        if (cam.quantity != 0 &&
            !cam.startDate!.isAfter(DateTime.now()) &&
            !cam.endDate!.isBefore(DateTime.now())) listShow.add(cam);
      }
    } else {
      setState(() {
        listShow.addAll(resultList);
      });
    }
    if (search != '') {
      for (Campaign element in resultList) {
        
        if (!element.productName!.toLowerCase().trim().contains(search.toLowerCase().trim())) {
          print('ec');
          if (listShow.contains(element)) {
            listShow.remove(element);
          }
        }
      }
    }
    setState(() {
        listShow = listShow;
      });
  }

  Future<void> loadCampaign() async {
    setState(() {
      isProccessing = true;
      page = 1;
    });
    resultList.clear();
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/campaigns/store?storeId=${store.id}&Page=${page}&PageSize=30&SortType=1&ColName=EndDate');
    try {
      http.Response response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${store.token}'
        },
      );

      print(response.statusCode);

      if (response.statusCode == 200) {
        String jsonString = response.body;
        print(response.body
            .substring(response.body.length - 500, response.body.length));
        Map<String, dynamic> jsonData = json.decode(response.body.toString());
        maxpage = jsonData['totalNumberOfPages'];

        List<dynamic> results = jsonData['results'];
        for (var result in results) {
          int id = result['id'];
          DateTime startDate = DateTime.parse(result['startDate']);
          DateTime endDate = DateTime.parse(result['endDate']);
          int status = result['status'];
          DateTime exp = DateTime.parse(result['exp']);
          int productId = result['productId'];
          int quantity = result['quantity'];

          dynamic product = result['product'];

          String productName = product['productName'];
          String productImg = product['productImg'];

          dynamic campaigndetails = result['campaignDetails'];
          List<CampaignDetail> listCampaignDetail = [];
          double price = double.parse(product['price'].toString());

          for (var campaigndetail in campaigndetails) {
            int id = campaigndetail['id'];
            DateTime dateApply = DateTime.parse(campaigndetail['dateApply']);
            print('flag' + dateApply.toString());
            double percentDiscount = 0; //campaigndetail['percentDiscount'];

            double discount =
                double.parse(campaigndetail['discount'].toString());

            int minQuantity = campaigndetail['minQuantity'];

            CampaignDetail campaignDetailr = CampaignDetail(
                dateApply: dateApply,
                discount: discount,
                id: id,
                minQuantity: minQuantity,
                percentDiscount: percentDiscount);
            listCampaignDetail.add(campaignDetailr);
          }

        //  Uint8List? image = await getImageFromFirebase(productImg);
          Campaign campaign = Campaign(
            endDate: endDate,
            exp: exp,
            id: id,
            image: productImg,
            listCampaignDetail: listCampaignDetail,
            price: price,
            productId: productId,
            productName: productName,
            quantity: quantity,
            startDate: startDate,
            status: status,
          );
          resultList.add(campaign);
          print(campaign.productName);
        }

        listShow.addAll(resultList);
      } else {
        setState(() {
          isProccessing = false;
        });
      }
    } catch (e) {
      setState(() {
        isProccessing = false;
      });
    }
    setState(() {
      isProccessing = false;
    });
  }
}

class CatCampaign {
  int id;
  String name;
  CatCampaign({required this.id, required this.name});
}
