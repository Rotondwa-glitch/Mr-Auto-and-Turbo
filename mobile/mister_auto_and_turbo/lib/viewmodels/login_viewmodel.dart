import 'package:flutter/foundation.dart';
import 'auth_viewmodel.dart';

/// State of the Log In screen.
class LoginViewModel extends ChangeNotifier {
  LoginViewModel(this._auth);

  final AuthViewModel _auth;
  String? _error;
  bool _busy = false;

  String? get error => _error;
  bool get isBusy => _busy;

  Future<bool> login(String email, String password) async {
    _busy = true;
    _error = null;
    notifyListeners();

    final ok = await _auth.login(email.trim(), password);
    if (!ok) {
      _error = _auth.error ?? 'Login failed';
    }

    _busy = false;
    notifyListeners();
    return ok;
  }

  void clearError() {
    if (_error == null) return;
    _error = null;
    notifyListeners();
  }
}
