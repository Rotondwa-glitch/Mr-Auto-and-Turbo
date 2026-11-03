import 'package:flutter/material.dart';

class VehicleStatusView extends StatelessWidget {
  const VehicleStatusView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Vehicle status')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: const [
          Card(
            child: ListTile(
              leading: Icon(Icons.directions_car_outlined),
              title: Text('No active jobs'),
              subtitle: Text('When your vehicle is in the workshop, status updates will show here'),
            ),
          ),
        ],
      ),
    );
  }
}
