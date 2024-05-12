import 'dart:convert';
import 'dart:typed_data';
class Product {
  int? id;
  double? price;
  String? origin;
  String? productImg;
  String? productName;
  String? description;
  String? unit;
  int? netWeight;
  int? categoryId;
  int? storeId;
  int? status;
  Uint8List? image;


  Product(
      {this.id,
      this.price,
      this.origin,
      this.productImg,
      this.productName,
      this.description,
      this.unit,
      this.netWeight,
      this.categoryId,
      this.storeId,
      this.status,
      this.image
      });

  Product.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    price = json['price'];
    origin = json['origin'];
    productImg = json['productImg'];
    productName = json['productName'];
    description = json['description'];
    unit = json['unit'];
    netWeight = json['netWeight'];
    categoryId = json['categoryId'];
    storeId = json['storeId'];
    status = json['status'];
  }
  

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['price'] = this.price;
    data['origin'] = this.origin;
    data['productImg'] = this.productImg;
    data['productName'] = this.productName;
    data['description'] = this.description;
    data['unit'] = this.unit;
    data['netWeight'] = this.netWeight;
    data['categoryId'] = this.categoryId;
    data['storeId'] = this.storeId;
    data['status'] = this.status;
    return data;
  }
  
}

