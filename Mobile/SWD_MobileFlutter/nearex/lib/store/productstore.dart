import 'dart:ffi';
import 'dart:typed_data';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/category.dart';
import 'package:nearex/model/product.dart';
import 'package:nearex/store/addnewproduct.dart';
import 'package:nearex/store/productdetail.dart';
import 'dart:convert';
import 'package:firebase_storage/firebase_storage.dart' as firebase_storage;
import 'package:http/http.dart' as http;
import 'package:dio/dio.dart';

import 'package:nearex/model/store.dart';

class ProductStoreScreen extends StatefulWidget {
  ProductStoreScreen({super.key, required this.store, required this.catList});
  Store store;
  List<CategoryProduct> catList;
  @override
  State<StatefulWidget> createState() {
    return ProductStoreScreenState(store, catList);
  }
}

class ProductStoreScreenState extends State<ProductStoreScreen> {
  Store store;
  ProductStoreScreenState(this.store, this.catList);
  bool isProccessing = true;
  int maxpage = 0;
  List<Product> resultList = [];
  List<Product> listshow = [];
  List<CategoryProduct> catList;

  int page = 1;
  int idCat = 0;
  String search = '';
  ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    loadProduct();
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
          await loadMore();
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
                            child: Text('Sản phẩm của ${store.storeName}',
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
                              onTap: () async {
                                await loadProduct();
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
                                  builder: (context) => AddNewProduct(
                                        catList: catList,
                                        store: store,
                                      )));
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
                        children: catList.map((cat) {
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
                                cat.categoryName.toString(),
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
                                loadProduct();
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
                                  cat.categoryName.toString(),
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
                        controller: _scrollController,
                        child: Column(
                          children: resultList.map((pro) {
                            return Container(
                              padding: EdgeInsets.all(10),
                              width: double.maxFinite,
                              height: 150,
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
                                                      ProductDetailScreen(
                                                        catList: catList,
                                                        product: pro,
                                                        store: store,
                                                      )));
                                        },
                                        child: Icon(Icons.edit, size: 35),
                                      ),
                                    ),
                                    Row(
                                      children: [
                                        if (pro.productImg == null)
                                          Image.asset(
                                            'images/product.png',
                                            width: 150,
                                            height: double.maxFinite,
                                            fit: BoxFit.contain,
                                          )
                                        else
                                          Image.network(
                                            pro.productImg!,
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
                                              SizedBox(
                                                height: 5,
                                              ),
                                              Text(
                                                pro.productName.toString(),
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.bold),
                                              ),
                                              SizedBox(
                                                height: 10,
                                              ),
                                              Text(
                                                '${pro.price} vnđ',
                                                style:
                                                    GoogleFonts.sourceSansPro(
                                                        fontSize: 20,
                                                        fontWeight:
                                                            FontWeight.w700,
                                                        color: Color.fromARGB(
                                                            255, 48, 144, 253)),
                                              ),
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
              const Align(
                  alignment: Alignment.center,
                  child: CircularProgressIndicator()),
          ],
        ),
      ),
    );
  }

  Future<void> loadMore() async {
    setState(() {
      isProccessing = true;
    });
    String catID = '';
    if (idCat != 0) {
      catID = '&CategoryId=${idCat}';
    }
    String searchQuery = '';
    if (search != '') {
      searchQuery = '&ProductName=${search}';
    }
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/products?StoreId=${store.id}&Page=${page}&PageSize=10${catID}${searchQuery}');
    try {
      http.Response response = await http.get(url);

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
          double price = double.parse(result['price'].toString()); 
          String origin = result['origin'];
          String productImg = result['productImg'];
          String productName = result['productName'];
          String description = result['description'];
          String unit = result['unit'];
          int netWeight = result['netWeight'];
          int categoryId = result['categoryId'];
          int storeId = result['storeId'];
          int? status = result['status'];
          Uint8List? image = await getImageFromFirebase(productImg);
          Product product = Product(
              categoryId: categoryId,
              description: description,
              id: id,
              netWeight: netWeight,
              origin: origin,
              price: price,
              productImg: productImg,
              productName: productName,
              status: status,
              storeId: storeId,
              unit: unit,
              image: image);
          resultList.add(product);
        }
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

  Future<void> loadProduct() async {
    setState(() {
      isProccessing = true;
      page = 1;
    });
    resultList.clear();
    String catID = '';
    if (idCat != 0) {
      catID = '&CategoryId=${idCat}';
    } else {

    }
    String searchQuery = '';
    if (search != '') {
      searchQuery = '&ProductName=${search}';
    }
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/products?StoreId=${store.id}&Page=${page}&PageSize=30${catID}${searchQuery}');
    try {
      http.Response response = await http.get(url);

      print(response.statusCode.toString() +'Long ne');
      print(store.id);

      if (response.statusCode == 200) {
        String jsonString = response.body;
        
        // print(response.body
        //     .substring(response.body.length - 500, response.body.length));
        Map<String, dynamic> jsonData = json.decode(response.body.toString());

        print('vo' + jsonString);
        maxpage = jsonData['totalNumberOfPages'];

        List<dynamic> results = jsonData['results'];

        for (var result in results) {
          
          int id = result['id'];
          
          print(result['price']);
          print('price tren');
          double price = double.parse(result['price'].toString()); 
          print('price tren');
          
          
          String origin = result['origin'];
          String productImg = result['productImg'];
          
          String productName = result['productName'];
          String description = result['description'];
          String unit = result['unit'];
          
          int netWeight = result['netWeight'];
          int categoryId = result['categoryId'];
          int storeId = result['storeId'];
          int? status = result['status'];
         // Uint8List? image = await getImageFromFirebase(productImg);
          Product product = Product(
              categoryId: categoryId,
              description: description,
              id: id,
              netWeight: netWeight,
              origin: origin,
              price: price,
              productImg: productImg,
              productName: productName,
              status: status,
              storeId: storeId,
              unit: unit,
              image: null);
          resultList.add(product);
        }
        
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

  Future<Uint8List?> getImageFromFirebase(String imageUrl) async { 
    try {
      // // Tạo một Firebase Storage instance
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
}
