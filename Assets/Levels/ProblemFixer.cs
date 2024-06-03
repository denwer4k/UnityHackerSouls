using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemFixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("HackBar").GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
