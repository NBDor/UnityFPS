using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 4f;
    public float movementSpeedBase = 10f;
    public float movementSpeedSprint = 20f;
    public float movementSpeedCrouch = 5f;

    public SprintBar sprintBar;
    private bool SprintRecoveryFlag;
    private bool allowedToSprint = true;
    public int MaxStamina = 1200;
    private int currentStamina;

    private Rigidbody rigidBody;
    private Vector3 movementDirection;

    private float checkRadius = 0.75f;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;

    private float yScaleCrouch;
    private float yScaleBase;

    private float realSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        realSpeed = movementSpeedBase;
        yScaleBase = this.transform.localScale.y;
        yScaleCrouch = yScaleBase / 4;

        currentStamina = MaxStamina;
        sprintBar.SetMaxValue(MaxStamina);

        StartCoroutine(GiveCameraControl());
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
        Sprint();    
        MoveInputs();
        Jump();
    }

    // Used on rigidbody when movement vector is updated to appear smoother
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidBody.MovePosition(transform.position + movementDirection.normalized * realSpeed * Time.deltaTime);

    }
    void MoveInputs()
    {
        // read movement inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movementDirection = transform.right * x + transform.forward * z; 
    }

    void Jump()
    {
        bool isGrounded = Physics.OverlapSphere(groundChecker.position,
                                                checkRadius,
                                                groundLayer).Length > 0;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioManager.Instance.Play("Jump");
        }
    }

    void Crouch()
    {
        Vector3 newScale = this.transform.localScale;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            realSpeed = movementSpeedCrouch;
            newScale.y = yScaleCrouch;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            realSpeed = movementSpeedSprint;
            newScale.y = yScaleBase;
        }
        this.transform.localScale = newScale;

    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && allowedToSprint)
        {
            realSpeed = movementSpeedSprint;

            currentStamina -= 5;
            sprintBar.SetValue(currentStamina);

            if (currentStamina <= 0)
            {
                allowedToSprint = false;
                SprintRecoveryFlag = true;
                realSpeed = movementSpeedBase;
            }

            SprintRecoveryFlag = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprintRecoveryFlag = true;
            realSpeed = movementSpeedBase;
          
        }

        if (SprintRecoveryFlag && currentStamina < MaxStamina)
        {
            currentStamina += 2;
            sprintBar.SetValue(currentStamina);
            if (!allowedToSprint)
            {
                allowedToSprint = currentStamina >= MaxStamina;
            }
        }
    }

    IEnumerator GiveCameraControl()
    {
        // IEnumerator is used to fix bug when camera is snapped upon game laucnh
        yield return new WaitForSeconds(0.2f);
        gameObject.AddComponent<PlayerCameraController>();
    }

}