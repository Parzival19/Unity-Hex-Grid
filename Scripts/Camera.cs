using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Keyboard Orbit")]
public class Camera : MonoBehaviour
{
    public float distance;
    public float zoomSpd = 1.0f;

    public float xSpeed = 5.0f;
    public float ySpeed = 5.0f;

    private float x;
    private float y;

    public void LateUpdate()
    {

            x += Input.GetAxis("Horizontal") * xSpeed;
            y += Input.GetAxis("Vertical") * ySpeed;


            distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpd;

            Vector3 position = new Vector3(x, distance, y);

            transform.position = position;
        
    }
}
