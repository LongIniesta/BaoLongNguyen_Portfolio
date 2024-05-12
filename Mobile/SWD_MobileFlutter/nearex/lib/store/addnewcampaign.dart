import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/model/campaign.dart';
import '../model/store.dart';

// ignore: must_be_immutable
class AddNewCampaignScreen extends StatefulWidget {
  AddNewCampaignScreen({super.key, required this.store});
  Store store;
  @override
  // ignore: no_logic_in_create_state
  State<AddNewCampaignScreen> createState() =>
      _AddNewCampaignScreenState(store);
}

class _AddNewCampaignScreenState extends State<AddNewCampaignScreen> {
  DateTime dateStart = DateTime.now();
  DateTime dateEnd = DateTime.now();
  DateTime dexp = DateTime.now();
  bool isProccessing = false;
  List<ProductItem> listProductItem = [];
  Store store;
  int productid = 0;
  double cmapaignPrice = 0;
  String error = '';
  double discount = 0;
  int minQuantity = 0;
  int quantity = 0;

  @override
  void initState() {
    super.initState();
    loadProduct();
  }

  _AddNewCampaignScreenState(this.store);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          AbsorbPointer(
            absorbing: isProccessing,
            child: Center(
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
                          'Tạo đợt giảm giá',
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
                                  'Sản phẩm',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              if (listProductItem.isNotEmpty)
                                Align(
                                  alignment: Alignment.center,
                                  child: Container(
                                    width: double.maxFinite,
                                    margin:
                                        EdgeInsets.only(left: 20, right: 20),
                                    padding: EdgeInsets.only(left: 10),
                                    decoration: BoxDecoration(
                                      border: Border.all(
                                          color: Colors.black, width: 1),
                                      borderRadius: BorderRadius.circular(10),
                                      color: Colors.white,
                                    ),
                                    child: DropdownButton<String>(
                                      underline: Container(
                                        height: 0,
                                      ),
                                      value: productid.toString(),
                                      style:
                                          const TextStyle(color: Colors.black),
                                      onChanged: (String? newValue) {
                                        setState(() {
                                          productid = int.parse(newValue!);
                                          cmapaignPrice = listProductItem
                                              .where((element) =>
                                                  element.id == productid)
                                              .first
                                              .price;
                                        });
                                      },
                                      items: listProductItem.map((e) {
                                        return DropdownMenuItem<String>(
                                          value: e.id.toString(),
                                          child: Text(e.name.toString()),
                                        );
                                      }).toList(),
                                    ),
                                  ),
                                ),
                              Container(
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Giá gốc: {$cmapaignPrice} vnđ',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                      color: Colors.red),
                                ),
                              ),
                              Container(
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Hạn sử dụng sản phẩm',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  child: InkWell(
                                    onTap: () {
                                      _showDateExpPicker();
                                    },
                                    child: TextField(
                                      enabled: false,
                                      keyboardType: TextInputType.phone,
                                      cursorColor: Colors.black,
                                      decoration: InputDecoration(
                                        hintText:
                                            '${dexp.day}/${dexp.month}/${dexp.year}',
                                        border: InputBorder.none,
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                              Container(
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Ngày triển khai mức giá',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  child: InkWell(
                                    onTap: () {
                                      _showDateStartPicker();
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
                                  'Ngày kết thúc',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  child: InkWell(
                                    onTap: () {
                                      _showDateEndPicker();
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
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Giá giảm còn',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
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
                                      discount = double.parse(value.toString());
                                    },
                                  ),
                                ),
                              ),
                              Container(
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Số lượng đặt mua tối thiểu',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
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
                                      minQuantity = int.parse(value.toString());
                                    },
                                  ),
                                ),
                              ),
                              Container(
                                margin: EdgeInsets.only(top: 10, left: 20),
                                child: Text(
                                  'Số lượng hàng còn',
                                  style: GoogleFonts.outfit(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold),
                                ),
                              ),
                              Align(
                                alignment: Alignment.center,
                                child: Container(
                                  margin: EdgeInsets.only(left: 20, right: 20),
                                  padding: EdgeInsets.only(left: 10),
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Colors.black, width: 1),
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
                                      quantity = int.parse(value.toString());
                                    },
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
                            ],
                          ),
                        ),
                      ),
                    ),
                    Align(
                      alignment: Alignment.bottomCenter,
                      child: InkWell(
                        onTap: () async {
                          await addCampaign();
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
                                color:
                                    const Color.fromARGB(255, 255, 255, 255)),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
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

  void _showDateExpPicker() {
    showDatePicker(
      context: context,
      initialDate: dexp,
      firstDate: DateTime(1950),
      lastDate: DateTime(2025),
    ).then((value) {
      setState(() {
        dexp = value!;
      });
    });
  }

  void _showDateStartPicker() {
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

  void _showDateEndPicker() {
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

  void addError(String e) {
    if (error != '') {
      error += ' | ';
    }
    error += e;
  }

  void checkValid() {
    setState(() {
      error = '';
    });
    if (dexp.isBefore(dateStart.add(Duration(days: 30)))) {
      setState(() {
        addError('Hạn sử dụng phải còn ít nhất 1 tháng sau khi bắt đầu bán');
      });
    }
    if (dateStart.isBefore(DateTime.now())) {
      setState(() {
        addError('Ngày bắt đầu không hợp lệ');
      });
    }
    if (dateEnd.isBefore(dateStart) || dateEnd.isAfter(dexp)) {
      setState(() {
        addError('Ngày kết thúc không hợp lệ');
      });
    }
    if (discount >= cmapaignPrice || discount <= 0) {
      setState(() {
        addError('Giá đã giảm phải bé hơn giá gốc và lớn hơn 0');
      });
    }
    if (minQuantity <= 0) {
      setState(() {
        addError('Số lượng đăt mua tối thiếu phải lớn hơn 0');
      });
    }
    if (quantity < minQuantity || quantity <=0) {
      setState(() {
        addError('Số lượng phải lơn hơn hoặc bằng số lượng đặt mua tối thiểu và lớn hơn 0');
      });
    }
  }

  String getStringDate(DateTime date) {
    String monthstr = date.month.toString();
    if (date.month < 10) monthstr = '0${date.month}';

    String daystr = date.day.toString();
    if (date.day < 10) daystr = '0${date.day}';

    String dateString = '${date.year}-$monthstr-$daystr';
    return dateString;
  }

  Future addCampaign() async {
    checkValid();
    if (error == '') {
      var url = Uri.parse('https://swd-nearex.azurewebsites.net/api/campaigns');
      Map<String, dynamic> body = {
        "startDate": getStringDate(dateStart),
        "endDate": getStringDate(dateEnd),
        "exp": getStringDate(dexp),
        "productId": productid,
        "quantity": quantity,
        "campaignDetail": {
          "dateApply": getStringDate(dateStart),
          "percentDiscount": (100-(discount / cmapaignPrice*100)).ceil(),
          "minQuantity": minQuantity
        }
      };
      try {
        http.Response response = await http.post(
          url,
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ${store.token}'
          },
          body: jsonEncode(body),
        );

        if (response.statusCode == 200) {
          print('Response body: ${response.body}');
          Navigator.pop(context);
        } else {
          setState(() {
            addError('Mã sản phẩm bị trùng');
          });
        }
      } catch (e) {
        // Xử lý lỗi khi gửi request
        print('Error sending POST request: $e');
      }
    }
  }

  Future<void> loadProduct() async {
    setState(() {
      isProccessing = true;
    });
    listProductItem.clear();
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/products/product-names?storeId=${store.id}');
    try {
      http.Response response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${store.token}'
        },
      );

      if (response.statusCode == 200) {
        List<dynamic> jsonData = json.decode(response.body.toString());
        for (var result in jsonData) {
          int id = result['id'];
          String name = result['name'];

          double price = double.parse(result['price'].toString());

          listProductItem.add(ProductItem(id: id, name: name, price: price));
        }
        cmapaignPrice = listProductItem.first.price;
        productid = listProductItem.first.id;
      } else {
        print(response.statusCode.toString() + 'hic hic');
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

class ProductItem {
  int id;
  String name;
  double price;
  ProductItem({required this.id, required this.name, required this.price});
}
