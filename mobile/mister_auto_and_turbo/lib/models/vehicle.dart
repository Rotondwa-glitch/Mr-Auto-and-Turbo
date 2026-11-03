class Vehicle {
  final String id;
  final String customerId;
  final String registrationNumber;
  final String make;
  final String model;
  final int year;
  final int mileage;

  Vehicle({
    required this.id,
    required this.customerId,
    required this.registrationNumber,
    required this.make,
    required this.model,
    required this.year,
    required this.mileage,
  });

  factory Vehicle.fromJson(Map<String, dynamic> json) {
    return Vehicle(
      id: json['id'] as String,
      customerId: json['customer_id'] as String? ?? '',
      registrationNumber: json['registration_number'] as String? ?? '',
      make: json['make'] as String? ?? '',
      model: json['model'] as String? ?? '',
      year: json['year'] as int? ?? 0,
      mileage: json['mileage'] as int? ?? 0,
    );
  }
}
