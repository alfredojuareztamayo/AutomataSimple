using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
   [SerializeField] int grid_x;
   [SerializeField] int grid_y;
    [SerializeField] GameObject cubeAlive;
    [SerializeField] GameObject cubeDead;
    GameObject[] cubes;
    Tiles[,] tiles;
    float timeInScreen = 2f;


    void Start ()
    {
        cubes = new GameObject[grid_x*grid_y];
        tiles = new Tiles[grid_x, grid_y];
        StartCoroutine(ReGenerateGridRotine());
        GenerateGrid(tiles);
        PrintGrid(tiles);
        //FillArrayCubes();
       // InstanceCubes();
       // TurnCubesAndSetPosition();
        //SetCubesGrid(tiles);
        Debug.Log("El tamaño del tablero es: " + tiles.Length);
        Debug.Log("El tamaño de los cubos son: " + cubes.Length);
    }

    // Update is called once per frame
    void Update ()
    {
       // ReGenerateGrid();
    }

    private void GenerateGrid(Tiles[,] tiles)
    {
        
        for(int x = 0; x < grid_x; x++)
        {
            for(int y = 0; y < grid_y; y++)
            {
                tiles[x, y] = new Tiles(x, y);
                int randNum = Random.Range(0, 2);
                tiles[x, y].SetIsDead(randNum  != 1 ? true : false);
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
                printArray += tiles[x, y].m_isDead ? '0' : '1';
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
                GameObject cube = Instantiate(tiles[x, y].m_isDead? cubeDead:cubeAlive, new Vector3(tiles[x, y].m_x, tiles[x, y].m_y), Quaternion.identity);
                Destroy(cube, timeInScreen);                
            }
        }
    }

    private void FillArrayCubes()
    {   
        int i = 0;
        for (int x = 0; x < grid_x; x++)
        {
            for (int y = 0; y < grid_y; y++)
            {
                cubes[i] = tiles[x, y].m_isDead ? cubeDead : cubeAlive;
                i++;
            }
        }
    }

    private void InstanceCubes()
    {
        foreach(GameObject c in cubes)
        {
             Instantiate(c, Vector3.zero, Quaternion.identity);

            c.SetActive(false);
        }
    }
    private void TurnCubesAndSetPosition()
    {
        int i = 0;
        for (int x = 0; x < grid_x; x++)
        {
            for (int y = 0; y < grid_y; y++)
            {
                cubes[i].transform.position = new Vector3(tiles[x, y].m_x, tiles[x, y].m_y, 0);
                cubes[i].SetActive(true);
                i++;
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
            yield return new WaitForSeconds(timeInScreen);
        }
    }
}
