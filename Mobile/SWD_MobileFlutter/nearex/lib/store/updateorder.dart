import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/order.dart';
import 'package:nearex/model/store.dart';
import 'package:http/http.dart' as http;
class UpdateOrder extends StatefulWidget {
  UpdateOrder({super.key, required this.order, required this.store});
  Order order;
  Store store;
  @override
  State<UpdateOrder> createState() => _UpdateOrderState(order, store);
}

class _UpdateOrderState extends State<UpdateOrder> {
  _UpdateOrderState(this.order, this.store);

  Order order;
  Store store;
  List<StatusOrder> statusOrder = [];
  int statusId = 0;

  @override
  void initState() {
    statusOrder.add(StatusOrder(id: 0, name: 'Đã hùy'));
    statusOrder.add(StatusOrder(id: 1, name: 'Chờ xác nhận'));
    statusOrder.add(StatusOrder(id: 2, name: 'Đã xác nhận'));
    statusOrder.add(StatusOrder(id: 3, name: 'Đã nhận hàng'));
    statusId = order.status!;
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          child: Stack(
            children: [
              if (order.productImg != null)
                Align(
                  alignment: Alignment.topCenter,
                  child: Container(
                      width: double.maxFinite,
                      height: 300,
                      child: Image.network(
                        order.productImg!,
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
                          order.productName.toString(),
                          style: GoogleFonts.sourceSansPro(
                              fontSize: 30, fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        margin: EdgeInsets.only(top: 10, left: 20),
                        child: Text(
                          '${order!.unitPrice}  đ',
                          style: GoogleFonts.outfit(
                              color: Color.fromRGBO(65, 109, 212, 1),
                              fontSize: 30,
                              fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: 200,
                        height: 60,
                        margin: const EdgeInsets.only(left: 20),
                        decoration: const BoxDecoration(
                            shape: BoxShape.rectangle,
                            color: Color.fromARGB(255, 221, 221, 221)),
                        child: Center(
                          child: DropdownButton<String>(
                            underline: Container(
                              height: 0,
                            ),
                            value: statusId.toString(),
                            style: const TextStyle(color: Colors.black),
                            onChanged: (String? newValue) {
                              setState(() {
                                statusId = int.parse(newValue!);
                              });
                            },
                            items: statusOrder.map((e) {
                              return DropdownMenuItem<String>(
                                value: e.id.toString(),
                                child: Text(e.name.toString()),
                              );
                            }).toList(),
                          ),
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
                                'Ngày đặt hàng',
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
                                '${order.orderDate!.day}/${order.orderDate!.month}/${order.orderDate!.year}',
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
                                'Tên khách hàng',
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
                                '${order.customer!.userName}',
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
                                'Số diện thoại',
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
                                '${order.customer!.phone}',
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
                                'Số lượng',
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
                                '${order.quantity}',
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
                                'Phương thức thanh toán',
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
                                order.payments!.first.method!,
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
                  onTap: ()async {
                    
                    await updateSatus();
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
                      'Cập nhật',
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

  Future updateSatus() async {
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/orders/${order.id}');
    Map<String, dynamic> body = {"status": statusId};
    order.status = statusId;
    try {
      http.Response response = await http.put(
        url,
        body: jsonEncode(body),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${store.token}'
        },
      );

      if (response.statusCode == 200) {
        print('Response body: ${response.body}');
        Navigator.pop(context);
      } else {
        setState(() {
        });
      }
    } catch (e) {
      // Xử lý lỗi khi gửi request
      print('Error sending POST request: $e');
    }
  }

  
}

class StatusOrder {
  int id;
  String name;
  StatusOrder({required this.id, required this.name});
}
