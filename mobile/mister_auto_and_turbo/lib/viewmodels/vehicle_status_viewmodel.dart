import 'package:flutter/foundation.dart';
import '../models/repair_job.dart';
import '../models/vehicle.dart';
import 'workshop_viewmodel.dart';

class VehicleStatusViewModel extends ChangeNotifier {
  VehicleStatusViewModel(this._workshop, {required this.vehicle, RepairJob? job})
      : _job = job {
    _workshop.addListener(_onWorkshopChanged);
  }

  final WorkshopViewModel _workshop;
  final Vehicle vehicle;
  RepairJob? _job;
  bool _refreshing = false;

  RepairJob? get job => _job;
  bool get isRefreshing => _refreshing;
  String? get jobCardId => _job?.id;

  String get summary =>
      '${vehicle.registrationNumber} · ${_job?.summaryLabel ?? 'No active repair'}';

  void _onWorkshopChanged() {
    notifyListeners();
  }

  Future<String> refresh() async {
    _refreshing = true;
    notifyListeners();
    try {
      await _workshop.loadDashboardData();
      if (_job == null) {
        return 'No repair job has been created for this vehicle yet.';
      }
      return 'Status refreshed';
    } catch (e) {
      return 'Could not refresh: $e';
    } finally {
      _refreshing = false;
      notifyListeners();
    }
  }

  @override
  void dispose() {
    _workshop.removeListener(_onWorkshopChanged);
    super.dispose();
  }
}
