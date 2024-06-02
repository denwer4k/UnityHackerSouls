using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float DistanceForPickup = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <DistanceForPickup )
        {
            Player.GetComponent<PlayerHealth>().currentHealth += 20;
            Destroy(gameObject);
        }
            
    }
}
