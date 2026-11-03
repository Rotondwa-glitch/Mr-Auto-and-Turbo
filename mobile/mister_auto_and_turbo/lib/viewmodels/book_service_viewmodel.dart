import 'package:flutter/foundation.dart';
import '../models/vehicle.dart';
import 'workshop_viewmodel.dart';

enum BookingStep { chooseService, pickTime, confirmed }

class BookServiceViewModel extends ChangeNotifier {
  BookServiceViewModel(this._workshop);

  final WorkshopViewModel _workshop;

  static const List<String> slots = [
    '09:00', '10:00', '11:00', '12:00',
    '13:00', '14:00', '15:00', '16:00',
  ];

  static const List<String> serviceTypes = [
    'Diagnostic',
    'Service',
    'Repair',
    'Other',
  ];

  BookingStep _step = BookingStep.chooseService;
  Vehicle? _vehicle;
  String? _service;
  DateTime? _date;
  String? _time;
  String? _notes;
  bool _busy = false;

  BookingStep get step => _step;
  Vehicle? get vehicle => _vehicle;
  String? get service => _service;
  DateTime? get date => _date;
  String? get time => _time;
  String? get notes => _notes;
  bool get isBusy => _busy;

  void selectVehicle(Vehicle v) {
    _vehicle = v;
    notifyListeners();
  }

  void selectService(String s) {
    _service = s;
    notifyListeners();
  }

  void selectDate(DateTime d) {
    _date = d;
    notifyListeners();
  }

  void selectTime(String t) {
    _time = t;
    notifyListeners();
  }

  void setNotes(String n) {
    _notes = n;
  }

  String? next() {
    if (_vehicle == null || _service == null) {
      return 'Select a vehicle and a service';
    }
    _step = BookingStep.pickTime;
    notifyListeners();
    return null;
  }

  void back() {
    _step = BookingStep.chooseService;
    notifyListeners();
  }

  Future<String?> confirm() async {
    if (_vehicle == null || _service == null) {
      return 'Select a vehicle and a service';
    }
    if (_date == null || _time == null) {
      return 'Pick a date and time';
    }
    _busy = true;
    notifyListeners();
    try {
      await _workshop.loadDashboardData();
      _step = BookingStep.confirmed;
      return null;
    } catch (e) {
      return e.toString();
    } finally {
      _busy = false;
      notifyListeners();
    }
  }
}
