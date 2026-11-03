import 'package:flutter/material.dart';

class NotificationsView extends StatelessWidget {
  const NotificationsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Notifications')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: const [
          Card(
            child: ListTile(
              leading: Icon(Icons.notifications_none),
              title: Text('No notifications yet'),
              subtitle: Text('Updates about your bookings and jobs will appear here'),
            ),
          ),
        ],
      ),
    );
  }
}
