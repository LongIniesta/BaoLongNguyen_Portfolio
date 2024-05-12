import 'package:nearex/model/payment.dart';

import 'customer.dart';

class Order{
  int? id;
  DateTime? orderDate;
  int? status;
  int? quantity;
  int? campaignId;
  int? customerId;
  String? productName;
  double? unitPrice;
  String? productImg;
  String? storeName;
  Customer? customer;
  List<Payments>? payments;
  Order(
      {this.id,
      this.orderDate,
      this.status,
      this.quantity,
      this.campaignId,
      this.customerId,
      this.productName,
      this.unitPrice,
      this.productImg,
      this.storeName,
      this.customer,
      this.payments});
}