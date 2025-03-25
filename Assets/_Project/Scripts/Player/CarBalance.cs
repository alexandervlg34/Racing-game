using UnityEngine;

public class CarBalance
{
    [Range(20, 190)] public readonly int maxSpeed = 90;

    [Range(10, 120)] public readonly int maxReverseSpeed = 45;

    [Range(1, 10)] public readonly int accelerationMultiplier = 2;

    [Range(10, 45)] public readonly int maxSteeringAngle = 27;
    
    [Range(0.1f, 1f)] public readonly float steeringSpeed = 0.5f;

    [Range(100, 600)] public readonly int brakeForce = 350;

    [Range(1, 10)] public readonly int decelerationMultiplier = 2; 

    [Range(1, 10)] public readonly int handbrakeDriftMultiplier = 5; 

    [Space(10)] public readonly Vector3 bodyMassCenter; 
}
