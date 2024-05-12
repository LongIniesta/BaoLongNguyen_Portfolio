import 'package:flutter/services.dart';
import 'package:nearex/model/campaigndetail.dart';

class Campaign {
  int? id;
  DateTime? startDate;
  DateTime? endDate;
  int? status;
  DateTime? exp;
  int? productId;
  int? quantity;
  String? productName;
  double? price;
  List<CampaignDetail>? listCampaignDetail;
  String? image;

  Campaign(
      {this.id,
      this.startDate,
      this.endDate,
      this.status,
      this.exp,
      this.productId,
      this.quantity,
      this.productName,
      this.price,
      this.listCampaignDetail,
      this.image});
}
