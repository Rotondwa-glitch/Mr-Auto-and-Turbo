import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

DateTime? parseDate(dynamic value) {
  if (value == null) return null;
  if (value is DateTime) return value.toLocal();
  return DateTime.tryParse(value.toString())?.toLocal();
}

String fmtDateTime(DateTime d) {
  const months = [
    'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec',
  ];
  String two(int n) => n.toString().padLeft(2, '0');
  return '${d.day} ${months[d.month - 1]} ${d.year}, ${two(d.hour)}:${two(d.minute)}';
}

void snack(BuildContext c, String msg) =>
    ScaffoldMessenger.of(c).showSnackBar(SnackBar(content: Text(msg)));

String? requiredField(String? v) =>
    (v == null || v.trim().isEmpty) ? 'Required' : null;

String? emailField(String? v) =>
    (v == null || !v.contains('@')) ? 'Enter a valid email' : null;

String? phoneField(String? v) {
  if (v == null || v.trim().isEmpty) return 'Required';
  final trimmed = v.trim();
  if (RegExp(r'[^0-9]').hasMatch(trimmed)) {
    return 'Numbers only — no letters or special characters';
  }
  if (trimmed.length < 10) return 'Enter a valid phone number (at least 10 digits)';
  if (trimmed.length > 15) return 'Phone number is too long';
  return null;
}

String? digitsField(String? v, {int? min, int? max, String label = 'Value'}) {
  if (v == null || v.trim().isEmpty) return 'Required';
  if (RegExp(r'[^0-9]').hasMatch(v.trim())) {
    return 'Numbers only — no letters or special characters';
  }
  final n = int.tryParse(v.trim());
  if (n == null) return 'Enter a valid number';
  if (min != null && n < min) return '$label must be at least $min';
  if (max != null && n > max) return '$label must be at most $max';
  return null;
}

final digitsOnlyFormatter = FilteringTextInputFormatter.digitsOnly;

const _months = [
  'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
  'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec',
];

String fmtDate(DateTime date) =>
    '${date.day} ${_months[date.month - 1]} ${date.year}';

String fmtSize(int bytes) {
  if (bytes < 1024) return '$bytes B';
  if (bytes < 1024 * 1024) return '${(bytes / 1024).toStringAsFixed(0)} KB';
  return '${(bytes / 1024 / 1024).toStringAsFixed(2)} MB';
}
