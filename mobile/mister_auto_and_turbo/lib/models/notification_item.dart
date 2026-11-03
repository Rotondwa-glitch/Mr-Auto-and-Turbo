import '../utils/helpers.dart';

/// Single notification entry shown on the Notifications screen.
class NotificationItem {
  final String title;
  final String body;
  final DateTime? at;
  final bool highlighted;

  const NotificationItem(this.title, this.body, {this.at, this.highlighted = false});

  bool get isQuotation => '$title $body'.toLowerCase().contains('quot');

  String? get when => at == null ? null : formatDate(at!);
}
