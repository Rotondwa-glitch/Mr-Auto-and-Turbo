import 'package:flutter/foundation.dart';
import 'workshop_viewmodel.dart';

class ProofOfPaymentViewModel extends ChangeNotifier {
  ProofOfPaymentViewModel(this._workshop);

  final WorkshopViewModel _workshop;

  String? _reference;
  double? _amount;
  String? _jobCardId;
  bool _busy = false;
  bool _submitted = false;

  String? get reference => _reference;
  double? get amount => _amount;
  bool get isBusy => _busy;
  bool get submitted => _submitted;

  void setReference(String v) {
    _reference = v.trim();
  }

  void setAmount(double? v) {
    _amount = v;
  }

  void linkJob(String? jobCardId) {
    _jobCardId = jobCardId;
  }

  Future<String?> submit() async {
    if (_reference == null || _reference!.isEmpty) {
      return 'Enter payment reference';
    }
    _busy = true;
    notifyListeners();
    try {
      await Future.delayed(const Duration(milliseconds: 400));
      _submitted = true;
      return null;
    } catch (e) {
      return e.toString();
    } finally {
      _busy = false;
      notifyListeners();
    }
  }
}
