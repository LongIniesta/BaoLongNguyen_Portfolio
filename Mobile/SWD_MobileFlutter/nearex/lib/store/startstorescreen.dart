import 'package:flutter/material.dart';
import 'package:nearex/model/store.dart';

class StartStoreScreen extends StatefulWidget{
  StartStoreScreen({super.key, required this.store});
  Store store;

  @override
  State<StatefulWidget> createState() {
    return StartStoreScreenState(store);
  }

}

class StartStoreScreenState extends State<StartStoreScreen>{
  Store store;
  StartStoreScreenState(this.store);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        child: Text('StartStoreScreen'),
      ),
    );
  }

}