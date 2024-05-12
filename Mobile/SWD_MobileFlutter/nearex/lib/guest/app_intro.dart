import 'package:flutter/material.dart';
import 'package:flutter_animate/flutter_animate.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:nearex/common/common_widget.dart';
import 'package:nearex/guest/advantage_introduction.dart';

class AppStart extends StatefulWidget {
  const AppStart({super.key});

  @override
  State<StatefulWidget> createState() {
    return _AppStartState();
  }
}

class _AppStartState extends State<AppStart> with TickerProviderStateMixin {
  @override
  Widget build(BuildContext context) {
    return Container(
        decoration:
            const BoxDecoration(gradient: ColorBackground.lightBlueGradient),
        child: Center(
            child: Animate(
          effects: const [FadeEffect(), SlideEffect()],
          delay: const Duration(milliseconds: 500),
          child: Image.asset('images/logo_simple.png'),
          onComplete: (controller) => {
            Navigator.push(context,
                MaterialPageRoute(builder: (context) => const AppIntro()))
          },
        )));
  }
}

class AppIntro extends StatefulWidget {
  const AppIntro({super.key});

  @override
  State<StatefulWidget> createState() {
    return _AppIntroState();
  }
}

class _AppIntroState extends State<AppIntro> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Container(
            width: double.maxFinite,
            decoration: const BoxDecoration(color: ColorBackground.eerieBlack),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.end,
              mainAxisSize: MainAxisSize.max,
              children: [
                Image.asset('images/logo_slogan.png'),
                const Text(
                  'Cuối tháng hết tiền? Một bữa no với chi phí cực rẻ? Tại sao không?',
                  style: TextStyle(color: Color.fromARGB(255, 175, 175, 175)),
                ),
                SizedBox(height: 10,),
                InkWell(
                    onTap: _onTap,
                    child: Container(
                      width: DimensionValue.getScreenWidth(context) * 0.5,
                      decoration: BoxDecoration(
                          shape: BoxShape.rectangle,
                          borderRadius: BorderRadius.circular(50),
                          color: ColorBackground.bubbles),
                      child: Padding(
                        padding: const EdgeInsets.all(10),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Text(
                              'Mua Hàng Ngay',
                              style: GoogleFonts.outfit(
                                  color: ColorBackground.blueberry,
                                  fontSize: 16),
                            ),
                            const SizedBox(
                              width: 5,
                            ),
                            Container(
                              width: DimensionValue.getScreenWidth(context) *
                                  0.5 *
                                  0.25,
                              decoration: BoxDecoration(
                                  shape: BoxShape.rectangle,
                                  borderRadius: BorderRadius.circular(10),
                                  gradient: ColorBackground.blueGradient),
                              child: const Icon(
                                Icons.arrow_forward_rounded,
                                color: ColorBackground.bubbles,
                              ),
                            )
                          ],
                        ),
                      ),
                    )),
                Image.asset('images/banner_buy.png')
              ],
            )));
  }

  void _onTap() {
    Navigator.push(context,
        MaterialPageRoute(builder: (context) => const AdvantageIntroduction()));
  }
}
