using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLock : MonoBehaviour
{
    public float mouseXSensitivity = 100;
    public float mouseYSensitivity = 100;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseXSensitivity = PlayerPrefs.GetFloat("MSensitivity");
        mouseYSensitivity = PlayerPrefs.GetFloat("MSensitivity");
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
