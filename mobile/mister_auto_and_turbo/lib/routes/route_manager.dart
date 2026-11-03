import 'package:flutter/material.dart';

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
    // Placeholder – full routing will wire to the actual views
    return MaterialPageRoute(
      builder: (_) => Scaffold(
        appBar: AppBar(title: Text(settings.name ?? 'Route')),
        body: Center(child: Text('Route: ${settings.name}')),
      ),
    );
  }
}
