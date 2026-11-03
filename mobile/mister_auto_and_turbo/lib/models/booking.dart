class Booking {
  final String id;
  final String customerId;
  final String vehicleId;
  final DateTime scheduledAt;
  final String status;
  final String serviceType;
  final String description;

  Booking({
    required this.id,
    required this.customerId,
    required this.vehicleId,
    required this.scheduledAt,
    required this.status,
    required this.serviceType,
    required this.description,
  });

  factory Booking.fromJson(Map<String, dynamic> json) {
    return Booking(
      id: json['id'] as String,
      customerId: json['customer_id'] as String? ?? '',
      vehicleId: json['vehicle_id'] as String? ?? '',
      scheduledAt: DateTime.tryParse(json['scheduled_at'] as String? ?? '') ?? DateTime.now(),
      status: json['status'] as String? ?? 'Pending',
      serviceType: json['service_type'] as String? ?? '',
      description: json['description'] as String? ?? '',
    );
  }
}
