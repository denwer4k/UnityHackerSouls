using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOprojectileScript : MonoBehaviour
{
    private Rigidbody RB;
    public float Power = 1000f;
    IEnumerator DestroyCor()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        RB.AddRelativeForce(Power, 0f, 0f);
        Player = GameObject.FindWithTag("Player");
    }

    private GameObject Player;
    [SerializeField] private float DistanceForPickup = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < DistanceForPickup)
        {
            Player.GetComponent<PlayerHealth>().currentHealth -= 25;
            Destroy(gameObject);
        }
        StartCoroutine(DestroyCor());
    }
}
