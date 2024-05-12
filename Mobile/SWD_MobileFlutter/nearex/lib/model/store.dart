import 'dart:convert';

class Store {
  
   int? id;
   String? storeName;
   String? phone;
   String? address;
   String? logo;
 String? token;
   String? coordinateString;

  Store({required this.id, required this.storeName, required this.phone, required this.address, required this.logo, required this.token, required this.coordinateString});
  factory Store.fromJson(Map<String, dynamic> json) {
    return Store(
      id: json['id'],
      storeName: json['storeName'],
      phone: json['phone'],
      address: json['address'],
      logo: json['logo'],
      token: json['token'],
      coordinateString: json['coordinateString']
    );
  }
}
Store parseJson(String jsonString) {
  final jsonData = json.decode(jsonString);
  return Store.fromJson(jsonData);
}