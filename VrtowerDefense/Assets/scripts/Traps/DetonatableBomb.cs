using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonatableBomb : MonoBehaviour
{
    //this will save all colliders inside the range of the bomb
    List<Collider> colliders = new List<Collider>();

    [SerializeField]
    //this will save the explosion object
    GameObject explosion;

    [SerializeField]
    //this will save the damage
    int attack = 100;



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

        //if the current collider doesnt exist, add him, and is an enemy
        if (!colliders.Contains(other) && other.CompareTag("Enemy")) { colliders.Add(other); }        

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

    //this will explode the bomb, and is called by the detonator
    public void ExplodeC4()
    {
        //it will activate the explosion object
        explosion.SetActive(true);
        //vai percorrer todos os colliders
        foreach (Collider i in colliders)
        {
            //se este collider nao tiver vazio
            if (i != null)
            {
                //se o objeto tiver um enemy controller diminuir hp
                if (i.gameObject.GetComponent<EnemyController>() == true)
                {
                    i.gameObject.GetComponent<EnemyController>().hp = i.gameObject.GetComponent<EnemyController>().hp - attack;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        //remove the collider because he left the trigger
        colliders.Remove(other);
        
    }
}
