import 'package:flutter/material.dart';
import '../theme.dart';

class ContactView extends StatelessWidget {
  const ContactView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Contact workshop')),
      backgroundColor: kSurface,
      body: const Padding(
        padding: EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Mister Auto & Turbo', style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
            SizedBox(height: 8),
            Text('Welkom, Free State'),
            Text('Phone: 057 000 0000'),
            Text('Email: info@misterauto.local'),
          ],
        ),
      ),
    );
  }
}
