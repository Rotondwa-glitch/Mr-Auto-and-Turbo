import 'package:flutter/material.dart';
import '../theme.dart';

class MyBookingsView extends StatelessWidget {
  const MyBookingsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('My bookings')),
      backgroundColor: kSurface,
      body: const Center(child: Text('No bookings yet.')),
    );
  }
}
