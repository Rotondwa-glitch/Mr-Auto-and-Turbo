import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../routes/route_manager.dart';
import '../viewmodels/auth_viewmodel.dart';

class DashboardView extends StatelessWidget {
  const DashboardView({super.key});

  @override
  Widget build(BuildContext context) {
    final auth = context.watch<AuthViewModel>();
    return Scaffold(
      appBar: AppBar(
        title: const Text('Mister Auto & Turbo'),
        actions: [
          IconButton(
            icon: const Icon(Icons.notifications_outlined),
            onPressed: () => Navigator.of(context).pushNamed(RouteManager.notifications),
          ),
        ],
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          Text('Hello!', style: Theme.of(context).textTheme.headlineSmall),
          const SizedBox(height: 4),
          Text('What would you like to do today?', style: Theme.of(context).textTheme.bodyMedium),
          const SizedBox(height: 24),
          _DashboardCard(
            icon: Icons.build_circle_outlined,
            title: 'Book a service',
            subtitle: 'Schedule a diagnostic or repair',
            onTap: () => Navigator.of(context).pushNamed(RouteManager.bookService),
          ),
          _DashboardCard(
            icon: Icons.calendar_month_outlined,
            title: 'My bookings',
            subtitle: 'View upcoming appointments',
            onTap: () => Navigator.of(context).pushNamed(RouteManager.myBookings),
          ),
          _DashboardCard(
            icon: Icons.history,
            title: 'Service history',
            subtitle: 'Past jobs and repairs',
            onTap: () => Navigator.of(context).pushNamed(RouteManager.history),
          ),
          _DashboardCard(
            icon: Icons.request_quote_outlined,
            title: 'Quotations',
            subtitle: 'View and respond to quotes',
            onTap: () => Navigator.of(context).pushNamed(RouteManager.quotations),
          ),
          _DashboardCard(
            icon: Icons.report_problem_outlined,
            title: 'Complaints',
            subtitle: 'Raise or track a complaint',
            onTap: () => Navigator.of(context).pushNamed(RouteManager.complaints),
          ),
        ],
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            const DrawerHeader(child: Text('Menu')),
            ListTile(
              leading: const Icon(Icons.person_outline),
              title: const Text('Profile'),
              onTap: () => Navigator.of(context).pushNamed(RouteManager.profile),
            ),
            ListTile(
              leading: const Icon(Icons.logout),
              title: const Text('Sign out'),
              onTap: () async {
                await auth.logout();
                if (context.mounted) {
                  Navigator.of(context).pushReplacementNamed(RouteManager.login);
                }
              },
            ),
          ],
        ),
      ),
    );
  }
}

class _DashboardCard extends StatelessWidget {
  final IconData icon;
  final String title;
  final String subtitle;
  final VoidCallback onTap;

  const _DashboardCard({
    required this.icon,
    required this.title,
    required this.subtitle,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.only(bottom: 12),
      child: ListTile(
        leading: Icon(icon, size: 32),
        title: Text(title),
        subtitle: Text(subtitle),
        trailing: const Icon(Icons.chevron_right),
        onTap: onTap,
      ),
    );
  }
}
