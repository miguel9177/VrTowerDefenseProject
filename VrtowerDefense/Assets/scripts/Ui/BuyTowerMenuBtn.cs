using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerMenuBtn : MonoBehaviour
{
    [SerializeField]
    //this will get the tower to be spawned
    GameObject TowerToSpawn;
    [SerializeField]
    //this will get the spawn tower controller
    SpawnTowerController spawnController;
    [SerializeField]
    //this wil save the price off the tower
    int price = 3000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendTowerToSpawner()
    {
        //this will change the tower to spawn
        spawnController.TowerToSpawn = TowerToSpawn;
        spawnController.towerPrice = price;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
