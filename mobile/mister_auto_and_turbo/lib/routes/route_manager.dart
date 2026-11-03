import 'package:flutter/material.dart';
import '../views/splash_view.dart';
import '../views/login_view.dart';
import '../views/register_view.dart';
import '../views/dashboard_view.dart';
import '../views/book_service_view.dart';
import '../views/my_bookings_view.dart';
import '../views/quotation_view.dart';
import '../views/notifications_view.dart';
import '../views/complaints_view.dart';
import '../views/contact_view.dart';
import '../views/edit_profile_view.dart';
import '../views/register_vehicle_view.dart';
import '../views/vehicle_status_view.dart';
import '../views/proof_of_payment_view.dart';
import '../views/forgot_password_view.dart';
import '../views/reset_password_view.dart';
import '../views/history_view.dart';

class RouteManager {
  static const splash = '/';
  static const login = '/login';
  static const register = '/register';
  static const dashboard = '/dashboard';
  static const bookService = '/book-service';
  static const myBookings = '/my-bookings';
  static const quotations = '/quotations';
  static const notifications = '/notifications';
  static const complaints = '/complaints';
  static const contact = '/contact';
  static const editProfile = '/edit-profile';
  static const registerVehicle = '/register-vehicle';
  static const vehicleStatus = '/vehicle-status';
  static const proofOfPayment = '/proof-of-payment';
  static const forgotPassword = '/forgot-password';
  static const resetPassword = '/reset-password';
  static const history = '/history';

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
      case quotations:
        return MaterialPageRoute(builder: (_) => const QuotationView());
      case notifications:
        return MaterialPageRoute(builder: (_) => const NotificationsView());
      case complaints:
        return MaterialPageRoute(builder: (_) => const ComplaintsView());
      case contact:
        return MaterialPageRoute(builder: (_) => const ContactView());
      case editProfile:
        return MaterialPageRoute(builder: (_) => const EditProfileView());
      case registerVehicle:
        return MaterialPageRoute(builder: (_) => const RegisterVehicleView());
      case vehicleStatus:
        return MaterialPageRoute(builder: (_) => const VehicleStatusView());
      case proofOfPayment:
        return MaterialPageRoute(builder: (_) => const ProofOfPaymentView());
      case forgotPassword:
        return MaterialPageRoute(builder: (_) => const ForgotPasswordView());
      case resetPassword:
        return MaterialPageRoute(builder: (_) => const ResetPasswordView());
      case history:
        return MaterialPageRoute(builder: (_) => const HistoryView());
      default:
        return MaterialPageRoute(
          builder: (_) => Scaffold(
            body: Center(child: Text('No route: ${settings.name}')),
          ),
        );
    }
  }
}
