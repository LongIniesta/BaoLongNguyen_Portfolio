import 'package:flutter/material.dart';

class ConfirmDone extends StatelessWidget{
  const ConfirmDone({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        width: double.maxFinite,
        decoration: BoxDecoration(color: Colors.white, ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Image.asset('images/confirmdone.png', height: 300, width: 300,),
            const SizedBox(height: 20,),
            const Text(
                'Xác minh thành công',
                style: TextStyle(
                    color: Color.fromRGBO(24, 24, 35, 1),
                    fontWeight: FontWeight.bold,
                    fontSize: 24),
              ),
              const SizedBox(height: 20,),
              SizedBox(
              height: 40,
              width: 300,
              child: ElevatedButton(
                onPressed: () {
                  // Xử lý sự kiện khi nút được nhấn
                },
                style: ElevatedButton.styleFrom(
                    foregroundColor: const Color.fromARGB(255, 255, 255, 255),
                    backgroundColor: const Color.fromARGB(255, 75, 121, 227),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(40))),
                child: const Text('Tiếp tục'),
              ),
            ),
          ],
        ),
      ),
    );
  }
}