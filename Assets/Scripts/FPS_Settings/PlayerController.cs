using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AiAgent supporter = null;

    [SerializeField] public Transform playerCamera = null;

    [Range(.1f, 10f)]
    [SerializeField] public float mouseSensitivity = 3.5f;
    [SerializeField] public float walkSpeed = 6.0f;
    [SerializeField] public float gravity = -13.0f;
    [SerializeField] public float jumpForce = 1.20f;
    [SerializeField] public Transform groundChecker;
    [SerializeField] public LayerMask groundMask;
    [Range(.1f, 2f)]
    [SerializeField] public float groundCheckRadius = 0.4f;
    private bool isGrounded;
    [SerializeField][Range(0.0f, 0.5f)] public float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] public float mouseSmoothTime = 0.03f;

    [SerializeField] public bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        Jump();

    }

    void Jump()
    {
        isGrounded = Physics.OverlapSphere(groundChecker.position,
                                           groundCheckRadius,
                                           groundMask).Length > 0;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 jumpDirection = Vector3.zero;
            jumpDirection.y = jumpForce + transform.position.y;
            controller.Move(jumpDirection * Time.deltaTime);
            AudioManager.Instance.Play("Jump");
        }
    }


    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }
}
