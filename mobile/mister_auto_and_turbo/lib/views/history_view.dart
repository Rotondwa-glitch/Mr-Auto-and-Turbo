import 'package:flutter/material.dart';
import '../theme.dart';

class HistoryView extends StatelessWidget {
  const HistoryView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Repair history')),
      backgroundColor: kSurface,
      body: const Center(child: Text('No completed jobs yet.')),
    );
  }
}
