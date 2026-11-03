import 'package:flutter/material.dart';

class ContactView extends StatelessWidget {
  const ContactView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Contact workshop')),
      body: ListView(
        padding: const EdgeInsets.all(24),
        children: [
          Text(
            'Mister Auto & Turbo',
            style: Theme.of(context).textTheme.headlineSmall,
          ),
          const SizedBox(height: 8),
          Text(
            'Quality Repairs. Trusted Service.',
            style: Theme.of(context).textTheme.bodyMedium,
          ),
          const SizedBox(height: 24),
          const _ContactTile(
            icon: Icons.phone,
            label: 'Cell Workshop',
            value: '073 002 4599',
          ),
          const _ContactTile(
            icon: Icons.email_outlined,
            label: 'Email Workshop',
            value: 'tsotetsimotlokoa@yahoo.com',
          ),
          const _ContactTile(
            icon: Icons.location_on_outlined,
            label: 'Location',
            value: 'No. 5 128 Constantia Road\nDagbreek\nWelkom, 9459\nFree State Province',
          ),
          const SizedBox(height: 16),
          Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text('Opening Hours', style: Theme.of(context).textTheme.titleMedium),
                  const SizedBox(height: 8),
                  const Text('Mon – Fri: 09:00 – 17:00'),
                  const Text('Sat: 09:00 – 13:00'),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class _ContactTile extends StatelessWidget {
  final IconData icon;
  final String label;
  final String value;

  const _ContactTile({
    required this.icon,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.only(bottom: 12),
      child: ListTile(
        leading: Icon(icon),
        title: Text(label),
        subtitle: Text(value),
      ),
    );
  }
}
