using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
   [SerializeField] int grid_x;
   [SerializeField] int grid_y;
    [SerializeField] GameObject cubeAlive;
    [SerializeField] GameObject cubeDead;
    Tiles[,] tiles;


    void Start ()
    {
        tiles = new Tiles[grid_x, grid_y];
        StartCoroutine(ReGenerateGridRotine());
        //GenerateGrid(tiles);
        //PrintGrid(tiles);
        //SetCubesGrid(tiles);
    }

    // Update is called once per frame
    void Update ()
    {
        ReGenerateGrid();
    }

    private void GenerateGrid(Tiles[,] tiles)
    {
        
        for(int x = 0; x < grid_x; x++)
        {
            for(int y = 0; y < grid_y; y++)
            {
                tiles[x, y] = new Tiles(x, y);
                int randNum = Random.Range(0, 2);
                if (randNum != 1)
                {
                    tiles[x, y].SetIsDead(true);
                }
                else
                {
                    tiles[x, y].SetIsDead(false);
                }
            }
        }
    }

    private void PrintGrid(Tiles[,] tiles)
    {
        string printArray = " ";
        for (int x = 0; x < grid_x; x++)
        {
            for (int y = 0; y < grid_y; y++)
            {
                if (tiles[x, y].m_isDead)
                {
                    printArray += '0';
                }
                else
                {
                    printArray += '1';
                }
                printArray += ',';
            }
            printArray += '\n';
        }
        Debug.Log(printArray);
    }

    public void SetCubesGrid(Tiles[,] tiles)
    {
        for(int x =0; x < grid_x; x++)
        {
            for(int y=0; y < grid_y; y++)
            {
                if (tiles[x, y].m_isDead)
                {
                    Instantiate(cubeDead, new Vector3(tiles[x,y].m_x, tiles[x, y].m_y), Quaternion.identity);
                }
                else
                {
                    Instantiate(cubeAlive, new Vector3(tiles[x, y].m_x, tiles[x, y].m_y), Quaternion.identity);
                }
                
            }
        }
    }

    public void ReGenerateGrid()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateGrid(tiles);
            PrintGrid(tiles);
            SetCubesGrid(tiles);
        }
    }

    IEnumerator ReGenerateGridRotine()
    {
        while (true)
        {
            GenerateGrid(tiles);
            PrintGrid(tiles);
            SetCubesGrid(tiles);
            yield return new WaitForSeconds(2f);
        }
    }
}
