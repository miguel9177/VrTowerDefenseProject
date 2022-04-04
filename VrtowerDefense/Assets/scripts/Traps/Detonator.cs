using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField]
    //this will save the bomb that is going to be detonated
    DetonatableBomb detonateBomb;

    void OnTriggerEnter(Collider other)
    {
        //if the bullet fired at the this bomb
        if(other.CompareTag("playerbullet"))
        {
            //and destroy this object in 0.4 seconds
            Destroy(this.gameObject, 0.5f);
            detonateBomb.ExplodeC4();
        }
    }
}
