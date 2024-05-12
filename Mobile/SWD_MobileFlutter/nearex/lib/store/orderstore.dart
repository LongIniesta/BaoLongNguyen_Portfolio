import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/customer.dart';
import 'package:nearex/model/order.dart';
import 'package:nearex/model/payment.dart';
import 'package:nearex/model/store.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/store/updateorder.dart';

import 'campaignstore.dart';

class OrderStoreScreen extends StatefulWidget {
  OrderStoreScreen({super.key, required this.store});
  Store store;

  @override
  State<StatefulWidget> createState() {
    return OrderStoreScreenState(store);
  }
}

class OrderStoreScreenState extends State<OrderStoreScreen> {
  Store store;
  bool isProccessing = true;
  int idCat = 0;
  List<Order> resultList = [];
  List<Order> listShow = [];
  List<CatCampaign> catCampaign = [];

  @override
  void initState() {
    catCampaign.add(CatCampaign(id: 0, name: 'Tất cả'));
    catCampaign.add(CatCampaign(id: 1, name: 'Hôm nay'));
    catCampaign.add(CatCampaign(id: 2, name: 'Tuần này'));
    catCampaign.add(CatCampaign(id: 3, name: 'Tháng này'));
    catCampaign.add(CatCampaign(id: 4, name: 'Năm nay'));
    catCampaign.add(CatCampaign(id: 5, name: 'Đã hủy'));
    catCampaign.add(CatCampaign(id: 6, name: 'Chờ xác nhận'));
    catCampaign.add(CatCampaign(id: 7, name: 'Đã xác nhận'));
    catCampaign.add(CatCampaign(id: 8, name: 'Đã nhận hàng'));
    loadOrder();
    super.initState();
  }

  OrderStoreScreenState(this.store);
  @override
  Widget build(BuildContext context) {
    return Center(
      child: Stack(
        children: [
          AbsorbPointer(
            absorbing: isProccessing,
            child: Container(
              width: double.maxFinite,
              height: double.maxFinite,
              decoration:
                  const BoxDecoration(color: Color.fromRGBO(233, 248, 249, 1)),
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
                            child: Text('Đơn hàng của ${store.storeName}',
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
                              onTap: () {},
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
                              onTap: () {
                                print('search');
                              },
                              child: const Icon(Icons.search),
                            ),
                            border: InputBorder.none,
                          ),
                          onChanged: (value) {},
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          // Navigator.push(
                          //                     context,
                          //                     MaterialPageRoute(
                          //                         builder: (context) =>
                          //                             AddNewCampaignScreen()));
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
                          children: listShow.map((order) {
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
                                                      UpdateOrder(order: order,store: store,)));
                                        },
                                        child: Icon(Icons.edit, size: 35),
                                      ),
                                    ),
                                    Row(
                                      children: [
                                        if (order.productImg == null)
                                          Image.asset(
                                            'images/product.png',
                                            width: 150,
                                            height: double.maxFinite,
                                            fit: BoxFit.contain,
                                          )
                                        else
                                          Image.network(
                                            order.productImg!,
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
                                                order.productName!,
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
                                                'Khách hàng: ${order.customer!.userName}',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 15),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                'SĐT: ${order.customer!.phone}',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                  fontSize: 15,
                                                ),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                'Đặt vào: ' +
                                                    formatDate(
                                                        order.orderDate!),
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 15),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                'Số lượng: ${order.quantity}',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.w700),
                                              ),
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                convertStatus(order.status!),
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.bold,
                                                        color: convertColor(
                                                            order.status!)),
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
          ),
          if (isProccessing)
            const Align(
                alignment: Alignment.center,
                child: CircularProgressIndicator()),
        ],
      ),
    );
  }

  Future<void> loadOrder() async {
    setState(() {
      isProccessing = true;
      // page = 1;
    });
    resultList.clear();
    // String catID = '';
    // if (idCat != 0) {
    //   catID = '&CategoryId=${idCat}';
    // } else {

    // }
    // String searchQuery = '';
    // if (search != '') {
    //   searchQuery = '&ProductName=${search}';
    // }
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/orders/storeId?storeId=${store.id}&Page=1&PageSize=10');
    try {
      http.Response response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${store.token}'
        },
      );
      if (response.statusCode == 200) {
        String jsonString = response.body;

        Map<String, dynamic> jsonData = json.decode(response.body.toString());

        print('vo' + jsonString);
        // maxpage = jsonData['totalNumberOfPages'];

        List<dynamic> results = jsonData['results'];

        for (var result in results) {
          int id = result['id'];
          DateTime orderDate = DateTime.parse(result['orderDate']);
          int? status = result['status'];
          int quantity = result['quantity'];
          int campaignId = result['campaignId'];

          int customerId = result['customerId'];
          String productName = result['productName'];
          print('messi' + result.toString());
          double unitPrice = double.parse(result['unitPrice'].toString());
          String productImg = result['productImg'];
          String storeName = result['storeName'];

          dynamic customerJson = result['customer'];

          int cusid = customerJson['id'];
          String cusemail = customerJson['email'];
          String cusphone = customerJson['phone'];
          String cusName = customerJson['userName'];

          Customer customer = Customer(
              id: cusid, email: cusemail, phone: cusphone, userName: cusName);
          List<Payments> listPayment = [];
          dynamic res = result['payments'];

          print('messi ' + res.toString());

          for (var re in res) {
            int payId = re['id'];
            String method = re['method'];
            int paystatus = re['status'];
            DateTime time = DateTime.parse(re['time']);
            int payorderId = re['orderId'];
            Payments payments = Payments(
                id: payId,
                method: method,
                status: paystatus,
                time: time,
                orderId: payorderId);
            listPayment.add(payments);
          }

          Order order = Order(
              campaignId: campaignId,
              customer: customer,
              customerId: customerId,
              id: id,
              orderDate: orderDate,
              payments: listPayment,
              productImg: productImg,
              productName: productName,
              quantity: quantity,
              status: status,
              storeName: storeName,
              unitPrice: unitPrice);

          resultList.add(order);
        }
        listShow.addAll(resultList);
        print('da add ${resultList.length}');
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

  String formatDate(DateTime dateTime) {
    String format = '${dateTime.day}/${dateTime.month}/${dateTime.year}';
    return format;
  }

  String convertStatus(int status) {
    if (status == 0) return 'Hủy đơn';
    if (status == 1) return 'Chờ xác nhận';
    if (status == 2) return 'Đã xác nhận';
    if (status == 3) return 'Đã nhận hàng';
    return '';
  }

  Color convertColor(int status) {
    if (status == 0) return const Color.fromARGB(255, 195, 5, 5);
    if (status == 1) return Color.fromARGB(255, 233, 241, 0);
    if (status == 2) return Color.fromARGB(255, 0, 248, 37);
    if (status == 3) return Color.fromARGB(255, 24, 0, 244);
    return const Color.fromARGB(255, 195, 5, 5);
  }

  void loadCat() {
    print('vo');
    print(idCat);
    listShow.clear();
    if (idCat == 1) {
      setState(() {
        for (Order order in resultList) {
          if (order.orderDate!.day == DateTime.now().day &&
              order.orderDate!.month == DateTime.now().month &&
              order.orderDate!.year == DateTime.now().year) listShow.add(order);
        }
      });
    } else if (idCat == 2) {
      setState(() {
        for (Order order in resultList) {
          if (isDateWithinCurrentWeek(order.orderDate!)) listShow.add(order);
        }
      });
    } else if (idCat == 3) {
      setState(() {
        for (Order order in resultList) {
          if (order.orderDate!.month == DateTime.now().month &&
              order.orderDate!.year == DateTime.now().year) listShow.add(order);
        }
      });
    } else if (idCat == 4) {
      setState(() {
        for (Order order in resultList) {
          if (order.orderDate!.year == DateTime.now().year) listShow.add(order);
        }
      });
    }else if (idCat == 5) {
      setState(() {
        for (Order order in resultList) {
          if (order.status == 0) listShow.add(order);
        }
      });
    }
    else if (idCat == 6) {
      setState(() {
        for (Order order in resultList) {
          if (order.status == 1) listShow.add(order);
        }
      });
    } 
    else if (idCat == 7) {
      setState(() {
        for (Order order in resultList) {
          if (order.status == 2) listShow.add(order);
        }
      });
    } 
    else if (idCat == 8) {
      setState(() {
        for (Order order in resultList) {
          if (order.status == 3) listShow.add(order);
        }
      });
    }   else {
      setState(() {
        listShow.addAll(resultList);
      });
    }
    // if (search != '') {
    //   for (Campaign element in resultList) {
    //     if (!element.productName!
    //         .toLowerCase()
    //         .trim()
    //         .contains(search.toLowerCase().trim())) {
    //       print('ec');
    //       if (listShow.contains(element)) {
    //         listShow.remove(element);
    //       }
    //     }
    //   }
    // }
    setState(() {
      listShow = listShow;
    });
  }

  bool isDateWithinCurrentWeek(DateTime testDateTime) {
    DateTime now = DateTime.now();
    DateTime startOfWeek = now.subtract(Duration(days: now.weekday - 1));
    DateTime endOfWeek = now.add(Duration(days: 7 - now.weekday));

    return testDateTime.isAfter(startOfWeek) &&
        testDateTime.isBefore(endOfWeek);
  }
}
