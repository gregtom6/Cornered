using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected Transform headParent;
    [SerializeField] protected Transform bodyParent;
    [SerializeField] protected Transform armParent;
    [SerializeField] protected Transform legParent;
    [SerializeField] protected Animator bodyAnimator;
    [SerializeField] protected Animator armAnimator;
    [SerializeField] protected Animator legAnimator;
    [SerializeField] protected string isMovingAnimKey;
    [SerializeField] protected string isStrafingAnimKey;
    [SerializeField] protected string standingAnimKey;
    [SerializeField] protected string rotateAnimKey;
    [SerializeField] protected float walkingSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float headMaxRotX;
    [SerializeField] protected float headMinRotX;

    protected MovementState _movementState = MovementState.Standing;

    Action<float> actualForwardMovement;
    Action<float> actualBackwardMovement;
    Action<float> actualLeftMovement;
    Action<float> actualRightMovement;

    static Controller instance;

    protected float interactionStartTime;
    protected Quaternion originalLookRotation;

    public static MovementState GetMovementState => instance == null ? MovementState.Standing : instance._movementState;

    protected MovementState movementState
    {
        get
        {
            return _movementState;
        }
        set
        {
            if (_movementState == value)
                return;

            _movementState = value;
        }
    }

    protected Vector3 movement;
    protected float rotY;
    protected float rotX;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;

        actualForwardMovement = ForwardMovement;
        actualBackwardMovement = BackMovement;
        actualLeftMovement = LeftMovement;
        actualRightMovement = RightMovement;
    }

    protected virtual void Update()
    {
        Debug.Log("actual speed: " + CalculateActualSpeed());

        ManageLooking();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        ManageGeneralMovement();
    }

    protected void ManageGeneralMovement()
    {
        movement = Vector3.zero;

        float actualSpeed = CalculateActualSpeed();

        ManageWalkingRunning();

        ManageWalkingStafingStanding(actualSpeed);

        ActualPositionChange();
    }

    protected void ActualPositionChange()
    {
        movement.y = 0;

        if (headParent != null && rigidbody != null)
        {
            Vector3 direction = headParent.TransformDirection(movement);
            Vector3 newPosition = rigidbody.position + direction * Time.deltaTime * 100;
            rigidbody.MovePosition(newPosition);

        }
    }

    protected void ManageLooking()
    {
        rotX += Input.GetAxis("Mouse X");
        rotY += -Input.GetAxis("Mouse Y");

        if (rotY > headMaxRotX)
            rotY = headMaxRotX;
        if (rotY < headMinRotX)
            rotY = headMinRotX;

        if (headParent != null && legParent != null && armParent != null && bodyParent != null)
        {
            headParent.localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
            legParent.localRotation = Quaternion.Euler(0f, rotX, 0f);
            armParent.localRotation = Quaternion.Euler(0f, rotX, 0f);
            bodyParent.localRotation = Quaternion.Euler(0f, rotX, 0f);
        }
    }

    protected void ManageWalkingStafingStanding(float actualSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            actualForwardMovement?.Invoke(actualSpeed);

            movementState = MovementState.Moving;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            actualBackwardMovement?.Invoke(actualSpeed);

            movementState = MovementState.Moving;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actualLeftMovement?.Invoke(actualSpeed);

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                movementState = MovementState.Strafing;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            actualRightMovement?.Invoke(actualSpeed);

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                movementState = MovementState.Strafing;
        }

        if (movement == Vector3.zero)
            movementState = MovementState.Standing;
    }

    protected void LeftMovement(float actualSpeed)
    {
        movement.x = -(actualSpeed / 100f);
    }

    protected void RightMovement(float actualSpeed)
    {
        movement.x = actualSpeed / 100f;
    }

    protected void BackMovement(float actualSpeed)
    {
        movement.z = -(actualSpeed / 100f);
    }

    public void ForwardMovement(float actualSpeed)
    {
        movement.z = actualSpeed / 100f;
    }

    public float CalculateActualSpeed()
    {
        return walkingSpeed;
    }

    protected void ManageWalkingRunning()
    {
        if (legAnimator != null)
            legAnimator.speed = 1f;
    }

    protected void Rotate(bool isActive)
    {
        armAnimator.SetBool(rotateAnimKey, isActive);
    }

    public enum MovementState
    {
        Standing,
        Moving,
        Strafing,
    }
}