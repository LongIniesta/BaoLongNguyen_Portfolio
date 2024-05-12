import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/category.dart';
import 'package:nearex/store/editproduct.dart';

import '../model/product.dart';
import '../model/store.dart';

class ProductDetailScreen extends StatefulWidget {
  ProductDetailScreen(
      {super.key,
      required this.catList,
      required this.store,
      required this.product});

  Store store;
  List<CategoryProduct> catList;
  Product product;

  @override
  State<ProductDetailScreen> createState() =>
      _ProductDetailScreenState(store, catList, product);
}

class _ProductDetailScreenState extends State<ProductDetailScreen> {
  _ProductDetailScreenState(this.store, this.catList, this.product);
  Store store;
  List<CategoryProduct> catList;
  Product product;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          child: Stack(
            children: [
              if (product.image != null)
                Align(
                  alignment: Alignment.topCenter,
                  child: Container(
                      width: double.maxFinite,
                      height: 300,
                      child: Image.memory(
                        product.image as Uint8List,
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
                    decoration: const BoxDecoration(
                        shape: BoxShape.circle,
                        color: Color.fromARGB(95, 206, 206, 206)),
                    child: const Icon(
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
                  decoration: const BoxDecoration(
                      borderRadius:
                          BorderRadius.vertical(top: Radius.circular(25)),
                      color: Color.fromARGB(255, 243, 243, 243)),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Container(
                        width: double.maxFinite,
                        margin:
                            const EdgeInsets.only(top: 20, left: 20, right: 20),
                        child: Text(
                          product.productName.toString(),
                          style: GoogleFonts.sourceSansPro(
                              fontSize: 30, fontWeight: FontWeight.bold),
                        ),
                      ),
                      Container(
                        width: double.maxFinite,
                        margin: const EdgeInsets.only(top: 10, left: 20),
                        child: Text(
                          'Giá gốc: ${product.price} đ',
                          style: GoogleFonts.outfit(
                              color: Color.fromRGBO(65, 109, 212, 1),
                              fontSize: 30,
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
                                'Loại sản phẩm',
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
                                catList
                                    .where((element) =>
                                        element.id == product.categoryId)
                                    .first
                                    .categoryName
                                    .toString(),
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
                                'Mô tả',
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
                                product.description.toString(),
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
                              margin: const EdgeInsets.only(left: 20, top: 20),
                              width: 200,
                              child: Text(
                                'Nơi sản xuất',
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
                                product.origin.toString(),
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
                                'Đơn vị tính',
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
                                product.unit.toString(),
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
                    Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                            builder: (context) => EditProductScreen(
                                  catList: catList,
                                  product: product,
                                  store: store,
                                )));
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
