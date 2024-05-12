import 'dart:convert';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/guest/createaccount.dart';
import 'package:nearex/guest/loginstore.dart';
import 'package:nearex/guest/otpconfirm.dart';
import 'package:nearex/guest/passwordtologin.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({super.key});

  @override
  State<StatefulWidget> createState() {
    return MainScreenState();
  }
}

class MainScreenState extends State<MainScreen> {
  var phoneNumber = '';
  var error = '';
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
                child:
                    Image.asset('images/logo.png', alignment: Alignment.center),
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
                        'Welcome To NearEx',
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
                        'Nhập số điện thoại để tiếp tục',
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
                          focusNode: _focusNode,
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
                      Text(
                        error,
                        style: const TextStyle(
                            color: Color.fromARGB(255, 255, 0, 0)),
                      ),
                      const SizedBox(
                        height: 30,
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
                            } else {
                              setState(() {
                                error = '';
                              });
                              checkPhone(phoneNumber);
                            }
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
                      const Text(
                        '-Hoặc đăng nhập bằng-',
                        style: TextStyle(
                            color: Color.fromARGB(255, 111, 111, 111),
                            fontWeight: FontWeight.w200,
                            fontSize: 15),
                        textAlign: TextAlign.center,
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      InkWell(
                        onTap: signInWithGoogle,
                        child: Container(
                          width: 50,
                          height: 50,
                          decoration: const BoxDecoration(
                            shape: BoxShape.circle,
                            image: DecorationImage(
                              image: AssetImage('images/google.png'),
                              fit: BoxFit.cover,
                            ),
                          ),
                        ),
                      ),
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
                      const SizedBox(
                        height: 30,
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => LoginStore()));
                        },
                        child: Image.asset(
                          'images/shop.png',
                          color: Color.fromARGB(255, 109, 109, 109),
                        ),
                      )
                    ],
                  )),
            ],
          ),
        ),
      ),
    ));
  }

  Future<void> sendOTP(String phone) async {
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/users/verification');
    Map<String, dynamic> body = {
      "accountSID": "AC8e6696ff8faac7281f34047859c4763c",
      "authToken": "367d20fdb4452534a0a36fb4ba09816a",
      "pathServiceSid": "VAac562f462db7c7cead88dc52e5731781",
      "phone": "$phone",
      "token": null
    };
    try {
      http.Response response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode(body),
      );

      if (response.statusCode == 200) {
        if (response.body == 'pending') {
          // ignore: use_build_context_synchronously
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => OtpComfirm(
                        phone: phone,
                      )));
        }
      } else {
        print('Request failed with status code: ${response.body}');
      }
    } catch (e) {
      print('Error sending POST request: $e');
    }
  }

  Future<void> checkPhone(String phone) async {
    
    setState(() {
      if (phone == '') {
        error = 'Vui lòng nhập số điện thoại';
      } else if (phone.length != 10) {
        error = 'Số điện thoại không hợp lệ';
      } else {
        error = '';
      }
    });
    if (error == '') {
      setState(() {
        inProcessing = true;
      });

      var url = Uri.parse(
          'https://swd-nearex.azurewebsites.net/api/users/verification?phone=$phone');
      Map<String, dynamic> body = {
        "accountSID": "",
        "authToken": "",
        "pathServiceSid": "",
        "phone": "",
        "token": null
      };

      try {
        http.Response response = await http.post(
          url,
          headers: {'Content-Type': 'application/json'},
          body: jsonEncode(body),
        );
        
        if (response.statusCode == 200) {
          print(response.body.toString());
          if (response.body == 'false') { 
            await sendOTP(phone);
          } else {
            // ignore: use_build_context_synchronously
            Navigator.push(
                context,
                MaterialPageRoute(
                    builder: (context) => PasswordToLogin(phone: phone)));
          }
        } else {
          // Request thất bại, xử lý lỗi
          print('Request failed with status code: ${response.statusCode.toString()}');
        }
      } catch (e) {
        // Xử lý lỗi khi gửi request
        print('Error sending POST request: $e');
      }
      setState(() {
        inProcessing = false;
      });
    }
  }

  Future<UserCredential> signInWithGoogle() async {
    // Trigger the authentication flow
    final GoogleSignInAccount? googleUser = await GoogleSignIn().signIn();

    // Obtain the auth details from the request
    final GoogleSignInAuthentication? googleAuth =
        await googleUser?.authentication;

    
    // Create a new credential
    final credential = GoogleAuthProvider.credential(
      accessToken: googleAuth?.accessToken,
      idToken: googleAuth?.idToken,
    );

    print(googleUser!.id);

    String googleId = googleUser.id;
    setState(() {
      inProcessing = true;
    });
    var url = Uri.parse(
        'https://swd-nearex.azurewebsites.net/api/users/verification?googleId=$googleId');
    Map<String, dynamic> body = {
      "accountSID": "",
      "authToken": "",
      "pathServiceSid": "",
      "phone": "",
      "token": null
    };

    try {
      http.Response response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode(body),
      );

      if (response.statusCode == 200) {
        if (response.body == 'false') {
          // ignore: use_build_context_synchronously
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => CreateAccount(
                      password: '',
                      phoneNumber: null,
                      googleUser: googleUser)));
        } else {
          print('Vào trang chính của account');
        }
      } else {
        // Request thất bại, xử lý lỗi
        print('Request failed with status code: ${response.body}');
      }
    } catch (e) {
      // Xử lý lỗi khi gửi request
      print('Error sending POST request: $e');
    }

    setState(() {
      inProcessing = false;
    });
    // Once signed in, return the UserCredential
    return await FirebaseAuth.instance.signInWithCredential(credential);
  }
}
