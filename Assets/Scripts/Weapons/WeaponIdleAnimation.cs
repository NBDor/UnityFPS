using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdleAnimation : MonoBehaviour
{
    private float rotationAngle;
    private float rotationStep = 0.5f;
    private float maxHeight;
    private float minHeight;
    private float heightStep = 0.0025f;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        minHeight = this.transform.position.y;
        maxHeight = this.transform.position.y + 0.5f;
        rotationAngle = this.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        RotationAnimation();
        HeightAnimation();
    }

    void RotationAnimation()
    {
        rotationAngle = (rotationAngle > 360f) ? 0f : rotationAngle + rotationStep;
        this.transform.Rotate(0f, rotationStep, 0f, Space.World);
        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, rotationAngle, this.transform.rotation.z);
    }

    void HeightAnimation()
    {
        if(this.transform.position.y > maxHeight || this.transform.position.y < minHeight)
        {
            direction *= -1;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + heightStep * direction, this.transform.position.z);


    }
}
