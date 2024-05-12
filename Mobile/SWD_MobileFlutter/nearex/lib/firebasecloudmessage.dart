import 'package:firebase_messaging/firebase_messaging.dart';

class FirebaseMessagingService {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging.instance;

  Future<void> initialize() async {
    // Request permission for receiving notifications
    await _firebaseMessaging.requestPermission();

    // Get the token for this device
    String? token = await _firebaseMessaging.getToken();
    print('FCM Token: $token');

    // Configure how to handle incoming messages
    FirebaseMessaging.onMessage.listen((RemoteMessage message) {
      print('Received message: ${message.notification?.body}');
    });

    FirebaseMessaging.onMessageOpenedApp.listen((RemoteMessage message) {
      print('Opened app from message: ${message.notification?.body}');
    });
  }
}
