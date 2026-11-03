import 'package:flutter/foundation.dart';
import '../services/supabase_service.dart';

class AuthViewModel extends ChangeNotifier {
  bool _isLoading = false;
  String? _error;
  bool _demoLoggedIn = false;

  bool get isLoading => _isLoading;
  String? get error => _error;
  bool get isLoggedIn => _demoLoggedIn || SupabaseService.isLoggedIn;

  Future<bool> login(String email, String password) async {
    _isLoading = true;
    _error = null;
    notifyListeners();

    try {
      if (!SupabaseService.isReady) {
        // Demo mode: accept any non-empty credentials so UI is testable offline
        if (email.isEmpty || password.isEmpty) {
          _error = 'Enter email and password';
          _isLoading = false;
          notifyListeners();
          return false;
        }
        _demoLoggedIn = true;
        _isLoading = false;
        notifyListeners();
        return true;
      }

      await SupabaseService.client.auth.signInWithPassword(
        email: email,
        password: password,
      );
      _isLoading = false;
      notifyListeners();
      return true;
    } catch (e) {
      _error = e.toString();
      _isLoading = false;
      notifyListeners();
      return false;
    }
  }

  Future<void> logout() async {
    _demoLoggedIn = false;
    if (SupabaseService.isReady) {
      await SupabaseService.client.auth.signOut();
    }
    notifyListeners();
  }
}
