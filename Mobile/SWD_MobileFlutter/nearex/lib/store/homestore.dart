import 'dart:convert';
import 'dart:math';

import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:nearex/model/category.dart';
import 'package:nearex/model/store.dart';
import 'package:nearex/store/accountstore.dart';
import 'package:nearex/store/campaignstore.dart';
import 'package:nearex/store/orderstore.dart';
import 'package:nearex/store/productstore.dart';
import 'package:nearex/store/startstorescreen.dart';
import 'package:http/http.dart' as http;
import 'package:firebase_storage/firebase_storage.dart' as firebase_storage;



class StoreScreen extends StatelessWidget {
  Store store;
  StoreScreen({super.key, required this.store});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Bottom Navigation Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(
        store: store,
      ),
    );
  }
}

class MyHomePage extends StatefulWidget {
  Store store;

  MyHomePage({super.key, required this.store});
  @override
  // ignore: library_private_types_in_public_api, no_logic_in_create_state
  _MyHomePageState createState() => _MyHomePageState(store);
}

class _MyHomePageState extends State<MyHomePage> {
  int _currentIndex = 0;
  late PageController _pageController;
  Store store;
  Uint8List? logoImg;
    List<CategoryProduct> catList = [];
  _MyHomePageState(this.store);

  @override
  void initState() {
    super.initState();
        loadCategory();
    _pageController = PageController();
  }
  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }
  void _onItemTapped(int index) {
    setState(() {
      _currentIndex = index;
      _pageController.animateToPage(
        index,
        duration: Duration(milliseconds: 200),
        curve: Curves.easeInOut,
      );
    });
  }

  Future<void> loadCategory() async {
    if (store.logo != null){
        logoImg = await getImageFromFirebase(store.logo.toString()) as Uint8List?;
    }
    if (!catList.contains(CategoryProduct(categoryName: 'Tất cả', id: 0)))
    catList.add(CategoryProduct(categoryName: 'Tất cả', id: 0));
    var url = Uri.parse('https://swd-nearex.azurewebsites.net/api/categories');
    try {
      http.Response response = await http.get(url);
      if (response.statusCode == 200) {
        String jsonString = response.body;
        Map<String, dynamic> jsonData = json.decode(response.body.toString());

        List<dynamic> results = jsonData['results'];

        for (var result in results) {
          int id = result['id'];
          String name = result['categoryName'];
          CategoryProduct category =
              CategoryProduct(id: id, categoryName: name);
          catList.add(category);
          print(category.categoryName);
        }
      }
    // ignore: empty_catches
    } catch (e) {
    }
  }
  

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: PageView(
        controller: _pageController,
        onPageChanged: (int index) {
          setState(() {
            _currentIndex = index;
          });
        },
        children: <Widget>[
          StartStoreScreen(
            store: store,
          ),
          CapaignStoreScreen(
            store: store,catList: catList,
          ),
          ProductStoreScreen(
            store: store,catList: catList,
          ),
          OrderStoreScreen(
            store: store,
          ),
          AccountStoreScreen(
            store: store, logoImg: logoImg,
          )
        ],
      ),
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: const Color.fromARGB(255, 255, 255, 255),
        unselectedItemColor: const Color.fromARGB(255, 78, 78, 78),
        currentIndex: _currentIndex,
        selectedItemColor: Color.fromARGB(255, 8, 8, 8),
        onTap: _onItemTapped,
        items: [
          BottomNavigationBarItem(
            icon: Image.asset('images/trangchu.png'),
            label: 'Trang chủ',
          ),
          BottomNavigationBarItem(
            icon: Image.asset('images/dothang.png'),
            label: 'Đợt hàng',
          ),
          BottomNavigationBarItem(
            icon: Image.asset('images/sanpham.png'),
            label: 'Sản phẩm',
          ),
          BottomNavigationBarItem(
            icon: Image.asset('images/donhang.png'),
            label: 'Đơn hàng',
          ),
          BottomNavigationBarItem(
            icon: Image.asset('images/taikhoan.png'),
            label: 'Tài khoảng',
          ),
        ],
      ),
    );
  }
  
  Future<Uint8List?> getImageFromFirebase(String imageUrl) async {
    try {

      
      
      // Tạo một Firebase Storage instance
      firebase_storage.FirebaseStorage storage =
          firebase_storage.FirebaseStorage.instance;

          

      // Lấy reference đến tệp hình ảnh trong Firebase Storage
      firebase_storage.Reference ref = storage.refFromURL(imageUrl);

      // Tải xuống URL của tệp hình ảnh
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
      print('error' +e.toString());
      return null;
    }
  }
}
