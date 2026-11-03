class Vehicle {
  final String id;
  final String make;
  final String model;
  final String plate;
  final int year;
  final int mileage;

  const Vehicle({
    required this.id,
    required this.make,
    required this.model,
    required this.plate,
    required this.year,
    this.mileage = 0,
  });

  String get displayName => '$make $model';
  String get registrationNumber => plate;

  factory Vehicle.fromMap(Map<String, dynamic> m) => Vehicle(
        id: m['id'] as String? ?? '',
        make: m['make'] as String? ?? '',
        model: m['model'] as String? ?? '',
        plate: m['plate'] as String? ?? m['registration_number'] as String? ?? '',
        year: (m['year'] as num?)?.toInt() ?? 0,
        mileage: (m['mileage'] as num?)?.toInt() ?? 0,
      );

  Map<String, dynamic> toMap() => {
        'id': id,
        'make': make,
        'model': model,
        'plate': plate,
        'year': year,
        'mileage': mileage,
      };
}
