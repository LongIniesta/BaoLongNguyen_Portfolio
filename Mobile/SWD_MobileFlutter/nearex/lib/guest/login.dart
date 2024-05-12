import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/guest/main-screen.dart';

class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Center(
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
                //alignment: Alignment.bottomCenter,
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  //crossAxisAlignment: CrossAxisAlignment.stretch,
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
                      'Đăng nhập với vai trò',
                      style: TextStyle(
                          color: Color.fromARGB(255, 58, 58, 58),
                          fontWeight: FontWeight.w400,
                          fontSize: 17),
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(
                      height: 60,
                    ),
                    InkWell(
                      onTap: () {
                        print('quanlicuahang');
                      },
                      child: Container(
                        height: 50,
                        width: 300,
                        decoration: const BoxDecoration(
                            borderRadius: BorderRadius.all(
                              Radius.circular(10),
                            ),
                            color: Color.fromRGBO(196, 253, 253, 1)),
                        alignment: Alignment.center,
                        child: Text(
                          'Quản lí cửa hàng',
                          style: GoogleFonts.sourceSansPro(
                            color: const Color.fromARGB(255, 0, 44, 119),
                            fontSize: 22,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(
                      height: 20,
                    ),
                    InkWell(
                      onTap: () {
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => const MainScreen()));
                      },
                      child: Container(
                        height: 50,
                        width: 300,
                        decoration: const BoxDecoration(
                            borderRadius: BorderRadius.all(
                              Radius.circular(10),
                            ),
                            color: Color.fromRGBO(196, 253, 253, 1)),
                        alignment: Alignment.center,
                        child: Text(
                          'Người dùng',
                          style: GoogleFonts.sourceSansPro(
                            color: const Color.fromARGB(255, 0, 44, 119),
                            fontSize: 22,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(
                      height: 30,
                    ),
                    const SizedBox(
                      height: 100,
                    ),
                    InkWell(
                      onTap: () {},
                      child: const Text(
                        'Bạn muốn đăng kí tài khoản cho cửa hàng?',
                        style: TextStyle(decoration: TextDecoration.underline),
                      ),
                    )
                  ],
                )),
          ],
        ),
      ),
    ));
  }
}
