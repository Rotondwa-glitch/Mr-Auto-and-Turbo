import 'package:flutter/material.dart';
import '../theme.dart';
import '../utils/helpers.dart';

class ComplaintsView extends StatelessWidget {
  const ComplaintsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Complaints')),
      backgroundColor: kSurface,
      floatingActionButton: FloatingActionButton(
        onPressed: () => snack(context, 'Complaint form (demo).'),
        child: const Icon(Icons.add),
      ),
      body: const Center(child: Text('No complaints submitted.')),
    );
  }
}
