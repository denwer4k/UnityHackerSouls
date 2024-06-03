using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCameraScript : MonoBehaviour
{
    public Slider MSslider;

    //public float mouseXSensitivity = 100;
    //public float mouseYSensitivity = 100;

    private float xRotation = 0f;
    public Transform playerBody;
    public Quaternion InitTrans;
    public bool isTesting = false;
    // Start is called before the first frame update
    void Start()
    {
        InitTrans = transform.rotation;
    }
    public void StartTesting()
    {
        isTesting = true;
    }
    public void StopTesting()
    {
        isTesting = false;
    }

    public void SaveDateInPlPr()
    {
        PlayerPrefs.SetFloat("MSensitivity", MSslider.value);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(MSslider.value);
        if (isTesting == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * MSslider.value * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MSslider.value * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            transform.rotation = InitTrans;
        }

    }
}
