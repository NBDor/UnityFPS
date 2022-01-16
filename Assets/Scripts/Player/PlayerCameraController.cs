using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Camera camera;
    public float mouseSensitivity = 240f; // TODO - add slider for mouse sensitivity ?
    private float headRotation = 0f;
    //[SerializeField] float limitHeadRotation = 90; // 90 degrees
    // Start is called before the first frame update

    void Start()
    {
        // locks mouse to game view, press ESC to exit
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * -1;

        // turn around by x in the y-axis
        this.transform.Rotate(0, x, 0);
        headRotation += y;
        //headRotation = Mathf.Clamp(headRotation, -limitHeadRotation, limitHeadRotation);

        camera.transform.localEulerAngles = new Vector3(headRotation, 0, 0);
    }
}

