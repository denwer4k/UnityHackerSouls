using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    IEnumerator StopExpl()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StopExpl());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
