using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float DistanceForPickup = 2f;
    [SerializeField] private AudioClip HealClip;
    private AudioSource KitSource;
    private bool HasBeenPickedUp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        KitSource = Player.GetComponent<AudioSource>();
    }
    IEnumerator Healing()
    {
        HasBeenPickedUp = true;
        KitSource.PlayOneShot(HealClip);
        Player.GetComponent<PlayerHealth>().currentHealth += 10;
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <DistanceForPickup  && HasBeenPickedUp == false)
        {
            StartCoroutine(Healing());     
        }
            
    }
}
