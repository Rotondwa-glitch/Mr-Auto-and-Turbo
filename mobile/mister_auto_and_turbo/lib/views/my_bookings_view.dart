import 'package:flutter/material.dart';

class MyBookingsView extends StatelessWidget {
  const MyBookingsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('My bookings')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: const [
          Card(
            child: ListTile(
              leading: Icon(Icons.calendar_today),
              title: Text('No upcoming bookings'),
              subtitle: Text('Book a service to get started'),
            ),
          ),
        ],
      ),
    );
  }
}
