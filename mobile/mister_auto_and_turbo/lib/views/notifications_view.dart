import 'package:flutter/material.dart';
import '../theme.dart';

class NotificationsView extends StatelessWidget {
  const NotificationsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Notifications')),
      backgroundColor: kSurface,
      body: const Center(child: Text('No notifications.')),
    );
  }
}
