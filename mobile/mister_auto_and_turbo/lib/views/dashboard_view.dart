import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../routes/route_manager.dart';
import '../theme.dart';
import '../viewmodels/auth_viewmodel.dart';
import '../viewmodels/workshop_viewmodel.dart';

class DashboardView extends StatelessWidget {
  const DashboardView({super.key});

  @override
  Widget build(BuildContext context) {
    final workshop = context.watch<WorkshopViewModel>();
    workshop.seedDemoJob();

    return Scaffold(
      backgroundColor: kSurface,
      appBar: AppBar(
        title: const Text('Dashboard'),
        actions: [
          IconButton(
            icon: const Icon(Icons.notifications_outlined),
            onPressed: () =>
                Navigator.of(context).pushNamed(RouteManager.notifications),
          ),
        ],
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            const DrawerHeader(
              decoration: BoxDecoration(color: kNavy),
              child: Text(
                'Mister Auto & Turbo',
                style: TextStyle(color: Colors.white, fontSize: 18),
              ),
            ),
            _item(context, 'Book service', RouteManager.bookService),
            _item(context, 'My bookings', RouteManager.myBookings),
            _item(context, 'Quotations', RouteManager.quotations),
            _item(context, 'Vehicle status', RouteManager.vehicleStatus),
            _item(context, 'Proof of payment', RouteManager.proofOfPayment),
            _item(context, 'Complaints', RouteManager.complaints),
            _item(context, 'History', RouteManager.history),
            _item(context, 'Register vehicle', RouteManager.registerVehicle),
            _item(context, 'Edit profile', RouteManager.editProfile),
            _item(context, 'Contact', RouteManager.contact),
            ListTile(
              leading: const Icon(Icons.logout),
              title: const Text('Sign out'),
              onTap: () async {
                await context.read<AuthViewModel>().logout();
                if (context.mounted) {
                  Navigator.of(context)
                      .pushReplacementNamed(RouteManager.login);
                }
              },
            ),
          ],
        ),
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          const Text(
            'Your vehicles',
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 12),
          ...workshop.vehicles.map(
            (v) => Card(
              child: ListTile(
                leading: const Icon(Icons.directions_car, color: kPrimary),
                title: Text(v.displayName),
                subtitle: Text('${v.plate} · ${v.year}'),
                trailing: const Icon(Icons.chevron_right),
                onTap: () => Navigator.of(context)
                    .pushNamed(RouteManager.vehicleStatus),
              ),
            ),
          ),
          const SizedBox(height: 16),
          SizedBox(
            width: double.infinity,
            child: ElevatedButton.icon(
              onPressed: () =>
                  Navigator.of(context).pushNamed(RouteManager.bookService),
              icon: const Icon(Icons.add),
              label: const Text('Book a service'),
            ),
          ),
        ],
      ),
    );
  }

  static Widget _item(BuildContext context, String title, String route) {
    return ListTile(
      title: Text(title),
      onTap: () {
        Navigator.pop(context);
        Navigator.of(context).pushNamed(route);
      },
    );
  }
}
