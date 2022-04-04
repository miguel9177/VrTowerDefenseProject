using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    //this will be changed when the tower attack instatiate this object
    public Transform enemy_;
    [SerializeField]
    //this will be the bullet speed
    int speed = 30;
    // Update is called once per frame
    void Update()
    {
        if(enemy_!=null)
        {
            //call the function to move the bullet
            MoveBulletToEnemy(enemy_);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //this will move the bullet and destroy the bullet 2 seconds after
    public void MoveBulletToEnemy(Transform enemy)
    {
        
        //i use movetowards to move the bullet to the enemy
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        Destroy(this.gameObject, 2);
    }
    void OnTriggerEnter(Collider other)
    {
        //destroy this object if the bullet collides with something
        Destroy(this.gameObject);
    }
}
