using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossFightManagerScript : MonoBehaviour
{
    public ParticleSystem AppearParticles;

    [SerializeField] private AudioClip YouWillDie;
    [SerializeField] private AudioClip FirstApeearance;
    
    private AudioSource BossSource;

    private GameObject MusicMngr;

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

    private bool WasthereBoss2 = false;
    // Start is called before the first frame update
    void Start()
    {
        MusicMngr = GameObject.FindWithTag("MusicManager");
        BossSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
    }
    IEnumerator startPhase2()
    {
        MusicMngr.GetComponent<MusicManager>().ShouldPlayBossMusic = true;
        //play sound clip here
        BossSource.PlayOneShot(YouWillDie);
        yield return new WaitForSeconds(8.5f);
        Instantiate(AppearParticles, BossSpawner.transform);
        yield return new WaitForSeconds(6.5f);
        
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        player.GetComponent<CharacterController>().enabled = true;

        Instantiate(BossPhase2, BossSpawner.transform.position, BossSpawner.transform.rotation);
        WasthereBoss2 = true;
        //GameObject.FindWithTag("BossPhase2").GetComponent<FinalBossPhase2Script>().Shield = ShieldInManager;
        ShieldInManager.transform.position = BossSpawner.transform.position + new Vector3(0, -3, 0);
        BossPhaseSecondInScene = GameObject.FindWithTag("BossPhase2");
        yield return new WaitForSeconds(4);
        Destroy(GameObject.FindWithTag("CreatorParticles"));
    }
    
    IEnumerator spawningRobots()
    {
        
        SummonDemRobots = false;
        
        yield return new WaitForSeconds(16);
        
        BossPhaseSecondInScene.GetComponent<FinalBossPhase2Script>().ShouldPlaySummonAnimation = true;
        Instantiate(BaseRobot, SpawnP1.transform.position, SpawnP1.transform.rotation); 
        Instantiate(BaseRobot, SpawnP2.transform.position, SpawnP2.transform.rotation); 
        Instantiate(BaseRobot, SpawnP3.transform.position, SpawnP3.transform.rotation); 
        Instantiate(BaseRobot, SpawnP4.transform.position, SpawnP4.transform.rotation); 
        Instantiate(BaseRobot, SpawnP5.transform.position, SpawnP5.transform.rotation);
        yield return new WaitForSeconds(4);
        SummonDemRobots = true;
    }

    IEnumerator TransportToCongrats()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("CongratulationsScreen");
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


        // Check if boss is defeated
        if(WasthereBoss2 == true && GameObject.FindWithTag("BossPhase2") == null)
        {
            Debug.Log("Congrats!");
            player.GetComponent<PlayerHealth>().currentHealth = player.GetComponent<PlayerHealth>().maxHealth;
            StartCoroutine(TransportToCongrats());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFirstPhaseStarted == false && other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().isInBossfight = true;
            isFirstPhaseStarted = true;
            BossSource.PlayOneShot(FirstApeearance);
            Instantiate(BossPhase1, BossSpawner.transform.position, BossSpawner.transform.rotation);
            BossGate.transform.position = WhereToPlaceGate;
        }
    }
}
