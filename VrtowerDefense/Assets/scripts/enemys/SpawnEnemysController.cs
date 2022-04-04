using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemysController : MonoBehaviour
{
    
    //this will save the transform of the enemies parent so that i can make the enemies a child of the enemy parent
    public Transform enemiesParent;
    [SerializeField]
    //this will create a conection from this script to the gamemanager script
    GameManager gamemanager;
    //the 0 index is the right spawn the 1 is center spawn and the 2 is the left spawn
    [SerializeField]
    GameObject[] Spawns;
    //the 0 index is the right path the 1 is center path and the 2 is the left path, we need this to when we spawn the enemy we will tell him wich path to follow
    [SerializeField]
    GameObject[] EnemyPaths;
    //this will store every enemy that can be spawned
    [SerializeField]
    GameObject[] ObjectsToSpawn;
    [SerializeField]
    //this is the time it will take to spawn an enemy
    public float TimeToWaitPerSpawn;
    //this bool is going to be true when the round ends, and it will stop the spawning, its value is changed by the game manager
    [HideInInspector]
    public bool pauseRound = true;
    // Start is called before the first frame update
    void Start()
    {
        //this will call the ienumerator that will make a yield return of the seconds in the variable timetowaitperspawn
        //StartCoroutine(WaitThenSpawn(TimeToWaitPerSpawn));
    }

    public IEnumerator WaitThenSpawn(float TimeToWaitPerSpawn_)
    {
        //wait timetowaitperspawn seconds then do the code below
        yield return new WaitForSeconds(TimeToWaitPerSpawn_);
        //if the round hasnt finished, spawn enemy
        if(pauseRound==false)
        {
            //call the function spawn enemy
            SpawnRandomEnemy();
            //call this function again to wait a few seconds and then spawn again
            StartCoroutine(WaitThenSpawn(TimeToWaitPerSpawn_));
        }
    }

    void SpawnRandomEnemy()
    {
        //im using a try so that if something goes wrong the show must go on XD
        try
        {
            //fazer um random number entre 0 e o numero de objetos na variavel ObjectsToSpawn
            int indexOfObjectToSpawn = Random.Range(0, ObjectsToSpawn.Length);
            //if this gets a 0 it will spawn the enemy on the right path, if 1 center path if 2 left path
            int spawnIndexPosicion = Random.Range(0, 3);
            
            //this if makes sure the random doesnt get a number bigger then 2 because we can only have the numbers 0,1,2
            if (spawnIndexPosicion > 2)
            { spawnIndexPosicion = 2; }

            //if the enemy is a flying enemy
            if(ObjectsToSpawn[indexOfObjectToSpawn].gameObject.GetComponent<EnemyController>().IsAerial==true)
            {
                //this will spawn the enemy and store the enemy spawned so that i can add him the path, it will spawn him as a child of the Enemies parent
                GameObject EnemySpawned = Instantiate(ObjectsToSpawn[indexOfObjectToSpawn].gameObject, Spawns[spawnIndexPosicion + 3].transform.position, Spawns[spawnIndexPosicion+3].transform.rotation, enemiesParent.transform);
                //tell the game manager that we just added another enemie
                gamemanager.SpawnedAnEnemie();
                //tells the spawn enemy to go to use the path of the spawn (0 to right, 1 to center, 2 to left) i do + 3  because this is an aerial enemy, so the paths are always plus 3, if it was the 0 path, now is the 3
                EnemySpawned.gameObject.GetComponent<EnemyController>().EnemyPathGameObject = EnemyPaths[spawnIndexPosicion + 3];
            }
            else
            {
                //this will spawn the enemy and store the enemy spawned so that i can add him the path, it will spawn him as a child of the Enemies parent
                GameObject EnemySpawned = Instantiate(ObjectsToSpawn[indexOfObjectToSpawn].gameObject, Spawns[spawnIndexPosicion].transform.position, Spawns[spawnIndexPosicion].transform.rotation, enemiesParent.transform);
                //tell the game manager that we just added another enemie
                gamemanager.SpawnedAnEnemie();
                //tells the spawn enemy to go to use the path of the spawn (0 to right, 1 to center, 2 to left)
                EnemySpawned.gameObject.GetComponent<EnemyController>().EnemyPathGameObject = EnemyPaths[spawnIndexPosicion];
            }
        }
        catch
        {
            Debug.Log("Something broke");
            //if something broke, spawn someone
            StartCoroutine(WaitThenSpawn(TimeToWaitPerSpawn));
        }
       
    }

    //this will destroy every enemy, for example when we lose
    public void DestroyEveryEnemie()
    {
        //this will destroy every child and pause the round so that we can stop the enemy spawning
        foreach (Transform child in enemiesParent.transform)
        {
            Destroy(child.gameObject);
            pauseRound = true;
        }
    }
}
