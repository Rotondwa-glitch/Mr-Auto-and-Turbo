import 'package:flutter/foundation.dart';
import 'workshop_viewmodel.dart';

class ComplaintViewModel extends ChangeNotifier {
  ComplaintViewModel(this._workshop);

  final WorkshopViewModel _workshop;

  String? _type;
  String? _jobId;
  String? _vehicleId;
  bool _busy = false;
  bool _sent = false;

  String? get type => _type;
  bool get isBusy => _busy;
  bool get sent => _sent;

  void selectType(String t) {
    _type = t;
    notifyListeners();
  }

  void linkJob(String? jobId, String? vehicleId) {
    _jobId = jobId;
    _vehicleId = vehicleId;
  }

  Future<String?> submit(String description) async {
    if (_type == null || _type!.isEmpty) {
      return 'Select a complaint type';
    }
    if (description.trim().isEmpty) {
      return 'Enter a description';
    }
    _busy = true;
    notifyListeners();
    try {
      // Wire to Supabase / workshop repository in full implementation
      await Future.delayed(const Duration(milliseconds: 400));
      _sent = true;
      return null;
    } catch (e) {
      return e.toString();
    } finally {
      _busy = false;
      notifyListeners();
    }
  }
}
