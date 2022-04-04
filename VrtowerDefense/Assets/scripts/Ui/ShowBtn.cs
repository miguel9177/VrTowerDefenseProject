using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBtn : MonoBehaviour
{
    [SerializeField]
    //this will store the gameobjects to show
    GameObject[] ShowThis;
    [SerializeField]
    //this will store the gameobjects to hide
    GameObject[] hideThis;

    [SerializeField]
    //this will be true if its a button to buy a tower
    bool isTowerToBuyBtn = false;
   
    private void OnTriggerEnter(Collider other)
    {
        //this will activate every object in here
        foreach (GameObject i in ShowThis)
        {
            i.SetActive(true);
        }
        //this will deactivate every object in here
        foreach ( GameObject i in hideThis)
        {
            i.SetActive(false);
        }
        //if this button is a tower to buy
        if (isTowerToBuyBtn == true)
        {
            //call the function to change the tower to spawn
            this.gameObject.GetComponent<BuyTowerMenuBtn>().SendTowerToSpawner();
        }
        Destroy(other.gameObject);
    }

}