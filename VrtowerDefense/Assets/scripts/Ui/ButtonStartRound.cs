using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartRound : MonoBehaviour
{
    [SerializeField]
    //this is going to save the game manager to a variable
    GameManager manager;

    //if something triggered this object
    private void OnTriggerEnter(Collider other)
    {
        //if the player shot this button start next round
        if(other.tag=="playerbullet")
        {
            Debug.Log("next round");
            //tell the game manager to start the next round
            manager.NextRound();
            //hide this button because it will be active only when we finish another round
            this.gameObject.SetActive(false);
        }
    }

}
