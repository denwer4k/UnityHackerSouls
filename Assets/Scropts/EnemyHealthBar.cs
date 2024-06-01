using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private string damagerTag = "PlayerBullet";
    [SerializeField] private float  getdamage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(damagerTag))
            currentHP -= getdamage;
        if (currentHP <= 0)
            Destroy(gameObject);
    }

}
