import 'package:flutter/material.dart';
import 'package:geocoding/geocoding.dart';
import 'package:nearex/place/mapscreen.dart';

class AddressPickerScreen extends StatefulWidget {
  @override
  _AddressPickerScreenState createState() => _AddressPickerScreenState();
}

class _AddressPickerScreenState extends State<AddressPickerScreen> {
  TextEditingController _addressController = TextEditingController();

  @override
  void dispose() {
    _addressController.dispose();
    super.dispose();
  }

  Future<void> _pickAddress() async {
    String enteredAddress = _addressController.text;
    List<Location> locations = await locationFromAddress(enteredAddress);

    if (locations.isNotEmpty) {
      Location firstLocation = locations.first;
     // print('Địa chỉ được chọn: ${locations.first.formattedAddress}');
      // ignore: use_build_context_synchronously
      print('Tọa độ:'+ firstLocation.latitude.toString() + firstLocation.longitude.toString());
      Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => MapScreen(latitude: firstLocation.latitude, longitude: firstLocation.longitude)));
    } else {
      print('Không tìm thấy địa chỉ');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Pick Địa chỉ'),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(
              controller: _addressController,
              decoration: InputDecoration(
                labelText: 'Nhập địa chỉ',
              ),
            ),
            SizedBox(height: 16.0),
            ElevatedButton(
              onPressed: _pickAddress,
              child: Text('Pick'),
            ),
          ],
        ),
      ),
    );
  }
}
