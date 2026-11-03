import 'package:flutter/material.dart';
import '../views/splash_view.dart';
import '../views/login_view.dart';
import '../views/register_view.dart';
import '../views/dashboard_view.dart';
import '../views/book_service_view.dart';
import '../views/my_bookings_view.dart';
import '../views/history_view.dart';
import '../views/complaints_view.dart';

class RouteManager {
  static const String splash = '/';
  static const String login = '/login';
  static const String register = '/register';
  static const String dashboard = '/dashboard';
  static const String bookService = '/book-service';
  static const String myBookings = '/my-bookings';
  static const String history = '/history';
  static const String quotations = '/quotations';
  static const String notifications = '/notifications';
  static const String complaints = '/complaints';
  static const String profile = '/profile';

  static Route<dynamic> generateRoute(RouteSettings settings) {
    switch (settings.name) {
      case splash:
        return MaterialPageRoute(builder: (_) => const SplashView());
      case login:
        return MaterialPageRoute(builder: (_) => const LoginView());
      case register:
        return MaterialPageRoute(builder: (_) => const RegisterView());
      case dashboard:
        return MaterialPageRoute(builder: (_) => const DashboardView());
      case bookService:
        return MaterialPageRoute(builder: (_) => const BookServiceView());
      case myBookings:
        return MaterialPageRoute(builder: (_) => const MyBookingsView());
      case history:
        return MaterialPageRoute(builder: (_) => const HistoryView());
      case complaints:
        return MaterialPageRoute(builder: (_) => const ComplaintsView());
      default:
        return MaterialPageRoute(
          builder: (_) => Scaffold(
            appBar: AppBar(title: const Text('Not found')),
            body: Center(child: Text('No route for ${settings.name}')),
          ),
        );
    }
  }
}
