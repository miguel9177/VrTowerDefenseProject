using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrap : MonoBehaviour
{
    //this will save all colliders inside the range of the bomb
    List<Collider> colliders = new List<Collider>();

    [SerializeField]
    //this will be the time to wait before exploding
    float secondsBeforeExplode;

    [SerializeField]
    //this will save the explosion object
    GameObject explosion;

    bool isExploding = false;

    [SerializeField]
    //this will save the speed decrease
    float divideSpeedBy = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        //if theres items on the colliders list
        if (colliders.Count != 0)
        {
            //if the first collider is null remove it
            if (colliders[0] == null)
            {
                colliders.Remove(colliders[0]);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //if the current collider doesnt exist, add him, and is an enemy
            if (!colliders.Contains(other) && other.CompareTag("Enemy")) { colliders.Add(other); }
            //if this object hasnt exploded yet
            if (isExploding == false)
            {
                // it will do a coroutine that will make the bomb wait seconds and then it will explode
                StartCoroutine(ExplodeIceBomb(secondsBeforeExplode));
            }

            //if theres already colliders added
            if (colliders.Count != 0)
            {
                //if the first collider is null remove it 
                if (colliders[0] == null)
                {
                    colliders.Remove(colliders[0]);
                }
            }
        }
    }

    //this will explode the bomb
    IEnumerator ExplodeIceBomb(float secondsBeforeExplode_)
    {
        //wait rateoffire seconds
        yield return new WaitForSeconds(secondsBeforeExplode_);
        //it will activate the explosion object
        explosion.SetActive(true);
        //vai percorrer todos os colliders
        foreach (Collider i in colliders)
        {
            //se este collider nao tiver vazio
            if (i != null)
            {
                //se o objeto tiver um enemy controller diminuir speed
                if (i.gameObject.GetComponent<EnemyController>() == true)
                {
                    i.gameObject.GetComponent<EnemyController>().speed = i.gameObject.GetComponent<EnemyController>().speed / divideSpeedBy;
                }
            }
        }
        //and destroy this object in 0.4 seconds
        Destroy(this.gameObject, 0.5f);
    }

    void OnTriggerExit(Collider other)
    {
        //remove the collider because he left the trigger
        colliders.Remove(other);
    }
}
