using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerController : MonoBehaviour
{
    /*
     * This script will only be active when we select a 
     * tower to spawn on the menu, and this tower will
     * be sended to the towertospawn variable
     */

    //this will be changed when we select a tower to buy
    public GameObject TowerToSpawn;
 
    [SerializeField]
    //this will get the movespawntower gameobject
    MoveSpawnTowerPos spawnTowerChecker;
    //this will be changed by the buyTowerMenuBtn
    [HideInInspector]
    public int towerPrice = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void OnTriggerEnter(Collider other)
    {
        
        //if the plaeyr has shooted at this
        if (other.CompareTag("playerbullet"))
        {
            Debug.Log(other.transform.position.x);
            //say to the movespawntower what tower will be spawned
            spawnTowerChecker.towerToSpawn = TowerToSpawn;

            spawnTowerChecker.transform.parent = null;

            //change the position of the movespawntower to the bullet position (i put 0 on x, because the x is always 0, and if this has some lag the movespawntower wont move in the depth  axis, dont forget that this object is sideways)
            spawnTowerChecker.gameObject.transform.position = new Vector3(spawnTowerChecker.transform.position.x, other.transform.position.y, other.transform.position.z);
            //I destroy this because if i would spawn the tower it would collide with the bullet, atleast it does in the testing
            Destroy(other.gameObject);
            spawnTowerChecker.transform.parent = this.transform;
            //call the function that will check if the moveposition is good, if it is spawn the tower
            spawnTowerChecker.CheckIfIsInsideGoodPos(towerPrice);
            
        }
    }
}
