import 'package:flutter/material.dart';
import '../views/splash_view.dart';
import '../views/login_view.dart';
import '../views/register_view.dart';
import '../views/dashboard_view.dart';
import '../views/book_service_view.dart';
import '../views/my_bookings_view.dart';
import '../views/history_view.dart';
import '../views/complaints_view.dart';
import '../views/forgot_password_view.dart';
import '../views/reset_password_view.dart';
import '../views/notifications_view.dart';
import '../views/contact_view.dart';
import '../views/edit_profile_view.dart';
import '../views/quotation_view.dart';
import '../views/register_vehicle_view.dart';
import '../views/vehicle_status_view.dart';
import '../views/proof_of_payment_view.dart';

class RouteManager {
  static const String splash = '/';
  static const String login = '/login';
  static const String register = '/register';
  static const String forgotPassword = '/forgot-password';
  static const String resetPassword = '/reset-password';
  static const String dashboard = '/dashboard';
  static const String bookService = '/book-service';
  static const String myBookings = '/my-bookings';
  static const String history = '/history';
  static const String quotations = '/quotations';
  static const String notifications = '/notifications';
  static const String complaints = '/complaints';
  static const String profile = '/profile';
  static const String contact = '/contact';
  static const String registerVehicle = '/register-vehicle';
  static const String vehicleStatus = '/vehicle-status';
  static const String proofOfPayment = '/proof-of-payment';

  static Route<dynamic> generateRoute(RouteSettings settings) {
    switch (settings.name) {
      case splash:
        return MaterialPageRoute(builder: (_) => const SplashView());
      case login:
        return MaterialPageRoute(builder: (_) => const LoginView());
      case register:
        return MaterialPageRoute(builder: (_) => const RegisterView());
      case forgotPassword:
        return MaterialPageRoute(builder: (_) => const ForgotPasswordView());
      case resetPassword:
        return MaterialPageRoute(builder: (_) => const ResetPasswordView());
      case dashboard:
        return MaterialPageRoute(builder: (_) => const DashboardView());
      case bookService:
        return MaterialPageRoute(builder: (_) => const BookServiceView());
      case myBookings:
        return MaterialPageRoute(builder: (_) => const MyBookingsView());
      case history:
        return MaterialPageRoute(builder: (_) => const HistoryView());
      case quotations:
        return MaterialPageRoute(builder: (_) => const QuotationView());
      case notifications:
        return MaterialPageRoute(builder: (_) => const NotificationsView());
      case complaints:
        return MaterialPageRoute(builder: (_) => const ComplaintsView());
      case profile:
        return MaterialPageRoute(builder: (_) => const EditProfileView());
      case contact:
        return MaterialPageRoute(builder: (_) => const ContactView());
      case registerVehicle:
        return MaterialPageRoute(builder: (_) => const RegisterVehicleView());
      case vehicleStatus:
        return MaterialPageRoute(builder: (_) => const VehicleStatusView());
      case proofOfPayment:
        return MaterialPageRoute(builder: (_) => const ProofOfPaymentView());
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
