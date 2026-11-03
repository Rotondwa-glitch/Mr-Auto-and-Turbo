import 'package:flutter/material.dart';

class ProofOfPaymentView extends StatefulWidget {
  const ProofOfPaymentView({super.key});

  @override
  State<ProofOfPaymentView> createState() => _ProofOfPaymentViewState();
}

class _ProofOfPaymentViewState extends State<ProofOfPaymentView> {
  final _refCtrl = TextEditingController();
  final _amountCtrl = TextEditingController();
  bool _loading = false;

  @override
  void dispose() {
    _refCtrl.dispose();
    _amountCtrl.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (_refCtrl.text.trim().isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Enter payment reference')),
      );
      return;
    }
    setState(() => _loading = true);
    await Future.delayed(const Duration(milliseconds: 500));
    if (!mounted) return;
    setState(() => _loading = false);
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(content: Text('Proof of payment submitted')),
    );
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Proof of payment')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'Upload or enter your payment details so the workshop can confirm receipt.',
            ),
            const SizedBox(height: 24),
            TextFormField(
              controller: _refCtrl,
              decoration: const InputDecoration(
                labelText: 'Payment reference',
                prefixIcon: Icon(Icons.receipt_long_outlined),
              ),
            ),
            const SizedBox(height: 16),
            TextFormField(
              controller: _amountCtrl,
              decoration: const InputDecoration(
                labelText: 'Amount (R)',
                prefixIcon: Icon(Icons.payments_outlined),
              ),
              keyboardType: const TextInputType.numberWithOptions(decimal: true),
            ),
            const SizedBox(height: 16),
            OutlinedButton.icon(
              onPressed: () {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(content: Text('File picker will be wired with file_picker package')),
                );
              },
              icon: const Icon(Icons.attach_file),
              label: const Text('Attach proof (optional)'),
            ),
            const SizedBox(height: 24),
            ElevatedButton(
              onPressed: _loading ? null : _submit,
              child: _loading
                  ? const SizedBox(height: 20, width: 20, child: CircularProgressIndicator(strokeWidth: 2))
                  : const Text('Submit'),
            ),
          ],
        ),
      ),
    );
  }
}
