import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:nearex/guest/createaccount.dart';

class CreatePassword extends StatefulWidget {
  CreatePassword({super.key, required this.googleUser, required this.phone});

  GoogleSignInAccount? googleUser;
  String? phone;

  @override
  State<StatefulWidget> createState() {
    return CreatePasswordState(googleUser, phone);
  }
}

class CreatePasswordState extends State<CreatePassword> {
  var obscureText = true;
  var obscureText2 = true;
  var pass = '';
  var confirm = '';
  var error = '';
  String email='';
  @override
  void initState() {
    super.initState();
    if (googleUser != null) email = googleUser!.email;
  }

  GoogleSignInAccount? googleUser;
  String? phone;

  CreatePasswordState(this.googleUser, this.phone);

  void validatePass(String pass, String confirm) {
    String result = '';
    if (pass.isEmpty || confirm.isEmpty) {
      result = 'Vui lòng điền hết các ô';
    } else if (pass.length < 8 || pass.length > 30) {
      result = 'Mật khẩu phải dài từ 8-30 ký tự';
    } else if (pass.contains(' ')) {
      result = 'Mật khẩu không được chứa khoảng trắng';
    } else if (pass != confirm) {
      result = 'Mật khẩu không khớp';
    } else
      result = '';

    setState(() {
      error = result;
    });

    if (error== ''){
        Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => CreateAccount(password: pass, phoneNumber: phone, googleUser: googleUser,)));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          padding: EdgeInsets.all(30),
          decoration: BoxDecoration(color: Colors.white),
          width: double.maxFinite,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              SizedBox(
                height: 10,
              ),
              const Text(
                'Nhập mật khẩu',
                style: TextStyle(
                    color: Color.fromRGBO(24, 24, 35, 1),
                    fontWeight: FontWeight.bold,
                    fontSize: 24),
              ),
              if (phone != null)
                 Text(
                  'Cài đặt mật khẩu cho sđt $phone',
                  style: const TextStyle(
                      color: Color.fromRGBO(90, 90, 90, 1),
                      fontWeight: FontWeight.w400,
                      fontSize: 14),
                )
              else
                Text(
                  'Cài đặt mật khẩu cho email $email',
                  style: const TextStyle(
                      color: Color.fromRGBO(90, 90, 90, 1),
                      fontWeight: FontWeight.w400,
                      fontSize: 14),
                ),
              const SizedBox(
                height: 50,
              ),
              Container(
                margin: const EdgeInsets.only(left: 10, bottom: 5),
                child: const Text('Mật khẩu mới'),
              ),
              Container(
                padding: const EdgeInsets.only(
                  left: 10,
                ),
                decoration: BoxDecoration(
                    border: Border.all(width: 1.0, color: Colors.black12),
                    borderRadius: const BorderRadius.all(Radius.circular(10))),
                height: 40,
                width: 400,
                child: TextFormField(
                  obscureText: obscureText2,
                  //textAlignVertical: TextAlignVertical.center,
                  decoration: InputDecoration(
                    suffixIcon: GestureDetector(
                      onTap: () {
                        setState(() {
                          obscureText2 = !obscureText2;
                        });
                      },
                      child: obscureText2
                          ? const Icon(
                              Icons.visibility_off,
                              color: Color.fromARGB(255, 203, 230, 244),
                            )
                          : const Icon(
                              Icons.visibility,
                              color: Color.fromARGB(255, 203, 230, 244),
                            ),
                    ),
                    hintText: 'Nhập mật khẩu mới',
                    hintStyle: const TextStyle(
                        fontSize: 18,
                        color: Color.fromARGB(255, 179, 179, 179)),
                    border: InputBorder.none,
                  ),
                  onChanged: (value) {
                    pass = value;
                  },
                  style: const TextStyle(
                      fontSize: 20, color: Color.fromARGB(255, 78, 78, 78)),
                  keyboardType: TextInputType.visiblePassword,
                  textAlign: TextAlign.start,
                  inputFormatters: [
                    LengthLimitingTextInputFormatter(30),
                  ],
                ),
              ),
              const SizedBox(
                height: 20,
              ),
              Container(
                margin: const EdgeInsets.only(left: 10, bottom: 5),
                child: const Text('Xác nhận mật khẩu'),
              ),
              Container(
                padding: const EdgeInsets.only(
                  left: 10,
                ),
                decoration: BoxDecoration(
                    border: Border.all(width: 1.0, color: Colors.black12),
                    borderRadius: const BorderRadius.all(Radius.circular(10))),
                height: 40,
                width: 400,
                child: TextFormField(
                  obscureText: obscureText,
                  //textAlignVertical: TextAlignVertical.center,
                  decoration: InputDecoration(
                    hintText: 'Xác nhận mật khẩu',
                    suffixIcon: GestureDetector(
                      onTap: () {
                        setState(() {
                          obscureText = !obscureText;
                        });
                      },
                      child: obscureText
                          ? const Icon(
                              Icons.visibility_off,
                              color: Color.fromARGB(255, 203, 230, 244),
                            )
                          : const Icon(
                              Icons.visibility,
                              color: Color.fromARGB(255, 203, 230, 244),
                            ),
                    ),
                    hintStyle: const TextStyle(
                        fontSize: 18,
                        color: Color.fromARGB(255, 179, 179, 179)),
                    border: InputBorder.none,
                  ),
                  onChanged: (value) {
                    confirm = value;
                  },
                  style: const TextStyle(
                      fontSize: 20, color: Color.fromARGB(255, 34, 34, 34)),
                  keyboardType: TextInputType.visiblePassword,
                  textAlign: TextAlign.start,
                  inputFormatters: [
                    LengthLimitingTextInputFormatter(30),
                  ],
                ),
              ),
              Container(
                margin: const EdgeInsets.only(top: 30),
                alignment: Alignment.center,
                // height: 40,
                // width: ,
                child: SizedBox(
                  height: 40,
                  width: 300,
                  child: ElevatedButton(
                    onPressed: () {
                      validatePass(pass, confirm);
                      if (error == '') {
                        print('chuyen to man hinh dang ky');
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
              ),
              Container(
                margin: const EdgeInsets.only(top: 10),
                alignment: Alignment.center,
                child: Text(error,
                    style: const TextStyle(color: Color.fromARGB(255, 144, 0, 0))),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
