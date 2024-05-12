import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:latlong2/latlong.dart';


class MapScreen extends StatelessWidget {
  final double latitude;
  final double longitude;

  const MapScreen({Key? key, required this.latitude, required this.longitude})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Stack(
          children: [
            Flexible(
              child: FlutterMap(
                options: MapOptions(center: LatLng(latitude,longitude), zoom: 16),
                layers: [
                  TileLayerOptions(
                    urlTemplate: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                    subdomains: ['a', 'b', 'c']
                  ),
                  MarkerLayerOptions(
                    markers: [
                      Marker(point: LatLng(latitude,longitude), builder: (ctx) => Image.asset('images/nearexpin.png', width: 100, height: 100,),),
                    ]
                  )
                ],
              ),
            ),

            Align(
              alignment: Alignment.bottomCenter,
              child: InkWell(
                onTap: (){Navigator.pop(context);},
                child: Container(
                  
                  alignment: Alignment.center,
                  margin: const EdgeInsets.only(bottom: 20),
                  height: 50,
                  width: 200,
                  decoration: BoxDecoration(
                    color: const Color.fromARGB(255, 164, 222, 255),
                    borderRadius: BorderRadius.circular(10)
                  ),
                  child: Text('Xác nhận', style: GoogleFonts.sourceSansPro(
                    color: const Color.fromARGB(255, 8, 0, 231) ,
                    fontSize: 20,
                    fontWeight: FontWeight.bold
                  ),),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
