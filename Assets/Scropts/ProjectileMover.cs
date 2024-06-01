using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Rigidbody RB;
    public float Power = 50f;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.AddRelativeForce(Power, 0f, 0f);
    }

}
