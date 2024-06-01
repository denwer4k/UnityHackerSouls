using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtPlayer : MonoBehaviour
{
    private Transform playerPos;

    [SerializeField] private string playerTag;

    private void Start()
    {
        playerPos = GameObject.FindWithTag(playerTag).transform;
    }

    private void Update()
    {
        transform.LookAt(playerPos);
    }
}
