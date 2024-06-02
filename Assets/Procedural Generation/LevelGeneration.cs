using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private const int maxSizeX = 20;
    private const int maxSizeY = 20;
    int[,] dungShape = new int[maxSizeX, maxSizeY];

    private int numberOfIterations = 6; // how much branches there will be

    private int minPixelAmount = 5;
    private int currPixelAmount = 0;

    private float chanceOfBranching = 0.6f;
    private float chanceOfClumping = 0.1f;

    public int DungeonSeed = 1;

    private Vector3 WTH = new Vector3(-0.5f, 0f, 0f);



    private int sizeOfRoom = 24;
    public GameObject AllDoorsR;
    public GameObject OneDoorR;
    public GameObject ThreeDoorsR;
    public GameObject ThroughDoorsR;
    public GameObject CornerDoorsR;
    public GameObject DoorPrefab;


    public GameObject DdosEnemy;
    public GameObject SOminiboss;
    public GameObject RCminiboss;
    public GameObject CIminiboss;

    private bool spawnedCI = false;
    private bool spawnedRC = false;
    private bool spawnedSO = false;

    private bool RCdefeated = false;
    private bool SOdefeated = false;

    //spawn points for enemies
    private Vector3 SpwnP1 = new Vector3(-8, 0, -8);
    private Vector3 SpwnP2 = new Vector3(8, 0, -8);
    private Vector3 SpwnP3 = new Vector3(-8, 0, 8);
    private Vector3 SpwnP4 = new Vector3(8, 0, 8);

    int GetNeighboringRoomsAmount(int x, int y, int[,] arrayum)
    {
        int ret = 0;
        if (arrayum[x + 1, y] != 0)
        {
            ret += 1;
        }
        if (arrayum[x - 1, y] != 0)
        {
            ret += 1;
        }
        if (arrayum[x, y + 1] != 0)
        {
            ret += 1;
        }
        if (arrayum[x, y - 1] != 0)
        {
            ret += 1;
        }
        return ret;
    }

    private void Start()
    {
        // initialize seed
        UnityEngine.Random.InitState(DungeonSeed);

        //fill array with empty space
        for (int i = 0; i < dungShape.GetLength(0); i++)
        {
            for (int j = 0; j < dungShape.GetLength(1); j++)
            {
                dungShape[i, j] = 0;
            }
        }

        


        dungShape[maxSizeX/2, maxSizeY/2] = 1;

        // going through branch iterations
        for (int i = 1;i < numberOfIterations; i++)
        {
            // going through every element of the array
            for (int a = 1; a < dungShape.GetLength(0) - 1; a++)
            {
                for (int b = 1; b < dungShape.GetLength(1) - 1; b++)
                {
                    bool wasPixelCreated = false;
                    //////////////////////////////////////////////////////////////////
                    // create new pixels
                    if (dungShape[a, b] == i)
                    {
                        if (dungShape[a + 1, b] == 0)
                        {
                            if (GetNeighboringRoomsAmount(a + 1, b, dungShape) == 1 && UnityEngine.Random.value < chanceOfBranching)
                            {
                                dungShape[a + 1, b] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                            else if (GetNeighboringRoomsAmount(a + 1, b, dungShape) > 1 && UnityEngine.Random.value < chanceOfClumping)
                            {
                                dungShape[a + 1, b] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                        }
                        if (dungShape[a -1, b] == 0)
                        {
                            if (GetNeighboringRoomsAmount(a - 1, b, dungShape) == 1 && UnityEngine.Random.value < chanceOfBranching)
                            {
                                dungShape[a - 1, b] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                            else if (GetNeighboringRoomsAmount(a - 1, b, dungShape) > 1 && UnityEngine.Random.value < chanceOfClumping)
                            {
                                dungShape[a - 1, b] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                        }
                        if (dungShape[a, b+1] == 0)
                        {
                            if (GetNeighboringRoomsAmount(a, b + 1, dungShape) == 1 && UnityEngine.Random.value < chanceOfBranching)
                            {
                                dungShape[a, b+1] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                            else if (GetNeighboringRoomsAmount(a, b+1, dungShape) > 1 && UnityEngine.Random.value < chanceOfClumping)
                            {
                                dungShape[a, b+1] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                        }
                        if (dungShape[a, b-1] == 0)
                        {
                            if (GetNeighboringRoomsAmount(a , b-1, dungShape) == 1 && UnityEngine.Random.value < chanceOfBranching)
                            {
                                dungShape[a, b-1] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                            else if (GetNeighboringRoomsAmount(a , b - 1, dungShape) > 1 && UnityEngine.Random.value < chanceOfClumping)
                            {
                                dungShape[a, b-1] = i + 1;
                                wasPixelCreated = true;
                                currPixelAmount += 1;
                            }
                        }
                        //failsafe in case pixel is not created
                        if (wasPixelCreated == false && currPixelAmount < minPixelAmount)
                        {
                            if (dungShape[a + 1, b] == 0)
                            {
                                dungShape[a + 1, b] = i + 1;
                            }
                            else if (dungShape[a - 1, b] == 0)
                            {
                                dungShape[a - 1, b] = i + 1;
                            }
                            else if (dungShape[a, b + 1] == 0)
                            {
                                dungShape[a, b + 1] = i + 1;
                            }
                            else if (dungShape[a, b - 1] == 0)
                            {
                                dungShape[a, b - 1] = i + 1;
                            }
                        }
                    
                    }
                    ///////////////////////////////////////////////////////

                }
            }

        }

        //print array in console
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dungShape.GetLength(1); i++)
        {
            for (int j = 0; j < dungShape.GetLength(0); j++)
            {
                sb.Append(dungShape[i, j]);
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());



        //TEST GENERATION DONT USE FOLLOWING CODE IN GAME
        for (int i = 0; i < dungShape.GetLength(1); i++)
        {
            for (int j = 0; j < dungShape.GetLength(0); j++)
            {
                if (dungShape[i,j] != 0)
                {
                    if (GetNeighboringRoomsAmount(i, j, dungShape) == 4)
                    {
                        Instantiate(AllDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 0));

                    }

                    if (GetNeighboringRoomsAmount(i, j, dungShape) == 1)
                    {
                        if (dungShape[i+1, j] != 0) Instantiate(OneDoorR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 90));
                        if (dungShape[i - 1, j] != 0) Instantiate(OneDoorR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, -90));
                        if (dungShape[i, j+1] != 0) Instantiate(OneDoorR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 0));
                        if (dungShape[i, j - 1] != 0) Instantiate(OneDoorR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 180));
                    }
                    if (GetNeighboringRoomsAmount(i, j, dungShape) == 3)
                    {
                        if (dungShape[i + 1, j] == 0) Instantiate(ThreeDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, -90));
                        if (dungShape[i - 1, j] == 0) Instantiate(ThreeDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 90));
                        if (dungShape[i, j + 1] == 0) Instantiate(ThreeDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 180));
                        if (dungShape[i, j - 1] == 0) Instantiate(ThreeDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 0));
                    }
                    if (GetNeighboringRoomsAmount(i, j, dungShape) == 2)
                    {
                        //if it is like a tunnel
                        if (dungShape[i + 1, j] != 0 && dungShape[i - 1, j] != 0 && dungShape[i, j + 1] == 0 && dungShape[i, j - 1] == 0)
                        {
                            Instantiate(ThroughDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 90));
                        }
                        if (dungShape[i, j + 1] != 0 && dungShape[i, j - 1] != 0 && dungShape[i + 1, j] == 0 && dungShape[i - 1, j] == 0)
                        {
                            Instantiate(ThroughDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 0));
                        }

                        //if it is some corner doors
                        if (dungShape[i + 1, j] != 0 && dungShape[i, j+1] != 0) Instantiate(CornerDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 0));

                        if (dungShape[i + 1, j] != 0 && dungShape[i, j - 1] != 0) Instantiate(CornerDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 90));
                        if (dungShape[i - 1, j] != 0 && dungShape[i, j + 1] != 0) Instantiate(CornerDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, -90));
                        if (dungShape[i - 1, j] != 0 && dungShape[i, j - 1] != 0) Instantiate(CornerDoorsR, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(-90, 0, 180));
                    }

                }

                //Spawn em DOORS!
                Instantiate(DoorPrefab, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + new Vector3(12, -8, 0), Quaternion.Euler(0, 0, 0));
                Instantiate(DoorPrefab, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + new Vector3(0, -8, 12), Quaternion.Euler(0, 90, 0));



                //Spawn da enemies
                if (dungShape[i, j] != 0)
                {
                    Instantiate(DdosEnemy, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + SpwnP1, Quaternion.Euler(0, 0, 0));
                    Instantiate(DdosEnemy, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + SpwnP2, Quaternion.Euler(0, 0, 0));
                    Instantiate(DdosEnemy, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + SpwnP3, Quaternion.Euler(0, 0, 0));
                    Instantiate(DdosEnemy, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom) + SpwnP4, Quaternion.Euler(0, 0, 0));
                }
            }
        }
        //Spawning dem enemies
        for (int i = 0; i < dungShape.GetLength(1); i++)
        {
            for (int j = 0; j < dungShape.GetLength(0); j++)
            {
                if (dungShape[i, j] != 0)
                {
                    // TESTING!!!!!
                    //Instantiate(CIminiboss, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(0, 0, 0));
                    if (dungShape[i, j] == numberOfIterations)
                    {
                        if (spawnedCI == false)
                        {
                            Instantiate(CIminiboss, new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(0, 0, 0));
                            spawnedCI = true;
                        }
                    }
                    
                }
                if (dungShape[maxSizeX - i - 1, j] != 0)
                {
                    if (dungShape[maxSizeX - i - 1, j] == numberOfIterations - 1)
                    {
                        if (spawnedRC == false)
                        {
                            Instantiate(RCminiboss, new Vector3((maxSizeX - i - 1 - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(0, 0, 0));
                            spawnedRC = true;
                        }
                    }

                }
                if (dungShape[maxSizeX - i - 1, maxSizeY-j - 1] != 0)
                {
                    if (dungShape[maxSizeX - i - 1, maxSizeY - j - 1] == numberOfIterations - 2)
                    {
                        if (spawnedSO == false)
                        {
                            Instantiate(SOminiboss, new Vector3((maxSizeX - i - 1 - maxSizeX / 2) * sizeOfRoom, 0, (maxSizeY - j - 1 - maxSizeY / 2) * sizeOfRoom), Quaternion.Euler(0, 0, 0));
                            spawnedSO = true;
                        }
                    }

                }

            }
        }
    }


    // Abilities unlock here!
    private void Update()
    {
        if (RCdefeated == false)
        {
            if (GameObject.FindWithTag("RacingConditionTag") == null)
            {
                RCdefeated = true;
                Debug.Log("Yo1");
                GameObject.Find("First Person Player").GetComponent<PlayerMovemnt>().IsAllowedToDash = true;
                GameObject.Find("First Person Player").GetComponent<PlayerMovemnt>().IsAllowedToDoubleJump = true;
                GameObject.Find("First Person Player").GetComponent<PlayerMovemnt>().IsAllowedToSlide = true;
            }
        }
        if(SOdefeated == false)
        {
            if (GameObject.FindWithTag("StackOverflowTag") == null)
            {
                SOdefeated = true;
                GameObject.Find("PlayerHandModel").GetComponent<Fire>().ShootAbilityUnlocked = true;
            }

        }
        
    }
}
