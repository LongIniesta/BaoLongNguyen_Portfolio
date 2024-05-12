class CampaignDetail {
  int? id;
  DateTime? dateApply;
  double? percentDiscount;
  double? discount;
  int? minQuantity;
  bool isSave = true;

  CampaignDetail(
      {this.id,
      this.dateApply,
      this.percentDiscount,
      this.discount,
      this.minQuantity});
}
