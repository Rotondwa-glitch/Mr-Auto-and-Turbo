import 'package:flutter/material.dart';
import '../theme.dart';
import '../utils/helpers.dart';

class ProofOfPaymentView extends StatelessWidget {
  const ProofOfPaymentView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Proof of payment')),
      backgroundColor: kSurface,
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text('Upload a proof of payment for an outstanding quotation.'),
            const SizedBox(height: 16),
            ElevatedButton.icon(
              onPressed: () => snack(context, 'File picker (demo).'),
              icon: const Icon(Icons.upload_file),
              label: const Text('Choose file'),
            ),
          ],
        ),
      ),
    );
  }
}
