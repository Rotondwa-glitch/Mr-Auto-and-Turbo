import 'vehicle.dart';

enum JobStage {
  inspection,
  quotation,
  inProgress,
  qualityCheck,
  readyForCollection,
  collected,
}

extension JobStageLabel on JobStage {
  String get label => switch (this) {
        JobStage.inspection => 'Inspection',
        JobStage.quotation => 'Quotation',
        JobStage.inProgress => 'In Progress',
        JobStage.qualityCheck => 'Quality Check',
        JobStage.readyForCollection => 'Ready for Collection',
        JobStage.collected => 'Collected',
      };
}

/// Maps to JOB_CARD (same Supabase table the ASP.NET staff app writes to).
class RepairJob {
  final String id;
  final Vehicle vehicle;
  final String title;
  final JobStage stage;
  final bool collected;
  final String diagnosis;
  final String repairNotes;

  const RepairJob({
    required this.id,
    required this.vehicle,
    required this.title,
    required this.stage,
    this.collected = false,
    this.diagnosis = '',
    this.repairNotes = '',
  });

  bool get isComplete =>
      collected || stage == JobStage.collected || stage == JobStage.readyForCollection;

  bool get hasDiagnosis => diagnosis.trim().isNotEmpty;
  bool get hasRepairNotes => repairNotes.trim().isNotEmpty;

  String get summaryLabel {
    if (collected || stage == JobStage.collected) return 'Collected';
    return stage.label;
  }

  bool stageDone(JobStage s) {
    if (collected || stage == JobStage.collected) return true;
    return s.index <= stage.index;
  }
}
