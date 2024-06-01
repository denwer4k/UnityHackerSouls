using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Transform playa;
    private bool canUse = true;
    public bool IsShieldUnlocked = false;

    private Vector3 additional = new Vector3(90, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator Go()
    {
        canUse = false;
        transform.position = playa.position;
        transform.rotation = playa.rotation * Quaternion.Euler(additional);
        yield return new WaitForSeconds(7f);
        transform.position = new Vector3(0, -100, 0);
        canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canUse == true && IsShieldUnlocked == true)
        {
            StartCoroutine(Go());   
        }
    }
}
