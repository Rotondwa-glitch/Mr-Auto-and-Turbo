class NotificationItem {
  final String id;
  final String title;
  final String body;
  final DateTime createdAt;
  final bool isRead;
  final String? type;
  final String? relatedId;

  const NotificationItem({
    required this.id,
    required this.title,
    required this.body,
    required this.createdAt,
    this.isRead = false,
    this.type,
    this.relatedId,
  });

  factory NotificationItem.fromMap(Map<String, dynamic> m) => NotificationItem(
        id: m['id'] as String? ?? '',
        title: m['title'] as String? ?? m['type'] as String? ?? 'Notice',
        body: m['message'] as String? ?? m['body'] as String? ?? '',
        createdAt: DateTime.tryParse(m['created_at']?.toString() ?? '') ?? DateTime.now(),
        isRead: m['is_read'] as bool? ?? false,
        type: m['type'] as String?,
        relatedId: m['related_id'] as String?,
      );
}
