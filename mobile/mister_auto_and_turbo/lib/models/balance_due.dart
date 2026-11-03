class BalanceDue {
  final String quotationId;
  final String jobId;
  final String vehicleId;
  final double total;
  final double paid;

  const BalanceDue({
    required this.quotationId,
    required this.jobId,
    required this.vehicleId,
    required this.total,
    required this.paid,
  });

  double get balance => total - paid;
}
