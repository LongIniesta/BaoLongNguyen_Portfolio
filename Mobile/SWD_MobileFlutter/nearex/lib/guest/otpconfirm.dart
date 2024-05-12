import 'dart:convert';
import 'dart:ffi';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:http/http.dart' as http;
import 'package:nearex/guest/createpass.dart';

class OtpComfirm extends StatefulWidget {
  OtpComfirm({super.key, required this.phone});
  String phone;

  @override
  State<StatefulWidget> createState() {
    return OtpComfirmState(phone);
  }
}

class OtpComfirmState extends State<OtpComfirm> {
  String phoneNumber = '';
  String phoneHint = '';

  OtpComfirmState(String phone) {
    phoneNumber = phone;
  }
  List<String> listValidate = ['', '', '', '', '', ''];
  @override
  void initState() {
    super.initState();
    for (int i = 1; i <= phoneNumber.length; i++) {
      phoneHint += '*';
    }
    phoneHint +=
        phoneNumber.substring(phoneNumber.length - 3, phoneNumber.length);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          decoration: const BoxDecoration(
            color: Colors.white,
          ),
          //width: double.maxFinite,
          child: Column(
            //  crossAxisAlignment: CrossAxisAlignment.stretch,
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Container(
                margin: const EdgeInsets.only(left: 20, top: 20),
                alignment: Alignment.topLeft,
                child: InkWell(
                  onTap: () {},
                  child: Container(
                    width: 30,
                    height: 30,
                    decoration: const BoxDecoration(
                      image: DecorationImage(
                        image: AssetImage('images/back.png'),
                        fit: BoxFit.scaleDown,
                      ),
                    ),
                  ),
                ),
              ),
              Container(
                margin: const EdgeInsets.only(left: 20, top: 20),
                alignment: Alignment.topLeft,
                child: const Text(
                  'Xác minh',
                  style: TextStyle(
                      color: Color.fromRGBO(24, 24, 35, 1),
                      fontWeight: FontWeight.bold,
                      fontSize: 24),
                ),
              ),
              Container(
                margin: const EdgeInsets.only(left: 20, top: 20, right: 20),
                alignment: Alignment.topLeft,
                child: Text(
                  'Nhập mã được gửi đến số điện thoại $phoneHint',
                  style: const TextStyle(
                      color: Color.fromRGBO(90, 90, 90, 1),
                      fontWeight: FontWeight.w400,
                      fontSize: 14),
                ),
              ),
              Container(
                margin: const EdgeInsets.all(20),
                child: Form(
                    child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius: BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          if (value.length == 1) {
                            listValidate[0] = value;
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: const TextStyle(
                            fontSize: 25,
                            color: Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius:
                              const BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          if (value.length == 1) {
                            listValidate[1] = value;
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: const TextStyle(
                            fontSize: 25,
                            color: Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius: BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          if (value.length == 1) {
                            listValidate[2] = value;
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: TextStyle(
                            fontSize: 25,
                            color: const Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius: BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          listValidate[3] = value;
                          if (value.length == 1) {
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: TextStyle(
                            fontSize: 25,
                            color: const Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius: BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          if (value.length == 1) {
                            listValidate[4] = value;
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: TextStyle(
                            fontSize: 25,
                            color: const Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                    Container(
                      decoration: BoxDecoration(
                          border: Border.all(width: 1.0, color: Colors.black12),
                          borderRadius: BorderRadius.all(Radius.circular(10))),
                      height: 40,
                      width: 40,
                      child: TextFormField(
                        //textAlignVertical: TextAlignVertical.center,
                        decoration: const InputDecoration(
                          border: InputBorder.none,
                        ),
                        onChanged: (value) {
                          if (value.length == 1) {
                            listValidate[5] = value;
                            FocusScope.of(context).nextFocus();
                          }
                        },
                        style: const TextStyle(
                            fontSize: 25,
                            color: Color.fromARGB(255, 78, 78, 78)),
                        keyboardType: TextInputType.number,
                        textAlign: TextAlign.center,
                        inputFormatters: [
                          LengthLimitingTextInputFormatter(1),
                          FilteringTextInputFormatter.digitsOnly,
                        ],
                      ),
                    ),
                  ],
                )),
              ),
              SizedBox(
                height: 50,
              ),
              SizedBox(
                height: 40,
                width: 300,
                child: ElevatedButton(
                  onPressed: () {
                    checkOTP();
                  },
                  style: ElevatedButton.styleFrom(
                      foregroundColor: const Color.fromARGB(255, 255, 255, 255),
                      backgroundColor: const Color.fromARGB(255, 75, 121, 227),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(40))),
                  child: const Text('Tiếp tục'),
                ),
              ),
              SizedBox(
                height: 50,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('Không nhận được mã?'),
                  const SizedBox(
                    width: 5,
                  ),
                  InkWell(
                    onTap: () {},
                    child: const Text(
                      'Gửi lại',
                      style: TextStyle(color: Color.fromRGBO(83, 127, 231, 1)),
                    ),
                  ),
                ],
              ),
              SizedBox(
                height: 10,
              ),
              InkWell(
                onTap: () {},
                child: const Text(
                  'Hủy bỏ',
                  style: TextStyle(color: Color.fromRGBO(106, 106, 106, 1)),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Future<void> checkOTP() async {
    String otp = '';
    for (String s in listValidate) {
      otp += s;
    }
    print(otp);
    var url =
        Uri.parse('https://swd-nearex.azurewebsites.net/api/users/verification');
    Map<String, dynamic> body = {
      "accountSID": "AC8e6696ff8faac7281f34047859c4763c",
      "authToken": "367d20fdb4452534a0a36fb4ba09816a",
      "pathServiceSid": "VAac562f462db7c7cead88dc52e5731781",
      "phone": "$phoneNumber",
      "token": "$otp"
    };

    try {
      http.Response response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode(body),
      );

      if (response.statusCode == 200) {
        if (response.body == 'approved') {
          // ignore: use_build_context_synchronously
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => CreatePassword(
                        googleUser: null,
                        phone: phoneNumber,
                      )));
        } else {
          print('Ma xa nhan khong dung');
        }
      } else {
        print('error roi');
      }
    } catch (e) {
      // Xử lý lỗi khi gửi request
      print('Error sending POST request: $e');
    }
  }
}
