using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMover : MonoBehaviour
{
    [SerializeField] private Transform transform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            transform.position += new Vector3(10f, 0f, 0f);
    }
}
