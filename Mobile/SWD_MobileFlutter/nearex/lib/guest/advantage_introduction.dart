import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/common/common_widget.dart';
import 'package:nearex/guest/login.dart';
import 'package:nearex/guest/main-screen.dart';

class AdvantageIntroduction extends StatefulWidget {
  const AdvantageIntroduction({super.key});

  @override
  State<StatefulWidget> createState() {
    return _AdvantageIntroductionState();
  }
}

class _AdvantageIntroductionState extends State<AdvantageIntroduction> {
  double _screenWidth = 0;
  double _screenHeight = 0;
  double itemWidth = 400;
  ScrollController scrollController = ScrollController();
  List<_Item> items = [
    const _Item(
        imageUrl: "images/banner_online.png",
        title: "Online 24/7",
        text:
            "Dễ dàng đặt hàng và thanh toán online. Ngoài ra bạn có thể chọn thanh toán khi nhận hàng tùy vào lựa chọn của bạn."),
    const _Item(
        imageUrl: "images/banner_delivery.png",
        title: "Giao hàng nhanh",
        text:
            "Bạn sẽ nhanh chóng nhận được những sản phẩm gần hết hạn mà không cần quá lo lắng"),
    const _Item(
        imageUrl: "images/banner_choice.png",
        title: "Lựa chọn dễ dàng",
        text:
            "Dễ dàng tìm kiếm các sản phẩm tại các vị trí gần bạn và khu xả đồ gần hết hạn")
  ];

  @override
  Widget build(BuildContext context) {
    _screenWidth = DimensionValue.getScreenWidth(context);
    _screenHeight = DimensionValue.getScreenHeight(context);
    return Scaffold(
        body: Container(
      decoration:
          const BoxDecoration(gradient: ColorBackground.lightBlueGradient),
      child: Column(mainAxisAlignment: MainAxisAlignment.center, children: [
        SizedBox(
          height: _screenHeight * 0.7,
          child: ListView.separated(
              controller: scrollController,
              scrollDirection: Axis.horizontal,
              itemCount: items.length,
              itemBuilder: (context, index) =>
                  buildItemView(item: items[index]),
              separatorBuilder: (context, _) => const SizedBox(width: 12)),
        ),
        Center(
          child: Column(
              mainAxisAlignment: MainAxisAlignment.end,
              // mainAxisSize: MainAxisSize.max,
              children: [
                Text(
                  'View More',
                  style: GoogleFonts.dosis(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: const Color.fromARGB(255, 137, 163, 226)),
                ),
                const SizedBox(
                  height: 20,
                ),
                Container(
                  // margin: EdgeInsets.all(100.0),
                  decoration: const BoxDecoration(
                      gradient: ColorBackground.blueGradient,
                      shape: BoxShape.circle),
                  child: IconButton(
                      onPressed: _onPressed,
                      icon: const Icon(Icons.arrow_forward_ios_rounded),
                      color: Colors.white),
                ),
              ]),
        )
      ]),
    ));
  }

  Widget buildItemView({required _Item item}) {
    return SizedBox(
      width: _screenWidth,
      child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          mainAxisSize: MainAxisSize.max,
          children: [
            Image.asset(
              item.imageUrl,
              fit: BoxFit.cover,
            ),
            Text(
              item.title,
              style: GoogleFonts.inter(
                  fontSize: 26,
                  fontWeight: FontWeight.bold,
                  color: const Color.fromARGB(255, 66, 109, 212)),
              textAlign: TextAlign.center,
            ),
            const SizedBox(
              height: 20,
            ),
            SizedBox(
                width: _screenWidth * 0.7,
                child: Text(
                  item.text,
                  style: GoogleFonts.sourceSansPro(
                      fontSize: 17,
                      color: const Color.fromARGB(255, 92, 90, 120)),
                  textAlign: TextAlign.center,
                ))
          ]),
    );
  }

  void _onPressed() {
    int index = (scrollController.offset / itemWidth).round();
    if (index >= 0 && index < items.length - 1) {
      // animate to next item on press button
      scrollController.animateTo(itemWidth * (index + 1),
          duration: const Duration(seconds: 1), curve: Curves.easeInOut);
    } else if (index == items.length - 1) {
      // navigate to register screen
      Navigator.push(
          context, MaterialPageRoute(builder: (context) => const MainScreen()));

      // navigate in animated way
      // Navigator.of(context).push(PageRouteBuilder(
      //   pageBuilder: (context, animation, secondaryAnimation) =>
      //       const MainScreen(),
      //   transitionsBuilder: (context, animation, secondaryAnimation, child) {
      //     return child;
      //   },
      // ));
    }
  }
}

class _Item {
  final String imageUrl;
  final String title;
  final String text;

  const _Item(
      {required this.imageUrl, required this.title, required this.text});
}
