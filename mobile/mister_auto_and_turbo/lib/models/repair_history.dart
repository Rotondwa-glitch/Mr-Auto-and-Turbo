class RepairHistory {
  final String id;
  final String vehicleId;
  final String title;
  final DateTime completedAt;
  final String notes;

  const RepairHistory({
    required this.id,
    required this.vehicleId,
    required this.title,
    required this.completedAt,
    this.notes = '',
  });
}
