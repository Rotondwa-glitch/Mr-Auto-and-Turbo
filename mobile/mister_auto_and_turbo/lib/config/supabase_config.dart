/// Supabase project settings shared with the ASP.NET staff app.
/// Replace these with your project values before enabling live data.
class SupabaseConfig {
  static const String url = String.fromEnvironment(
    'SUPABASE_URL',
    defaultValue: 'https://YOUR_PROJECT.supabase.co',
  );

  static const String anonKey = String.fromEnvironment(
    'SUPABASE_ANON_KEY',
    defaultValue: 'YOUR_ANON_KEY',
  );

  static bool get isConfigured =>
      !url.contains('YOUR_') && !anonKey.contains('YOUR_');
}
