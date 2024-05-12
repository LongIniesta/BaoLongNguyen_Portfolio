import 'dart:convert';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/model/store.dart';
import 'package:nearex/store/homestore.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../model/product.dart';


class LoginStore extends StatefulWidget {
  const LoginStore({super.key});

  @override
  State<StatefulWidget> createState() {
    return LoginStoreState();
  }
}

class LoginStoreState extends State<LoginStore> {
  var phoneNumber = '';
  var error = '';
  var password = '';
  var inProcessing = false;
  final FocusNode _focusNode = FocusNode();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: AbsorbPointer(
          absorbing: inProcessing,
          child: Container(
            decoration: const BoxDecoration(
              gradient: LinearGradient(
                begin: Alignment.topLeft, // Điểm bắt đầu gradient
                end: Alignment.bottomRight, // Điểm kết thúc gradient
                colors: [
                  Color.fromARGB(255, 11, 0, 61),
                  Color.fromARGB(255, 49, 49, 49),
                ],
              ),
            ),
            width: double.maxFinite,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.end,
              mainAxisSize: MainAxisSize.max,
              children: [
                Align(
                  alignment: Alignment.center,
                  child: Image.asset('images/logo.png',
                      alignment: Alignment.center),
                ),
                Container(
                    height: 600,
                    width: double.maxFinite,
                    decoration: const BoxDecoration(
                      image: DecorationImage(
                          image: AssetImage('images/backwhitemain.png'),
                          fit: BoxFit.fill),
                    ),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        const Text(
                          'Đăng nhập cửa hàng',
                          style: TextStyle(
                              color: Color.fromARGB(255, 8, 6, 0),
                              fontWeight: FontWeight.bold,
                              fontSize: 30),
                          textAlign: TextAlign.center,
                        ),
                        const SizedBox(
                          height: 5,
                        ),
                        const Text(
                          'Nhập số điện thoại và mật khẩu để tiếp tục',
                          style: TextStyle(
                              color: Color.fromARGB(255, 58, 58, 58),
                              fontWeight: FontWeight.w400,
                              fontSize: 17),
                          textAlign: TextAlign.center,
                        ),
                        const SizedBox(
                          height: 30,
                        ),
                        Container(
                          height: 60,
                          width: 370,
                          alignment: Alignment.center,
                          padding: const EdgeInsets.only(left: 10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                color: const Color.fromARGB(255, 196, 196, 196),
                                width: 1.5,
                              ),
                              borderRadius: BorderRadius.circular(10)),
                          child: TextField(
                            style: const TextStyle(fontSize: 18),
                            keyboardType: TextInputType.phone,
                            onChanged: (value) {
                              phoneNumber = value;
                            },
                            cursorColor: Colors.black,
                            decoration: const InputDecoration(
                              hintText: 'Số điện thoại của bạn',
                              border: InputBorder.none,
                            ),
                          ),
                        ),
                        const SizedBox(
                          height: 30,
                        ),
                        Container(
                          height: 60,
                          width: 370,
                          alignment: Alignment.center,
                          padding: const EdgeInsets.only(left: 10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                color: const Color.fromARGB(255, 196, 196, 196),
                                width: 1.5,
                              ),
                              borderRadius: BorderRadius.circular(10)),
                          child: TextField(
                            obscureText: true,
                            style: const TextStyle(fontSize: 18),
                            keyboardType: TextInputType.visiblePassword,
                            onChanged: (value) {
                              password = value;
                            },
                            cursorColor: Colors.black,
                            decoration: const InputDecoration(
                              hintText: 'Mật khẩu',
                              border: InputBorder.none,
                            ),
                          ),
                        ),
                        Text(
                          error,
                          style: const TextStyle(
                              color: Color.fromARGB(255, 255, 0, 0)),
                        ),
                        const SizedBox(
                          height: 20,
                        ),
                        SizedBox(
                          height: 40,
                          width: 300,
                          child: ElevatedButton(
                            onPressed: () async {
                              setState(() {
                                _focusNode.unfocus();
                              });
                              if (phoneNumber.isEmpty) {
                                setState(() {
                                  error = 'Vui lòng nhập số điện thoại của bạn';
                                });
                              } else if (phoneNumber.length != 10) {
                                setState(() {
                                  error = 'Số điện thoại không hợp lệ';
                                });
                              } else if (password.isEmpty) {
                                if (error != '') error += ' | ';
                                setState(() {
                                  error += 'Nhập mật khẩu';
                                });
                              } else {
                                setState(() {
                                  error = '';
                                });
                              }
                              print(phoneNumber);
                              print(password);

                              await loginstore();
                            },
                            style: ElevatedButton.styleFrom(
                                foregroundColor:
                                    const Color.fromARGB(255, 255, 255, 255),
                                backgroundColor:
                                    const Color.fromARGB(255, 75, 121, 227),
                                shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(40))),
                            child: const Text('Tiếp tục'),
                          ),
                        ),
                        const SizedBox(
                          height: 70,
                        ),
                        if (inProcessing) const CircularProgressIndicator(),
                        const SizedBox(
                          height: 20,
                        ),
                        InkWell(
                          onTap: () {},
                          child: const Text(
                            'Bạn muốn đăng kí tài khoản cho cửa hàng?',
                            style:
                                TextStyle(decoration: TextDecoration.underline),
                          ),
                        ),
                      ],
                    )),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Future<void> loginstore() async {
    if (error == '') {
      setState(() {
        inProcessing = true;
      });
      var url =
          Uri.parse('https://swd-nearex.azurewebsites.net/api/stores/authentication');
      Map<String, dynamic> body = {
        "phone": "$phoneNumber",
        "password": "$password"
      };

      try {
        http.Response response = await http.post(
          url,
          headers: {'Content-Type': 'application/json'},
          body: jsonEncode(body),
        );
        
        if (response.statusCode == 200) {
          Store store = parseJson(response.body);
          SharedPreferences prefs = await SharedPreferences.getInstance();
          await prefs.setString('Loginwith', 'Store');
          await prefs.setString('Store', response.body);

          print('token: Bearer ${store.token}');

          // ignore: use_build_context_synchronously
          Navigator.pushReplacement(
              context,
              MaterialPageRoute(
                  builder: (context) => StoreScreen(
                        store: store,
                      )));
        } else {
          print(response.statusCode.toString());
          setState(() {
            error = 'Sai sđt hoặc mật khẩu';
          });
        }
      } catch (e) {
        print('loi call');
        setState(() {
          error = 'Sai sđt hoặc mật khẩu';
        });
      }

      setState(() {
        inProcessing = false;
      });
    }
  }
  

}
