import 'package:flutter/foundation.dart';
import 'auth_viewmodel.dart';

class WorkshopViewModel extends ChangeNotifier {
  final AuthViewModel auth;

  WorkshopViewModel(this.auth);

  bool _loading = false;
  String? _error;

  bool get isLoading => _loading;
  String? get error => _error;

  // Placeholder lists – replace with real Supabase fetches
  List<Map<String, dynamic>> bookings = [];
  List<Map<String, dynamic>> quotations = [];
  List<Map<String, dynamic>> history = [];

  Future<void> loadDashboardData() async {
    _loading = true;
    _error = null;
    notifyListeners();
    try {
      // TODO: fetch from Supabase using customer id from auth
      await Future.delayed(const Duration(milliseconds: 300));
    } catch (e) {
      _error = e.toString();
    } finally {
      _loading = false;
      notifyListeners();
    }
  }
}
