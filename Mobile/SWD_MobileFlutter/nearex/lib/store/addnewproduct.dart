import 'dart:convert';
import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/model/category.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/model/store.dart';

class AddNewProduct extends StatefulWidget {
  AddNewProduct({super.key, required this.store, required this.catList,});

  Store store;
  List<CategoryProduct> catList;

  @override
  State<AddNewProduct> createState() => _AddNewProductState(store, catList);
}

class _AddNewProductState extends State<AddNewProduct> {
  String category = 'Mì gói';
  PlatformFile? pickFile;
  Store store;
  List<CategoryProduct> catList;
  

  String name = '';
  int catId = 0;
  String mota = '';
  String maSanPham = '';
  String noisanxuat = '';
  String donvitinh = '';
  double giagoc = 0;
  String linkImg = '';
  String error = '';

  _AddNewProductState(this.store, this.catList);
  Future selectImage() async {
    final result = await FilePicker.platform.pickFiles();
    if (result == null) return;
    setState(() {
      pickFile = result.files.first;
    });
  }

  Future uploadImage() async {
    //WidgetsFlutterBinding.ensureInitialized();
   // await Firebase.initializeApp();
    FirebaseAuth mAuth = FirebaseAuth.instance;

    // if (mAuth.currentUser != null) {
    //   // do your stuff
    // } else {
    //  await mAuth.signInAnonymously();
    // }

    final path = 'avts/${pickFile!.name}';
    final file = File(pickFile!.path!);
    final ref = FirebaseStorage.instance.ref().child(path);
    UploadTask uploadTask = ref.putFile(file);

    final snapshot = await uploadTask!.whenComplete(() {});

    final urlDownload = await snapshot.ref.getDownloadURL();

    linkImg = urlDownload;
  }

  @override
  void initState() {
    super.initState();
    catId = catList.first.id as int;
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
    if (name.trim().isEmpty) {
      setState(() {
        addError('Chưa nhập tên sản phẩm');
      });
    }
    if (maSanPham.trim().isEmpty) {
      setState(() {
        addError('Chưa nhập mã sản phẩm');
      });
    }
    if (mota.trim().isEmpty) {
      setState(() {
        addError('Chưa nhập mô tả');
      });
    }
    if (catId == 0){
      setState(() {
        addError('Loại sản phẩm không hợp lệ');
      });
    }
    if (noisanxuat.trim().isEmpty) {
      setState(() {
        addError('Chưa nhập nơi sản xuất');
      });
    }
    if (donvitinh.trim().isEmpty) {
      setState(() {
        addError('Chưa nhập đơn vị tính');
      });
    }
    if (giagoc <= 0) {
      setState(() {
        addError('Giá gốc không hợp lệ');
      });
    }
    if (pickFile == null) {
      setState(() {
        addError('Chưa tải ảnh sản phẩm');
      });
    }
  }

  Future addProduct() async {
    checkValid();
    if (error == '') {
      await uploadImage();

      var url = Uri.parse('https://swd-nearex.azurewebsites.net/api/products');
      Map<String, dynamic> body = {
        "code": maSanPham,
        "price": giagoc,
        "origin": "$noisanxuat",
        "productImg": "$linkImg",
        "productName": "$name",
        "description": "$mota",
        "unit": "$donvitinh",
        "netWeight": 0,
        "categoryId": catId,
        "storeId": store.id,
        "status": 1
      };  
      try {
        http.Response response = await http.post(
          url,
          headers: {'Content-Type': 'application/json','Authorization': 'Bearer ${store.token}' },
          body: jsonEncode(body),
        );

        if (response.statusCode == 200) {
          // Request thành công, xử lý response
          print('Response body: ${response.body}');
          // ignore: use_build_context_synchronously
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          color: const Color.fromRGBO(233, 248, 249, 1),
          child: Stack(
            children: [
              Align(
                alignment: Alignment.topLeft,
                child: InkWell(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: Container(
                    margin: const EdgeInsets.only(top: 50, left: 10),
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
                alignment: Alignment.topCenter,
                child: Container(
                  margin: const EdgeInsets.only(top: 65),
                  child: Text(
                    'Thêm sản phẩm mới',
                    style: GoogleFonts.outfit(
                        fontSize: 25,
                        fontWeight: FontWeight.bold,
                        color: const Color.fromARGB(255, 15, 14, 36)),
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
                        if (pickFile == null)
                          Align(
                            alignment: Alignment.center,
                            child: InkWell(
                              onTap: () async {
                                selectImage();
                              },
                              child: Container(
                                margin:
                                    const EdgeInsets.only(left: 10, right: 10),
                                width: double.maxFinite,
                                height: 100,
                                decoration: BoxDecoration(
                                  borderRadius: BorderRadius.circular(10),
                                  image: const DecorationImage(
                                    image: AssetImage('images/uploadimg.png'),
                                    fit: BoxFit.cover,
                                  ),
                                ),
                              ),
                            ),
                          )
                        else
                          Align(
                            alignment: Alignment.center,
                            child: InkWell(
                              onTap: () async {
                                selectImage();
                              },
                              child: Container(
                                margin:
                                    const EdgeInsets.only(left: 10, right: 10),
                                width: double.maxFinite,
                                height: 200,
                                decoration: BoxDecoration(
                                  borderRadius: BorderRadius.circular(10),
                                  image: DecorationImage(
                                    image: FileImage(File(pickFile!.path!)),
                                    fit: BoxFit.contain,
                                  ),
                                ),
                              ),
                            ),
                          ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Tên sản phẩm',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.text,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Tên sản phẩm',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                name = value;
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Mã sản phẩm',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.text,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Mã sản phẩm',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                maSanPham = value;
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Phân loại sản phẩm',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            width: double.maxFinite,
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: DropdownButton<String>(
                              underline: Container(
                                height: 0,
                              ),
                              value: catId.toString(),
                              style: const TextStyle(color: Colors.black),
                              onChanged: (String? newValue) {
                                setState(() {
                                  catId = int.parse(newValue!);
                                });
                              },
                              items: catList.map((e) {
                                return DropdownMenuItem<String>(
                                  value: e.id.toString(),
                                  child: Text(e.categoryName.toString()),
                                );
                              }).toList(),
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Mô tả',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.text,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Mô tả',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                mota = value;
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Nơi sản xuất',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.text,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Nơi sản xuất',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                noisanxuat = value;
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Đơn vị tính',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.text,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: 'Đơn vị tính',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                donvitinh = value;
                              },
                            ),
                          ),
                        ),
                        Container(
                          margin: const EdgeInsets.only(top: 10, left: 20),
                          child: Text(
                            'Giá gốc',
                            style: GoogleFonts.outfit(
                                fontSize: 20, fontWeight: FontWeight.bold),
                          ),
                        ),
                        Align(
                          alignment: Alignment.center,
                          child: Container(
                            margin: const EdgeInsets.only(left: 20, right: 20),
                            padding: const EdgeInsets.only(left: 10),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black, width: 1),
                              borderRadius: BorderRadius.circular(10),
                              color: Colors.white,
                            ),
                            child: TextField(
                              keyboardType: TextInputType.number,
                              cursorColor: Colors.black,
                              decoration: const InputDecoration(
                                hintText: '0',
                                border: InputBorder.none,
                              ),
                              onChanged: (value) {
                                giagoc = double.parse(value);
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
                        )
                      ],
                    ),
                  ),
                ),
              ),
              Align(
                alignment: Alignment.bottomCenter,
                child: InkWell(
                  onTap: () async {
                    await addProduct();
                  },
                  child: Container(
                    margin: const EdgeInsets.only(bottom: 10),
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
}
