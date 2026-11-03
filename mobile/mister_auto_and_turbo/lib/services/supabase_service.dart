import 'package:supabase_flutter/supabase_flutter.dart';
import '../config/supabase_config.dart';

class SupabaseService {
  static bool _ready = false;

  static SupabaseClient get client {
    if (!_ready) {
      throw StateError(
        'Supabase not configured. Set URL + anon key in supabase_config.dart',
      );
    }
    return Supabase.instance.client;
  }

  static bool get isReady => _ready;
  static User? get currentUser =>
      _ready ? Supabase.instance.client.auth.currentUser : null;
  static bool get isLoggedIn => currentUser != null;

  static Future<void> initialize() async {
    if (!SupabaseConfig.isConfigured) {
      _ready = false;
      return;
    }
    await Supabase.initialize(
      url: SupabaseConfig.url,
      anonKey: SupabaseConfig.anonKey,
    );
    _ready = true;
  }
}
