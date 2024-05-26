using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Random.InitState(1);
    //Debug.Log(Random.value);
    //Debug.Log(Random.value);
    //Debug.Log(Random.value);
    //Debug.Log(Random.value);
    private const int maxSizeX = 20;
    private const int maxSizeY = 20;
    int[,] dungShape = new int[maxSizeX, maxSizeY];

    private int numberOfIterations = 9; // how much branches there will be

    private int minPixelAmount = 5;
    private int currPixelAmount = 0;

    private float chanceOfBranching = 0.6f;
    private float chanceOfClumping = 0.1f;

    public int DungeonSeed = 1;



    private int sizeOfRoom = 16;

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
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3((i - maxSizeX / 2) * sizeOfRoom, 0, (j - maxSizeY / 2) * sizeOfRoom);
                }
            }
        }
    }
    private void Update()
    {
        
    }
}