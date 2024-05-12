import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class NotificationStoreScreen extends StatefulWidget {
  const NotificationStoreScreen({super.key});

  @override
  State<NotificationStoreScreen> createState() =>
      _NotificationStoreScreenState();
}

class _NotificationStoreScreenState extends State<NotificationStoreScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          color: Color.fromRGBO(233, 248, 249, 1),
          child: Stack(
            children: [
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
                    decoration: BoxDecoration(
                        shape: BoxShape.circle,
                        color: Color.fromARGB(95, 206, 206, 206)),
                    child: Icon(
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
                  margin: EdgeInsets.only(top: 65),
                  child: Text(
                    'Thông báo',
                    style: GoogleFonts.outfit(
                        fontSize: 25,
                        fontWeight: FontWeight.bold,
                        color: Color.fromARGB(255, 15, 14, 36)),
                  ),
                ),
              ),
              Container(
                margin: EdgeInsets.only(top: 120),
                height: double.maxFinite,
                width: double.maxFinite,
                child: Column(
                  children: [
                    Container(
                      margin: EdgeInsets.all(5),
                      padding: EdgeInsets.all(15),
                      // height: 100,
                      width: double.maxFinite,
                      decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(10)),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'Nhận thanh toán',
                            style: GoogleFonts.outfit(
                                fontSize: 25, fontWeight: FontWeight.bold),
                          ),
                          SizedBox(
                            height: 5,
                          ),
                          Text(
                            '12:12:12 28/10/2023',
                            style: GoogleFonts.outfit(
                                fontSize: 13,
                                color: Color.fromARGB(255, 0, 16, 195)),
                          ),
                          SizedBox(
                            height: 5,
                          ),
                          Text(
                            'Nguyễn Bảo Long - 0929828328 \nĐã đặt đơn\nSL: 5 ',
                            style: GoogleFonts.outfit(
                              fontSize: 15,
                            ),
                          ),
                          SizedBox(
                            height: 5,
                          ),
                        ],
                      ),
                    )
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
