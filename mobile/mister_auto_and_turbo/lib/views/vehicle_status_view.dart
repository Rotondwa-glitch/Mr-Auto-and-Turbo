import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../theme.dart';
import '../viewmodels/workshop_viewmodel.dart';

class VehicleStatusView extends StatelessWidget {
  const VehicleStatusView({super.key});

  @override
  Widget build(BuildContext context) {
    final jobs = context.watch<WorkshopViewModel>().jobs;
    return Scaffold(
      appBar: AppBar(title: const Text('Vehicle status')),
      backgroundColor: kSurface,
      body: jobs.isEmpty
          ? const Center(child: Text('No active jobs.'))
          : ListView(
              padding: const EdgeInsets.all(16),
              children: jobs
                  .map(
                    (j) => Card(
                      child: ListTile(
                        title: Text(j.title),
                        subtitle: Text('${j.vehicle.displayName} · ${j.summaryLabel}'),
                        trailing: Text('${(j.progress * 100).round()}%'),
                      ),
                    ),
                  )
                  .toList(),
            ),
    );
  }
}
