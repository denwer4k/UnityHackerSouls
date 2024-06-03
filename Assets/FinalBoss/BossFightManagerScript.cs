using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossFightManagerScript : MonoBehaviour
{
    private bool isFirstPhaseStarted = false;
    private bool isSecondPhaseStarted = false;
    public GameObject BaseRobot;
    public GameObject BossPhase1;
    public GameObject BossPhase2;
    public GameObject BossSpawner;
    public GameObject PlayerTeleporter;

    public GameObject SpawnP1;
    public GameObject SpawnP2;
    public GameObject SpawnP3;
    public GameObject SpawnP4;
    public GameObject SpawnP5;

    private GameObject player;

    public GameObject BossGate;
    private Vector3 WhereToPlaceGate = new Vector3(0, 30, -38);

    public GameObject ShieldInManager;


    private GameObject BossPhaseSecondInScene;
    private bool SummonDemRobots = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    IEnumerator startPhase2()
    {
        //play sound clip here
        yield return new WaitForSeconds(1);
        
        Instantiate(BossPhase2, BossSpawner.transform.position, BossSpawner.transform.rotation);
        //GameObject.FindWithTag("BossPhase2").GetComponent<FinalBossPhase2Script>().Shield = ShieldInManager;
        ShieldInManager.transform.position = BossSpawner.transform.position + new Vector3(0, -3, 0);
        BossPhaseSecondInScene = GameObject.FindWithTag("BossPhase2");
    }
    
    IEnumerator spawningRobots()
    {
        
        SummonDemRobots = false;
        yield return new WaitForSeconds(2);
        BossPhaseSecondInScene.GetComponent<FinalBossPhase2Script>().ShouldPlaySummonAnimation = true;
        Instantiate(BaseRobot, SpawnP1.transform.position, SpawnP1.transform.rotation); 
        Instantiate(BaseRobot, SpawnP2.transform.position, SpawnP2.transform.rotation); 
        Instantiate(BaseRobot, SpawnP3.transform.position, SpawnP3.transform.rotation); 
        Instantiate(BaseRobot, SpawnP4.transform.position, SpawnP4.transform.rotation); 
        Instantiate(BaseRobot, SpawnP5.transform.position, SpawnP5.transform.rotation);
        yield return new WaitForSeconds(25);
        SummonDemRobots = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstPhaseStarted == true && GameObject.FindWithTag("BossPhase1") == null && isSecondPhaseStarted == false)
        {
            isSecondPhaseStarted = true;
            StartCoroutine(startPhase2());
            
            //player.transform.position = PlayerTeleporter.transform.position;
            //player.transform.rotation = PlayerTeleporter.transform.rotation;
        }
        if (SummonDemRobots == true && isSecondPhaseStarted == true)
        {
            Debug.Log(SpawnP1);
            StartCoroutine(spawningRobots());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFirstPhaseStarted == false && other.CompareTag("Player"))
        {
            isFirstPhaseStarted = true;
            Instantiate(BossPhase1, BossSpawner.transform.position, BossSpawner.transform.rotation);
            BossGate.transform.position = WhereToPlaceGate;
        }
    }
}
