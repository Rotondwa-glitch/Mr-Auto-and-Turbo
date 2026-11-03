import 'package:flutter/material.dart';
import '../theme.dart';

class QuotationView extends StatelessWidget {
  const QuotationView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Quotations')),
      backgroundColor: kSurface,
      body: const Center(child: Text('No quotations yet.')),
    );
  }
}
