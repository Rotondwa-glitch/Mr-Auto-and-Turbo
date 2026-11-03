class Customer {
  final String id;
  final String fullName;
  final String email;
  final String phone;
  final String address;

  const Customer({
    required this.id,
    required this.fullName,
    required this.email,
    required this.phone,
    this.address = '',
  });

  factory Customer.fromJson(Map<String, dynamic> json) {
    return Customer(
      id: json['id'] as String? ?? '',
      fullName: json['full_name'] as String? ?? json['fullName'] as String? ?? '',
      email: json['email'] as String? ?? '',
      phone: json['phone'] as String? ?? '',
      address: json['address'] as String? ?? '',
    );
  }
}
