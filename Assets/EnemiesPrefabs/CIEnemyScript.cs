using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIEnemyScript : MonoBehaviour
{
    public GameObject WhatToSpawn;
    public GameObject Spawner1;
    public GameObject Spawner2;
    public GameObject Spawner3;
    public GameObject Spawner4;

    private bool CanSpawn = true;
    private bool isDefeated = false;

    private float HackProccess = 0;

    public Material MyMat;

    IEnumerator WaitTime()
    {
        CanSpawn = false;
        Instantiate(WhatToSpawn, Spawner1.transform.position, Spawner1.transform.rotation);
        Instantiate(WhatToSpawn, Spawner2.transform.position, Spawner2.transform.rotation);
        Instantiate(WhatToSpawn, Spawner3.transform.position, Spawner3.transform.rotation);
        Instantiate(WhatToSpawn, Spawner4.transform.position, Spawner4.transform.rotation);
        yield return new WaitForSeconds(7);
        CanSpawn = true;
    }

    void Awake()
    {
        GameObject.Find("HackBar").GetComponent<Slider>().maxValue = 10;
        GameObject.Find("HackBar").GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);

        MyMat.SetColor("_Color", Color.red);
        MyMat.SetColor("_EmissionColor", Color.red * 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerPosition = GameObject.FindWithTag("Player").transform;
        

        if (((int)transform.position.x + 3612) / 24 == ((int)playerPosition.position.x + 3612) / 24 
            && ((int)transform.position.z + 3612) / 24 == ((int)playerPosition.position.z + 3612) / 24 && isDefeated==false)
        {
            GameObject.Find("HackBar").GetComponent<Slider>().value += 10f * Time.deltaTime;
            if (CanSpawn) { StartCoroutine(WaitTime()); }
            GameObject.Find("HackBar").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Debug.Log("1");
        }
        else
        {
            GameObject.Find("HackBar").GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
            GameObject.Find("HackBar").GetComponent<Slider>().value = 0;
            Debug.Log("2");
        }
        
        if (GameObject.Find("HackBar").GetComponent<Slider>().value == GameObject.Find("HackBar").GetComponent<Slider>().maxValue)
        {
            GameObject.Find("ShieldModel").GetComponent<ShieldScript>().IsShieldUnlocked = true;
            isDefeated = true;
            MyMat.SetColor("_Color", Color.green);
            MyMat.SetColor("_EmissionColor", Color.green * 5f);

        }

    }
}
