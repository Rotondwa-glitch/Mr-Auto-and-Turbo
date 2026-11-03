import 'package:flutter/material.dart';
import '../theme.dart';
import '../utils/helpers.dart';

class EditProfileView extends StatelessWidget {
  const EditProfileView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Edit profile')),
      backgroundColor: kSurface,
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            const TextField(decoration: InputDecoration(labelText: 'Full name')),
            const SizedBox(height: 12),
            const TextField(decoration: InputDecoration(labelText: 'Phone')),
            const SizedBox(height: 24),
            ElevatedButton(
              onPressed: () => snack(context, 'Profile saved (demo).'),
              child: const Text('Save'),
            ),
          ],
        ),
      ),
    );
  }
}
