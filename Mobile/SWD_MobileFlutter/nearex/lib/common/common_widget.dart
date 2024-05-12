import 'package:flutter/material.dart';

class ColorBackground {
  static const Color bubbles = Color.fromARGB(255, 233, 248, 249);
  static const Color blueberry = Color.fromARGB(255, 83, 127, 231);
  static const Color diamond = Color.fromARGB(255, 192, 238, 242);
  static const Color eerieBlack = Color.fromARGB(255, 24, 24, 35);

  static const LinearGradient lightBlueGradient = LinearGradient(
      colors: [diamond, bubbles],
      begin: Alignment.topLeft,
      end: Alignment.bottomRight);

  static const LinearGradient blueGradient = LinearGradient(
      colors: [blueberry, diamond],
      begin: Alignment.topLeft,
      end: Alignment.bottomRight);
}

class DimensionValue {
  static double getScreenWidth(BuildContext context) {
    return MediaQuery.of(context).size.width;
  }

  static double getScreenHeight(BuildContext context) {
    return MediaQuery.of(context).size.height;
  }
}
