import 'package:flutter/material.dart';

class PasswordToLogin extends StatefulWidget{
  PasswordToLogin({super.key, required this.phone});
  String phone;

  @override
  State<StatefulWidget> createState() {
    return PasswordLoginState(phone);
  }

}

class PasswordLoginState extends State<PasswordToLogin>{
  String phone='';
  var obscureText2 = true;
  var pass = '';
  String phoneHint = '';
  @override
  void initState() {
    super.initState();
    for (int i = 1; i <= phone.length; i++) {
      phoneHint += '*';
    }
    phoneHint +=
        phone.substring(phone.length - 3, phone.length);
  }
  PasswordLoginState(this.phone);

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
              const SizedBox(
                height: 10,
              ),
              const Text(
                'Nhập mật khẩu',
                style: TextStyle(
                    color: Color.fromRGBO(24, 24, 35, 1),
                    fontWeight: FontWeight.bold,
                    fontSize: 24),
              ),
              Text(
                'Nhập mật khẩu cho sđt $phoneHint',
                style: const TextStyle(
                    color: Color.fromRGBO(90, 90, 90, 1),
                    fontWeight: FontWeight.w400,
                    fontSize: 14),
              ),
              const SizedBox(
                height: 50,
              ),
              Container(
                margin: EdgeInsets.only(left: 10, bottom: 5),
                child: Text('Mật khẩu'),
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
                    hintText: 'Nhập mật khẩu',
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
                ),
              ),
              Container(
                margin: EdgeInsets.only(top: 30),
                alignment: Alignment.center,
                // height: 40,
                // width: ,
                child: SizedBox(
                  height: 40,
                  width: 300,
                  child: ElevatedButton(
                    onPressed: () {
                      
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
            ]
            ), 
          ),
      ),
    );
  }
  

}