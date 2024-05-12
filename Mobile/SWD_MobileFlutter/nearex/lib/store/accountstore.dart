import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:file_picker/file_picker.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:firebase_storage/firebase_storage.dart' as firebase_storage;
import 'package:http/http.dart' as http;
import 'package:nearex/model/store.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'homestore.dart';

class AccountStoreScreen extends StatefulWidget {
  AccountStoreScreen({super.key, required this.store, required this.logoImg});
  Store store;
  Uint8List? logoImg;
  @override
  State<StatefulWidget> createState() {
    return AccountStoreScreenState(store, logoImg);
  }
}

class AccountStoreScreenState extends State<AccountStoreScreen> {
  PlatformFile? pickFile;
  String? linkImg;
  Store store;
  String? storeName;
  String? phone;
  String? address;
  Uint8List? logoImg;
  AccountStoreScreenState(this.store, this.logoImg);
  Future selectImage() async {
    final result = await FilePicker.platform.pickFiles();
    if (result == null) return;
    setState(() {
      pickFile = result.files.first;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        child: Stack(
          children: [
            Align(
              alignment: Alignment.topLeft,
              child: InkWell(
                onTap: () {
                 // Navigator.pop(context);
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
                margin: EdgeInsets.only(top: 60),
                child: Text(
                  'Thông tin cửa hàng',
                  style: GoogleFonts.outfit(
                      fontSize: 25,
                      fontWeight: FontWeight.bold,
                      color: Color.fromARGB(255, 60, 125, 255)),
                ),
              ),
            ),
            Align(
              alignment: Alignment.bottomCenter,
              child: Container(
                width: double.maxFinite,
                height: 730,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    if (logoImg != null && pickFile == null)
                      Align(
                        alignment: Alignment.center,
                        child: InkWell(
                          onTap: () async {
                            selectImage();
                          },
                          child: Container(
                            margin: EdgeInsets.only(left: 10, right: 10),
                            width: double.maxFinite,
                            height: 100,
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(10),
                            ),
                            child: Image.memory(
                              logoImg!,
                              width: 150,
                              height: double.maxFinite,
                              fit: BoxFit.contain,
                            ),
                          ),
                        ),
                      )
                    else if (pickFile == null)
                      Align(
                        alignment: Alignment.center,
                        child: InkWell(
                          onTap: () async {
                            selectImage();
                          },
                          child: Container(
                            margin: EdgeInsets.only(left: 10, right: 10),
                            width: double.maxFinite,
                            height: 100,
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(10),
                              image: DecorationImage(
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
                            margin: EdgeInsets.only(left: 10, right: 10),
                            width: double.maxFinite,
                            height: 300,
                            decoration: BoxDecoration(
                              color: Color.fromARGB(255, 74, 192, 243),
                              borderRadius: BorderRadius.circular(10),
                              image: DecorationImage(
                                image: FileImage(File(pickFile!.path!)),
                                fit: BoxFit.cover,
                              ),
                            ),
                          ),
                        ),
                      ),
                    Container(
                      margin: EdgeInsets.only(top: 10, left: 20),
                      child: Text(
                        'Tên cửa hàng',
                        style: GoogleFonts.outfit(
                            fontSize: 20, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Align(
                      alignment: Alignment.center,
                      child: Container(
                        margin: EdgeInsets.only(left: 20, right: 20, top: 10),
                        padding: EdgeInsets.only(left: 10),
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.black, width: 1),
                          borderRadius: BorderRadius.circular(10),
                          color: Colors.white,
                        ),
                        child: TextField(
                          keyboardType: TextInputType.text,
                          cursorColor: Colors.black,
                          decoration: InputDecoration(
                            hintText: store.storeName,
                            border: InputBorder.none,
                          ),
                          onChanged: (value) {
                            setState(() {
                              storeName = value;
                            });
                          },
                        ),
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.only(top: 20, left: 20),
                      child: Text(
                        'Số điện thoại',
                        style: GoogleFonts.outfit(
                            fontSize: 20, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Align(
                      alignment: Alignment.center,
                      child: Container(
                        margin: EdgeInsets.only(left: 20, right: 20, top: 10),
                        padding: EdgeInsets.only(left: 10),
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.black, width: 1),
                          borderRadius: BorderRadius.circular(10),
                          color: Colors.white,
                        ),
                        child: TextField(
                          keyboardType: TextInputType.text,
                          cursorColor: Colors.black,
                          decoration: InputDecoration(
                            hintText: store.phone,
                            border: InputBorder.none,
                          ),
                          onChanged: (value) {
                            setState(() {
                              phone = value;
                            });
                          },
                        ),
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.only(top: 20, left: 20),
                      child: Text(
                        'Địa chỉ',
                        style: GoogleFonts.outfit(
                            fontSize: 20, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Align(
                      alignment: Alignment.center,
                      child: Container(
                        margin: EdgeInsets.only(left: 20, right: 20, top: 10),
                        padding: EdgeInsets.only(left: 10),
                        decoration: BoxDecoration(
                          border: Border.all(color: Colors.black, width: 1),
                          borderRadius: BorderRadius.circular(10),
                          color: Colors.white,
                        ),
                        child: TextField(
                          keyboardType: TextInputType.text,
                          cursorColor: Colors.black,
                          decoration: InputDecoration(
                            hintText: store.address,
                            border: InputBorder.none,
                          ),
                          onChanged: (value) {
                            setState(() {
                              address = value;
                            });
                          },
                        ),
                      ),
                    ),
                    Align(
                      alignment: Alignment.center,
                      child: InkWell(
                        onTap: () {},
                        child: Container(
                          margin: EdgeInsets.only(bottom: 10, top: 10),
                          alignment: Alignment.center,
                          height: 50,
                          width: 300,
                          decoration: BoxDecoration(
                              color: const Color.fromARGB(255, 117, 191, 252),
                              borderRadius: BorderRadius.circular(10)),
                          child: Text(
                            'Đổi mật khẩu',
                            style: GoogleFonts.outfit(
                                fontSize: 20,
                                fontWeight: FontWeight.bold,
                                color:
                                    const Color.fromARGB(255, 255, 255, 255)),
                          ),
                        ),
                      ),
                    ),
                    Align(
                      alignment: Alignment.center,
                      child: InkWell(
                        onTap: ()async {
                            await editStore();
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
                            'Cập nhật',
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
            )
          ],
        ),
      ),
    );
  }

  Future uploadImage() async {
    //WidgetsFlutterBinding.ensureInitialized();
    // await Firebase.initializeApp();
    // FirebaseAuth mAuth = FirebaseAuth.instance;

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
    print('ddd');
  }

  Future editStore() async {
    if (pickFile != null) {
      await uploadImage();
    }

    if (phone == null || phone!.isEmpty){
       phone = store.phone;
    }
    if (address == null || address!.isEmpty){
       address = store.address;
    }
    if (storeName == null || storeName!.isEmpty){
       storeName = store.storeName;
    }
    if (linkImg == null || linkImg!.isEmpty){
       linkImg = store.logo;
    }
    

    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/stores/${store.id}');
    Map<String, dynamic> body = {
      "storeName": "$storeName",
      "address": "$address",
      "phone": "$phone",
      "logo": "$linkImg",
      "coordinateString": null
    };
    print(body);
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
        print('token hihi:' + store.token.toString() );
          Store store2 = parseJson(response.body);
          store2.id = store.id;
          store2.token=  store.token;
          SharedPreferences prefs = await SharedPreferences.getInstance();
          await prefs.setString('Loginwith', 'Store');
          await prefs.setString('Store', response.body);
          
          // ignore: use_build_context_synchronously
          Navigator.pushReplacement(
              context,
              MaterialPageRoute(
                  builder: (context) => StoreScreen(
                        store: store2,
                      )));
      } else {
        // Request thất bại, xử lý lỗi
        print('Request failed with status code: ${response.statusCode}');
      }
    } catch (e) {
      // Xử lý lỗi khi gửi request
      print('Error sending POST request: $e');
    }
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
