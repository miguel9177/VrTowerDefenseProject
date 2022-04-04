using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    //this will save all colliders inside the range of the bomb
    List<Collider> colliders = new List<Collider>();

    [SerializeField]
    //this will be the time to wait before exploding
    float secondsBeforeExplode;

    [SerializeField]
    //this will save the explosion object
    GameObject explosion;

    [SerializeField]
    //this will save the damage
    int attack = 100;

    bool isExploding = false;

    
    //if this is a bomb
    public bool isAerialBomb = false;

    //this is used for the aerial bombs to not move on the y axis, i do this because of the spawning of them
    [SerializeField]
    float Ypos = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        //get the initial y value
       // Ypos = this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAerialBomb==true)
        {
            //always stay on the same y pos if is a aerial bomb
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, Ypos, this.transform.localPosition.z);
        }
        
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

        if (other.CompareTag("Enemy"))
        {
            //if the current collider doesnt exist, add him, and is an enemy
            if (!colliders.Contains(other) && other.CompareTag("Enemy")) { colliders.Add(other); }
            //if this object hasnt exploded yet
            if (isExploding == false)
            {
                // it will do a coroutine that will make the bomb wait seconds and then it will explode
                StartCoroutine(ExplodeBomb(secondsBeforeExplode));
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
    IEnumerator ExplodeBomb(float secondsBeforeExplode_)
    {
        //wait rateoffire seconds
        yield return new WaitForSeconds(secondsBeforeExplode_);
        //it will activate the explosion object
        explosion.SetActive(true);
        //vai percorrer todos os colliders
        foreach(Collider i in colliders)
        { 
            //se este collider nao tiver vazio
            if(i!=null)
            {
                //se o objeto tiver um enemy controller diminuir hp
                if(i.gameObject.GetComponent<EnemyController>()==true)
                {
                    i.gameObject.GetComponent<EnemyController>().hp = i.gameObject.GetComponent<EnemyController>().hp - attack;
                }
            }
        }
        //and destroy this object in 0.4 seconds
        Destroy(this.gameObject,0.5f);
    }

    void OnTriggerExit(Collider other)
    {
        //remove the collider because he left the trigger
        colliders.Remove(other);
    }
}
