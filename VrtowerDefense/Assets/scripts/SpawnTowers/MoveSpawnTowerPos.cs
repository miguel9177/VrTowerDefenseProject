using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawnTowerPos : MonoBehaviour
{
    [SerializeField]
    //i need to get this object aso that i can deactivate after spawning
    GameObject smallArena;
    //this will be true if the tower is inside the roud or in a tower
    [HideInInspector]
    public bool isOnBadPos=false;
    //this will be true if the tower is inside the spawnable ground
    [HideInInspector]
    public bool isOnSpawnablePos = false;
    //this will get this objects box collider so that i can activate it and deactivate it
    BoxCollider boxColliderOfThisObject;
    //this will be activated changed by the spawn tower controller
    [HideInInspector]
    public GameObject towerToSpawn=null;
    [SerializeField]
    GameObject TowersParent;
    [SerializeField]
    GameManager gamemanage;
    //this will be changed by the SpawnTowerController, that is changed by the buyTowerMenuBtn
    [HideInInspector]
    public int TowerPrice = 0;
    // Start is called before the first frame update
    void Start()
    {
        //start by getting this object collider so that i can check if this is in a good position
        boxColliderOfThisObject = this.gameObject.GetComponent<BoxCollider>();
        //i deactivate it, so that the ontrigger only activates when i want
        boxColliderOfThisObject.enabled = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function will activate the collider, call a coroutine that will check if this object is on a good position, i call a coroutine, because i need to wait some time for the ontrigger to do his thing
    public void CheckIfIsInsideGoodPos(int TowerPrice_)
    {
        //this will change the tower price to the price sended by  the spawntowercontroller
        TowerPrice = TowerPrice_;
        //activate the box collider so that the ontrigger activates
        boxColliderOfThisObject.enabled = true;
        //call a coroutine that will wait and then check if its on a good pos
        StartCoroutine(WaitThenCheckIfTowerCanSpawn(0.5f));
    }

    private IEnumerator WaitThenCheckIfTowerCanSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Debug.Log(isOnBadPos + " " + isOnSpawnablePos);

        //if the object to spawn is a trap ill just check if its on a bad pos, if it is, it means is on top of a road, so ill change the bool, so that it spawns
        if (towerToSpawn.CompareTag("Trap"))
        {
            //if is in a bad pos (on top of a road), change the bool, so that it spawns
            if(isOnBadPos==true)
            {
                isOnBadPos = false;
            }
            else
            {
                isOnBadPos = true;
            }
        }
        //this will check if the tower spawned, it it did it will be true, and will deactivate the minimmap
        bool didItSpawn = false;
        //if is on a good pos move
        if (isOnBadPos == false && isOnSpawnablePos==true)
        {
            //spawn tower, then change its pos
            GameObject towerSpawned=Instantiate(towerToSpawn, TowersParent.transform);
            towerSpawned.transform.position = this.transform.position;
            //call the function to spawn the tower
            gamemanage.SpawnTower(towerSpawned, towerSpawned.transform);
            //if the tower is a trap
            if(towerToSpawn.CompareTag("Trap"))
            {
                Debug.Log(towerSpawned.transform.localPosition.y);
                Destroy(towerSpawned);
            }
            didItSpawn = true;
            gamemanage.DecreaseMoney(TowerPrice);
            
        }
        //deactivate collider, and reset the variables for us to be able to call this function again
        isOnBadPos = false;
        isOnSpawnablePos = false;
        boxColliderOfThisObject.enabled = false;
        if(didItSpawn==true)
        {  
            //deactivate MiniMap
            smallArena.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if we are spawning a Trap
        if(towerToSpawn.CompareTag("Trap"))
        {
            //if its inside a bad position tell the code that it is
            if (other.CompareTag("CantSpawnTower"))
            {
                //if we are inside a road, say that the pos is bad, and then on the checker i check if is a trap, if it is, it will spawn
                if (other.gameObject.layer == 6)
                {
                    
                    isOnBadPos = true;
                }
            }
            //if is iside a spawnable pos tell the code that it is
            if (other.CompareTag("CanSpawnTower"))
            {
                isOnSpawnablePos = true;
            }
        }
        else
        {
            //if its inside a bad position tell the code that it is
            if (other.CompareTag("CantSpawnTower"))
            {
                isOnBadPos = true;
            }
            //if is iside a spawnable pos tell the code that it is
            if (other.CompareTag("CanSpawnTower"))
            {
                isOnSpawnablePos = true;
            }
        }

    }

}
