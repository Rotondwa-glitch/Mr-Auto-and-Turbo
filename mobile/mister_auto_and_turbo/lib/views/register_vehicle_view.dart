import 'package:flutter/material.dart';
import '../data/car_makes.dart';
import '../theme.dart';
import '../utils/helpers.dart';

class RegisterVehicleView extends StatefulWidget {
  const RegisterVehicleView({super.key});

  @override
  State<RegisterVehicleView> createState() => _RegisterVehicleViewState();
}

class _RegisterVehicleViewState extends State<RegisterVehicleView> {
  String? _make;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Register vehicle')),
      backgroundColor: kSurface,
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          DropdownButtonFormField<String>(
            value: _make,
            decoration: const InputDecoration(labelText: 'Make'),
            items: kCarMakes
                .map((m) => DropdownMenuItem(value: m, child: Text(m)))
                .toList(),
            onChanged: (v) => setState(() => _make = v),
          ),
          const SizedBox(height: 12),
          const TextField(decoration: InputDecoration(labelText: 'Model')),
          const SizedBox(height: 12),
          const TextField(decoration: InputDecoration(labelText: 'Registration')),
          const SizedBox(height: 12),
          TextField(
            decoration: const InputDecoration(labelText: 'Year'),
            keyboardType: TextInputType.number,
            inputFormatters: [digitsOnlyFormatter],
          ),
          const SizedBox(height: 24),
          ElevatedButton(
            onPressed: () => snack(context, 'Vehicle registered (demo).'),
            child: const Text('Save vehicle'),
          ),
        ],
      ),
    );
  }
}
