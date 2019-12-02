using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLayout : MonoBehaviour
{
    // the transform limits of the area where obstacles can be placed 
    // these numbers have been chosen to leave a corridor around the outer edge 
    // of the environment so that there is always a path through
    public int lowerX = -10;
    public int upperX = 9;
    public int lowerY = 12;
    public int upperY = 302;

    // values that control how many obstacles are placed in the environment 
    // the script will spawn a random number within theses bounds
    public int minObstacles = 30;
    public int maxObstacles = 40;

    //container to hold obstacle prefabs
    public GameObject[] obstacles;

    private Spawn_Manager spawnManager;
    void Start()
    {
        spawnManager = FindObjectOfType<Spawn_Manager>();
    }

    public void SpawnObstacles()
    {

    }
}
