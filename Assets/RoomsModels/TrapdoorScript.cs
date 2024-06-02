using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorScript : MonoBehaviour
{
    private bool ShouldMoveDown = false;
    private bool ShouldDestroy = false;
    // Start is called before the first frame update
    IEnumerator Waiter1()
    {
        yield return new WaitForSeconds(4);
        ShouldMoveDown = true;
    }
    IEnumerator Waiter2()
    {
        yield return new WaitForSeconds(10);
        ShouldDestroy = true;
    }
    void Start()
    {
        StartCoroutine(Waiter1());
        StartCoroutine(Waiter2());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ShouldMoveDown == true)
        {
            transform.position -= new Vector3(0, 10, 0) * Time.deltaTime;
        }
        if (ShouldDestroy == true)
        {
            Destroy(gameObject);
        }
    }
}
