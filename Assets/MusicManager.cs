using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip EarlyGameMusic;
    [SerializeField] private AudioClip BossfightMusic;
    [SerializeField] private AudioClip DungeonMusic;
    private AudioSource MusicSource;

    private bool isClassicPlaying = true;
    private bool isDungeonPlaying = false;
    

    public bool ShouldPlayBossMusic = false;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        MusicSource = GetComponent<AudioSource>();
        MusicSource.PlayOneShot(EarlyGameMusic);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 15 && isClassicPlaying == true)
        {
            isClassicPlaying = false;
            isDungeonPlaying = true;
            MusicSource.Stop();
            MusicSource.PlayOneShot(DungeonMusic);
            
        }
        if(player.transform.position.y > 15 && isDungeonPlaying == true)
        {
            isDungeonPlaying = false;
            isClassicPlaying = true;
            MusicSource.Stop();
            MusicSource.PlayOneShot(EarlyGameMusic);
            if (!MusicSource.isPlaying)
            {
                MusicSource.PlayOneShot(EarlyGameMusic);
            }
        }
        if (player.transform.position.y < 15 && isDungeonPlaying == true)
        {
            if (!MusicSource.isPlaying)
            {
                MusicSource.PlayOneShot(DungeonMusic);
            }
        }
        if (player.transform.position.y > 15 && isClassicPlaying == true)
        {
            if (!MusicSource.isPlaying)
            {
                MusicSource.PlayOneShot(EarlyGameMusic);
            }
        }

        if(ShouldPlayBossMusic == true)
        {
            if(isDungeonPlaying == true || isClassicPlaying == true)
            {
                MusicSource.Stop();
            }
            isDungeonPlaying = false;
            isClassicPlaying = false;
            //MusicSource.PlayOneShot(BossfightMusic);
            if (isDungeonPlaying == false && isClassicPlaying == false && !MusicSource.isPlaying)
            {
                MusicSource.PlayOneShot(BossfightMusic);
            }
        }


        if (player.transform.position.y < 20 && player.transform.position.y > 10)
        {
            MusicSource.volume = Mathf.Abs((player.transform.position.y - 15) / 5);
        }
        else
        {
            MusicSource.volume = 1;
        }
        
    }
}
