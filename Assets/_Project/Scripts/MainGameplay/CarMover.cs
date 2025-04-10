using System;
using UnityEngine;

public class CarMover : MonoBehaviour
{
  public event Action<float> OnSpeedChanged;
  
  [SerializeField] private ParticleEffects _effects;
  
  public GameObject frontLeftMesh;
  public WheelCollider frontLeftCollider;

  [Space(10)] 
  [SerializeField] private GameObject frontRightMesh;
  [SerializeField] private WheelCollider frontRightCollider;

  [Space(10)] 
  [SerializeField] private GameObject rearLeftMesh;
  [SerializeField] private WheelCollider rearLeftCollider;

  [Space(10)] 
  [SerializeField] private GameObject rearRightMesh;
  [SerializeField] private WheelCollider rearRightCollider;
  
  [Space(10)] 
  
  private CarBalance _balance;

  private float _speed;
  
  public Rigidbody carRigidbody; 
  public float steeringAxis; 
  public float throttleAxis;
  public float driftingAxis;
  public float localVelocityZ;
  public float localVelocityX;
  public bool deceleratingCar;
  
  [Space(10)] 
  
  private WheelFrictionCurve FLwheelFriction;
  private float FLWextremumSlip;
  
  private WheelFrictionCurve FRwheelFriction;
  private float FRWextremumSlip;
  
  private WheelFrictionCurve RLwheelFriction;
  private float RLWextremumSlip;
  
  private WheelFrictionCurve RRwheelFriction;
  private float RRWextremumSlip;


  public float Speed
  {
    get => _speed;
    set
    {
      _speed = value;
      OnSpeedChanged?.Invoke(Mathf.Abs(_speed));
    }
  }
  
  public bool isDrifting; 
  public bool isTractionLocked;
  
  private void Start()
  {
    _balance = new CarBalance();
    
    carRigidbody = gameObject.GetComponent<Rigidbody>();
    carRigidbody.centerOfMass = _balance.bodyMassCenter;
    
    FLwheelFriction = new WheelFrictionCurve();

    FLwheelFriction.extremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLWextremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLwheelFriction.extremumValue = frontLeftCollider.sidewaysFriction.extremumValue;
    FLwheelFriction.asymptoteSlip = frontLeftCollider.sidewaysFriction.asymptoteSlip;
    FLwheelFriction.asymptoteValue = frontLeftCollider.sidewaysFriction.asymptoteValue;
    FLwheelFriction.stiffness = frontLeftCollider.sidewaysFriction.stiffness;

    FRwheelFriction = new WheelFrictionCurve();

    FRwheelFriction.extremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRWextremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRwheelFriction.extremumValue = frontRightCollider.sidewaysFriction.extremumValue;
    FRwheelFriction.asymptoteSlip = frontRightCollider.sidewaysFriction.asymptoteSlip;
    FRwheelFriction.asymptoteValue = frontRightCollider.sidewaysFriction.asymptoteValue;
    FRwheelFriction.stiffness = frontRightCollider.sidewaysFriction.stiffness;

    RLwheelFriction = new WheelFrictionCurve();

    RLwheelFriction.extremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLWextremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLwheelFriction.extremumValue = rearLeftCollider.sidewaysFriction.extremumValue;
    RLwheelFriction.asymptoteSlip = rearLeftCollider.sidewaysFriction.asymptoteSlip;
    RLwheelFriction.asymptoteValue = rearLeftCollider.sidewaysFriction.asymptoteValue;
    RLwheelFriction.stiffness = rearLeftCollider.sidewaysFriction.stiffness;


    RRwheelFriction = new WheelFrictionCurve();

    RRwheelFriction.extremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRWextremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRwheelFriction.extremumValue = rearRightCollider.sidewaysFriction.extremumValue;
    RRwheelFriction.asymptoteSlip = rearRightCollider.sidewaysFriction.asymptoteSlip;
    RRwheelFriction.asymptoteValue = rearRightCollider.sidewaysFriction.asymptoteValue;
    RRwheelFriction.stiffness = rearRightCollider.sidewaysFriction.stiffness;
  }
  
  public void TurnLeft()
  {
    steeringAxis = steeringAxis - (Time.deltaTime * 10f * _balance.steeringSpeed);
    if (steeringAxis < -1f)
    {
      steeringAxis = -1f;
    }

    var steeringAngle = steeringAxis * _balance.maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
  }
  
  
  public void TurnRight()
  {
    steeringAxis = steeringAxis + (Time.deltaTime * 10f * _balance.steeringSpeed);
    if (steeringAxis > 1f)
    {
      steeringAxis = 1f;
    }

    var steeringAngle = steeringAxis * _balance.maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
  }
  
  public void ResetSteeringAngle()
  {
    if (steeringAxis < 0f)
    {
      steeringAxis = steeringAxis + (Time.deltaTime * 10f * _balance.steeringSpeed);
    }
    else if (steeringAxis > 0f)
    {
      steeringAxis = steeringAxis - (Time.deltaTime * 10f * _balance.steeringSpeed);
    }

    if (Mathf.Abs(frontLeftCollider.steerAngle) < 1f)
    {
      steeringAxis = 0f;
    }

    var steeringAngle = steeringAxis * _balance.maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, _balance.steeringSpeed);
  }
  
  public void AnimateWheelMeshes()
  {
    try
    {
      Quaternion FLWRotation;
      Vector3 FLWPosition;
      frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
      frontLeftMesh.transform.position = FLWPosition;
      frontLeftMesh.transform.rotation = FLWRotation;

      Quaternion FRWRotation;
      Vector3 FRWPosition;
      frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
      frontRightMesh.transform.position = FRWPosition;
      frontRightMesh.transform.rotation = FRWRotation;

      Quaternion RLWRotation;
      Vector3 RLWPosition;
      rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
      rearLeftMesh.transform.position = RLWPosition;
      rearLeftMesh.transform.rotation = RLWRotation;

      Quaternion RRWRotation;
      Vector3 RRWPosition;
      rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
      rearRightMesh.transform.position = RRWPosition;
      rearRightMesh.transform.rotation = RRWRotation;
    }
    catch (Exception ex)
    {
      Debug.LogWarning(ex);
    }
  }
  
  
  public void GoForward()
  {
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
      _effects.DriftCarPS();
    }
    else
    {
      isDrifting = false;
      _effects.DriftCarPS();
    }
    
    throttleAxis = throttleAxis + (Time.deltaTime * 3f);
    if (throttleAxis > 1f)
    {
      throttleAxis = 1f;
    }
    
    if (localVelocityZ < -1f)
    {
      Brakes();
    }
    else
    {
      if (Mathf.RoundToInt(Speed) < _balance.maxSpeed)
      {
        frontLeftCollider.brakeTorque = 0;
        frontLeftCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        frontRightCollider.brakeTorque = 0;
        frontRightCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        rearLeftCollider.brakeTorque = 0;
        rearLeftCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        rearRightCollider.brakeTorque = 0;
        rearRightCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
      }
      else
      {
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;
      }
    }
  }

  
  public void GoReverse()
  {
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
      _effects.DriftCarPS();
    }
    else
    {
      isDrifting = false;
      _effects.DriftCarPS();
    }
    
    throttleAxis = throttleAxis - (Time.deltaTime * 3f);
    if (throttleAxis < -1f)
    {
      throttleAxis = -1f;
    }
    
    if (localVelocityZ > 1f)
    {
      Brakes();
    }
    else
    {
      if (Mathf.Abs(Mathf.RoundToInt(Speed)) < _balance.maxReverseSpeed)
      {
        frontLeftCollider.brakeTorque = 0;
        frontLeftCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        frontRightCollider.brakeTorque = 0;
        frontRightCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        rearLeftCollider.brakeTorque = 0;
        rearLeftCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
        rearRightCollider.brakeTorque = 0;
        rearRightCollider.motorTorque = (_balance.accelerationMultiplier * 50f) * throttleAxis;
      }
      else
      {
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;
      }
    }
  }
  
  public void ThrottleOff()
  {
    frontLeftCollider.motorTorque = 0;
    frontRightCollider.motorTorque = 0;
    rearLeftCollider.motorTorque = 0;
    rearRightCollider.motorTorque = 0;
  }
  
  public void ApplyDeceleration()
  {
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
      _effects.DriftCarPS();
    }
    else
    {
      isDrifting = false;
      _effects.DriftCarPS();
    }
    
    if (throttleAxis != 0f)
    {
      if (throttleAxis > 0f)
      {
        throttleAxis = throttleAxis - (Time.deltaTime * 10f);
      }
      else if (throttleAxis < 0f)
      {
        throttleAxis = throttleAxis + (Time.deltaTime * 10f);
      }

      if (Mathf.Abs(throttleAxis) < 0.15f)
      {
        throttleAxis = 0f;
      }
    }

    carRigidbody.velocity = carRigidbody.velocity * (1f / (1f + (0.025f * _balance.decelerationMultiplier)));
    
    frontLeftCollider.motorTorque = 0;
    frontRightCollider.motorTorque = 0;
    rearLeftCollider.motorTorque = 0;
    rearRightCollider.motorTorque = 0;
   
    if (carRigidbody.velocity.magnitude < 0.25f)
    {
      carRigidbody.velocity = Vector3.zero;
      //CancelInvoke("DecelerateCar");
    }
  }
  
  public void DecelerateCar()
  {
    ApplyDeceleration();
  }
  
  public void Brakes()
  {
    frontLeftCollider.brakeTorque = _balance.brakeForce;
    frontRightCollider.brakeTorque = _balance.brakeForce;
    rearLeftCollider.brakeTorque = _balance.brakeForce;
    rearRightCollider.brakeTorque = _balance.brakeForce;
  }
  
  public void Handbrake()
  {
    CancelInvoke("RecoverTraction");
    
    driftingAxis = driftingAxis + (Time.deltaTime);
    float secureStartingPoint = driftingAxis * FLWextremumSlip * _balance.handbrakeDriftMultiplier;

    if (secureStartingPoint < FLWextremumSlip)
    {
      driftingAxis = FLWextremumSlip / (FLWextremumSlip * _balance.handbrakeDriftMultiplier);
    }

    if (driftingAxis > 1f)
    {
      driftingAxis = 1f;
    }
    
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }

    
    if (driftingAxis < 1f)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * _balance.
        handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;
    }

    
    isTractionLocked = true;
    _effects.DriftCarPS();
  }
  
  public void RecoverTraction()
  {
    isTractionLocked = false;
    driftingAxis = driftingAxis - (Time.deltaTime / 1.5f);
    if (driftingAxis < 0f)
    {
      driftingAxis = 0f;
    }

    
    if (FLwheelFriction.extremumSlip > FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * _balance.handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      Invoke("RecoverTraction", Time.deltaTime);
    }
    else if (FLwheelFriction.extremumSlip < FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      driftingAxis = 0f;
    }
  }
}
