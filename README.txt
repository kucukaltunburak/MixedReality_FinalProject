Project Title: Mixed Reality Living Ecosystem & Physics-Based Interaction
Burak Kucukaltun 2019556047

1. Executive Summary

This project serves as a Mixed Reality simulation developed under the "Scenario 2: The Living Ecosystem" guidelines. The primary objective is to provide an interactive ecosystem where users can manipulate objects using hand gestures (Hand Tracking) subject to realistic physics (Gravity). Instead of standard marker-based methods, Google MediaPipe has been integrated for skeletal tracking, and a realistic "Grab & Drop" mechanism has been developed using Unity's physics engine.

2. Technologies & Methods

Game Engine: Unity 2022.x

Computer Vision: Google MediaPipe (Unity Plugin)

Scripting Language: C#

Interaction Model: Euclidean Distance Tracking (Index Finger & Thumb)

3. Coordinate Mapping & Calibration Logic

A critical technical aspect of this project is translating 2D camera data into the 3D Unity world space. A software-based "Screen-to-World" mapping algorithm was implemented to compensate for the lack of hardware projection setup.

Data Flow: The webcam feed is processed by MediaPipe to detect 21 distinct hand landmarks.

Transformation: Pixel coordinates (x, y) from the 2D screen plane are scaled to the Target Canvas coordinates within the Unity scene.

Z-Axis Handling: To translate 2D input into 3D interaction and prevent parallax errors, a fixed Z-depth is assigned. The HandCursor object follows the user's index finger coordinates (x, y) while remaining locked to the scene depth (z=0). This allows the user to manipulate 3D objects with depth perception using screen-surface gestures.

4. Interaction & Physics Algorithm

The project utilizes dynamic physics manipulation rather than simple collision triggers.

Pinch (Grab) Mechanism: The Euclidean Distance between the Index Finger Tip (ID 12) and the Thumb Tip (ID 4) is calculated every frame. A "GRAB" state is triggered when this distance falls below a defined threshold.

Visual Feedback (UI): Upon detecting a grab gesture, the virtual hand cursor dynamically switches from an "Open Hand" sprite to a "Closed Fist" sprite.

Physics Manipulation (Gravity Toggle):

On Grab: The target object's Rigidbody.isKinematic property is set to true. This disables external forces (gravity), allowing the object to lock onto the hand cursor.

On Release: When fingers are separated, isKinematic is set to false and UseGravity is enabled. The object is then returned to the Unity physics engine, allowing it to fall naturally to the ground.

5. Scene Design & Ecosystem

A dynamic environment was designed to enhance user immersion:

Dynamic Environment: A "Wind Zone" component was added to simulate wind effects on trees.

Audio Feedback: Each animal object is programmed to trigger a species-specific audio clip upon interaction (OnGrab).

Terrain Structure: A layered terrain structure reinforced with Box Colliders was created to support the physics of falling objects.

6. Conclusion

Despite hardware constraints (lack of projection device), this project demonstrates a fully functional Mixed Reality experience through software solutions (Raycasting and Physics Manipulation). The system successfully completes the loop of real-time hand tracking, physics-based interaction, and audio-visual feedback.