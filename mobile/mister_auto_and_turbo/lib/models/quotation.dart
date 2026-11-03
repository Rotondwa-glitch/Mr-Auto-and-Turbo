class Quotation {
  final String id;
  final String vehicleId;
  final double totalAmount;
  final String status;
  final String notes;
  final DateTime createdAt;

  const Quotation({
    required this.id,
    required this.vehicleId,
    required this.totalAmount,
    required this.status,
    this.notes = '',
    required this.createdAt,
  });

  factory Quotation.fromMap(Map<String, dynamic> m) => Quotation(
        id: m['id'] as String? ?? '',
        vehicleId: m['vehicle_id'] as String? ?? '',
        totalAmount: (m['total_amount'] as num?)?.toDouble() ?? 0,
        status: m['status'] as String? ?? 'Pending',
        notes: m['notes'] as String? ?? '',
        createdAt: DateTime.tryParse(m['created_at']?.toString() ?? '') ?? DateTime.now(),
      );
}
