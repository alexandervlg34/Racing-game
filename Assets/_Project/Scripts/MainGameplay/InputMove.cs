using System;
using UnityEngine;

public class InputMove : MonoBehaviour
{
    [SerializeField] private CarMover _mover;

    [SerializeField] private bool useTouchControls = false;

    [SerializeField] private GameObject throttleButton;
    private TouchInput throttlePTI;

    [SerializeField] private GameObject reverseButton;
    private TouchInput reversePTI;

    [SerializeField] private GameObject turnRightButton;
    private TouchInput turnRightPTI;

    [SerializeField] private GameObject turnLeftButton;
    private TouchInput turnLeftPTI;

    [SerializeField] private GameObject handbrakeButton;
    private TouchInput handbrakePTI;

    private bool touchControlsSetup = false;

    private void Start()
    {
        if (useTouchControls)
        {
            if (throttleButton != null && reverseButton != null &&
                turnRightButton != null && turnLeftButton != null
                && handbrakeButton != null)

            {
                throttlePTI = throttleButton.GetComponent<TouchInput>();
                reversePTI = reverseButton.GetComponent<TouchInput>();
                turnLeftPTI = turnLeftButton.GetComponent<TouchInput>();
                turnRightPTI = turnRightButton.GetComponent<TouchInput>();
                handbrakePTI = handbrakeButton.GetComponent<TouchInput>();
                touchControlsSetup = true;
            }
            else
            {
                String ex =
                    "Touch controls are not completely set up. You must drag and drop your scene buttons in the" +
                    " PrometeoCarController component.";
                Debug.LogWarning(ex);
            }
        }
    }

    void Update()
    {
        _mover.Speed = (2 * Mathf.PI * _mover.frontLeftCollider.radius * _mover.frontLeftCollider.rpm * 60) / 1000;
        _mover.localVelocityX = transform.InverseTransformDirection(_mover.carRigidbody.velocity).x;
        _mover.localVelocityZ = transform.InverseTransformDirection(_mover.carRigidbody.velocity).z;

        if (useTouchControls && touchControlsSetup)
        {
            if (throttlePTI.buttonPressed)
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.GoForward();
            }

            if (reversePTI.buttonPressed)
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.GoReverse();
            }

            if (turnLeftPTI.buttonPressed)
            {
                _mover.TurnLeft();
            }

            if (turnRightPTI.buttonPressed)
            {
                _mover.TurnRight();
            }

            if (handbrakePTI.buttonPressed)
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.Handbrake();
            }

            if (!handbrakePTI.buttonPressed)
            {
                _mover.RecoverTraction();
            }

            if ((!throttlePTI.buttonPressed && !reversePTI.buttonPressed))
            {
                _mover.ThrottleOff();
            }

            if ((!reversePTI.buttonPressed && !throttlePTI.buttonPressed) && !handbrakePTI.buttonPressed &&
                !_mover.deceleratingCar)
            {
                InvokeRepeating("Decelerate", 0f, 0.1f);
                _mover.deceleratingCar = true;
            }

            if (!turnLeftPTI.buttonPressed && !turnRightPTI.buttonPressed && _mover.steeringAxis != 0f)
            {
                _mover.ResetSteeringAngle();
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.GoForward();
            }

            if (Input.GetKey(KeyCode.S))
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.GoReverse();
            }

            if (Input.GetKey(KeyCode.A))
            {
                _mover.TurnLeft();
            }

            if (Input.GetKey(KeyCode.D))
            {
                _mover.TurnRight();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                CancelInvoke("Decelerate");
                _mover.deceleratingCar = false;
                _mover.Handbrake();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _mover.RecoverTraction();
            }

            if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))
            {
                _mover.ThrottleOff();
            }

            if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) &&
                !_mover.deceleratingCar)
            {
                InvokeRepeating("Decelerate", 0f, 0.1f);
                _mover.deceleratingCar = true;
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && _mover.steeringAxis != 0f)
            {
                _mover.ResetSteeringAngle();
            }
        }

        _mover.AnimateWheelMeshes();
    }

    private void Decelerate()
    {
        _mover.ApplyDeceleration();

        if (_mover.carRigidbody.velocity.magnitude < 0.25f)
        {
            CancelInvoke("Decelerate");
            _mover.deceleratingCar = false;
        }
    }
}