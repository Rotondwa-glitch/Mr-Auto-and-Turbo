class Quotation {
  final String id;
  final String customerId;
  final String vehicleId;
  final String status;
  final double totalAmount;
  final DateTime createdAt;
  final String notes;

  Quotation({
    required this.id,
    required this.customerId,
    required this.vehicleId,
    required this.status,
    required this.totalAmount,
    required this.createdAt,
    required this.notes,
  });

  factory Quotation.fromJson(Map<String, dynamic> json) {
    return Quotation(
      id: json['id'] as String,
      customerId: json['customer_id'] as String? ?? '',
      vehicleId: json['vehicle_id'] as String? ?? '',
      status: json['status'] as String? ?? 'Draft',
      totalAmount: (json['total_amount'] as num?)?.toDouble() ?? 0,
      createdAt: DateTime.tryParse(json['created_at'] as String? ?? '') ?? DateTime.now(),
      notes: json['notes'] as String? ?? '',
    );
  }
}
