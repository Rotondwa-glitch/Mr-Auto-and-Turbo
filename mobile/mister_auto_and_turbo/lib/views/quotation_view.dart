import 'package:flutter/material.dart';
import '../utils/helpers.dart';

class QuotationView extends StatelessWidget {
  const QuotationView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Quotations')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          Card(
            child: ListTile(
              leading: const Icon(Icons.request_quote_outlined),
              title: const Text('No quotations yet'),
              subtitle: const Text('Quotes from the workshop will appear here'),
              trailing: Text(formatCurrency(0)),
            ),
          ),
        ],
      ),
    );
  }
}
