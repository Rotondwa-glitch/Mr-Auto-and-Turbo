import 'package:flutter/foundation.dart';
import '../models/vehicle.dart';
import '../models/repair_job.dart';
import '../models/quotation.dart';
import '../models/notification_item.dart';
import 'auth_viewmodel.dart';

/// Holds customer workshop state (vehicles, jobs, quotes, notifications).
/// Uses demo data when Supabase is offline.
class WorkshopViewModel extends ChangeNotifier {
  WorkshopViewModel(this.auth);

  final AuthViewModel auth;

  final List<Vehicle> vehicles = [
    const Vehicle(
      id: 'v1',
      make: 'Toyota',
      model: 'Corolla',
      plate: 'ABC 123 GP',
      year: 2019,
      mileage: 85000,
    ),
  ];

  final List<RepairJob> jobs = [];
  final List<Quotation> quotations = [];
  final List<NotificationItem> notifications = [];

  void seedDemoJob() {
    if (jobs.isNotEmpty || vehicles.isEmpty) return;
    jobs.add(
      RepairJob(
        id: 'job-demo',
        vehicle: vehicles.first,
        title: 'Service & inspection',
        stage: JobStage.inspection,
      ),
    );
    notifyListeners();
  }
}
