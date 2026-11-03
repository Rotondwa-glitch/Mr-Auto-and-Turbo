import 'package:flutter/material.dart';
import '../theme.dart';
import '../utils/helpers.dart';

class BookServiceView extends StatelessWidget {
  const BookServiceView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Book service')),
      backgroundColor: kSurface,
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text('Choose a service type and preferred date.'),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => snack(context, 'Booking saved (demo).'),
              child: const Text('Submit booking'),
            ),
          ],
        ),
      ),
    );
  }
}
