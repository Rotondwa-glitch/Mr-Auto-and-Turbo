import '../models/customer.dart';
import '../models/vehicle.dart';
import '../models/booking.dart';
import '../models/quotation.dart';
import '../services/supabase_service.dart';

/// Data access for customer-facing Supabase tables.
/// Shared with the ASP.NET staff app via the same PostgreSQL database.
class CustomerRepository {
  CustomerRepository({SupabaseService? service});

  Future<Customer?> getCustomerByUserId(String userId) async {
    try {
      final res = await SupabaseService.client
          .from('customers')
          .select()
          .eq('user_id', userId)
          .maybeSingle();
      if (res == null) return null;
      return Customer.fromJson(Map<String, dynamic>.from(res));
    } catch (_) {
      return null;
    }
  }

  Future<List<Vehicle>> getVehicles(String customerId) async {
    try {
      final res = await SupabaseService.client
          .from('vehicles')
          .select()
          .eq('customer_id', customerId);
      return (res as List)
          .map((e) => Vehicle.fromJson(Map<String, dynamic>.from(e)))
          .toList();
    } catch (_) {
      return [];
    }
  }

  Future<List<Booking>> getBookings(String customerId) async {
    try {
      final res = await SupabaseService.client
          .from('appointments')
          .select()
          .eq('customer_id', customerId)
          .order('scheduled_at', ascending: false);
      return (res as List)
          .map((e) => Booking.fromJson(Map<String, dynamic>.from(e)))
          .toList();
    } catch (_) {
      return [];
    }
  }

  Future<List<Quotation>> getQuotations(String customerId) async {
    try {
      final res = await SupabaseService.client
          .from('quotations')
          .select()
          .eq('customer_id', customerId)
          .order('created_at', ascending: false);
      return (res as List)
          .map((e) => Quotation.fromJson(Map<String, dynamic>.from(e)))
          .toList();
    } catch (_) {
      return [];
    }
  }

  Future<String?> createBooking({
    required String customerId,
    required String vehicleId,
    required DateTime scheduledAt,
    required String serviceType,
    required String description,
  }) async {
    try {
      await SupabaseService.client.from('appointments').insert({
        'customer_id': customerId,
        'vehicle_id': vehicleId,
        'scheduled_at': scheduledAt.toIso8601String(),
        'service_type': serviceType,
        'description': description,
        'status': 'Pending',
      });
      return null;
    } catch (e) {
      return e.toString();
    }
  }

  Future<String?> submitComplaint({
    required String customerId,
    required String subject,
    required String description,
    String? jobCardId,
  }) async {
    try {
      await SupabaseService.client.from('complaints').insert({
        'customer_id': customerId,
        'subject': subject,
        'description': description,
        if (jobCardId != null) 'job_card_id': jobCardId,
        'status': 'Open',
      });
      return null;
    } catch (e) {
      return e.toString();
    }
  }

  Future<String?> registerVehicle({
    required String customerId,
    required String registrationNumber,
    required String make,
    required String model,
    required int year,
    int mileage = 0,
  }) async {
    try {
      await SupabaseService.client.from('vehicles').insert({
        'customer_id': customerId,
        'registration_number': registrationNumber,
        'make': make,
        'model': model,
        'year': year,
        'mileage': mileage,
      });
      return null;
    } catch (e) {
      return e.toString();
    }
  }
}
