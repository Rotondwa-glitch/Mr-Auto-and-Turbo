import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'routes/route_manager.dart';
import 'services/supabase_service.dart';
import 'theme.dart';
import 'viewmodels/auth_viewmodel.dart';
import 'viewmodels/workshop_viewmodel.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  try {
    await SupabaseService.initialize();
  } catch (e) {
    debugPrint('Supabase init failed: $e');
  }

  runApp(const MisterAutoApp());
}

/// Mister Auto & Turbo – customer app (Flutter / NeoGradient UI).
/// Architecture: MVVM with Provider + Supabase.
class MisterAutoApp extends StatelessWidget {
  const MisterAutoApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => AuthViewModel()),
        ChangeNotifierProvider(
          create: (context) => WorkshopViewModel(context.read<AuthViewModel>()),
        ),
      ],
      child: MaterialApp(
        title: 'Mister Auto & Turbo',
        debugShowCheckedModeBanner: false,
        theme: buildTheme(),
        initialRoute: RouteManager.splash,
        onGenerateRoute: RouteManager.generateRoute,
      ),
    );
  }
}
