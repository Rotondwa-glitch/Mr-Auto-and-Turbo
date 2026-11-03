import 'package:flutter/material.dart';
import '../data/car_makes.dart';

class RegisterVehicleView extends StatefulWidget {
  const RegisterVehicleView({super.key});

  @override
  State<RegisterVehicleView> createState() => _RegisterVehicleViewState();
}

class _RegisterVehicleViewState extends State<RegisterVehicleView> {
  final _formKey = GlobalKey<FormState>();
  final _regCtrl = TextEditingController();
  final _modelCtrl = TextEditingController();
  final _yearCtrl = TextEditingController();
  final _mileageCtrl = TextEditingController();
  String? _make;
  bool _loading = false;

  @override
  void dispose() {
    _regCtrl.dispose();
    _modelCtrl.dispose();
    _yearCtrl.dispose();
    _mileageCtrl.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;
    if (_make == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Select a make')),
      );
      return;
    }
    setState(() => _loading = true);
    await Future.delayed(const Duration(milliseconds: 500));
    if (!mounted) return;
    setState(() => _loading = false);
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(content: Text('Vehicle registered')),
    );
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Register vehicle')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              TextFormField(
                controller: _regCtrl,
                decoration: const InputDecoration(labelText: 'Registration number'),
                textCapitalization: TextCapitalization.characters,
                validator: (v) => (v == null || v.isEmpty) ? 'Required' : null,
              ),
              const SizedBox(height: 16),
              DropdownButtonFormField<String>(
                value: _make,
                decoration: const InputDecoration(labelText: 'Make'),
                items: carMakes
                    .map((m) => DropdownMenuItem(value: m, child: Text(m)))
                    .toList(),
                onChanged: (v) => setState(() => _make = v),
              ),
              const SizedBox(height: 16),
              TextFormField(
                controller: _modelCtrl,
                decoration: const InputDecoration(labelText: 'Model'),
                validator: (v) => (v == null || v.isEmpty) ? 'Required' : null,
              ),
              const SizedBox(height: 16),
              TextFormField(
                controller: _yearCtrl,
                decoration: const InputDecoration(labelText: 'Year'),
                keyboardType: TextInputType.number,
                validator: (v) => (v == null || v.isEmpty) ? 'Required' : null,
              ),
              const SizedBox(height: 16),
              TextFormField(
                controller: _mileageCtrl,
                decoration: const InputDecoration(labelText: 'Mileage (km)'),
                keyboardType: TextInputType.number,
              ),
              const SizedBox(height: 24),
              ElevatedButton(
                onPressed: _loading ? null : _submit,
                child: _loading
                    ? const SizedBox(height: 20, width: 20, child: CircularProgressIndicator(strokeWidth: 2))
                    : const Text('Register vehicle'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
