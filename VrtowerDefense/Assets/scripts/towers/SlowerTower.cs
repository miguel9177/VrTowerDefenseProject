using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerTower : MonoBehaviour
{

    [SerializeField]
    //this will get the bullet that is going to be used to shoot
    GameObject bullet;
    [SerializeField]
    //this is where the bullets will come out of
    GameObject shootarea;
    [SerializeField]
    //this will save the damage to take from enemies
    float speedToDivideBy = 2;
    [SerializeField]
    //this is going to control the rate of fire if i have 3, it will shoot wait 3 seconds and then shoot again
    float rateOfFire = 3f;
   

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

    //this will make the tower shoot bullets
    void ShootBullet(Transform bullet, Transform shootarea, Transform enemy)
    {
        //i do this to check if theres any custom hitbox on the enemy
        Transform enemyHitBox;

        //if we find a child with the name Hitbox
        if (enemy.Find("Hitbox"))
        {
            //save the childs transform 
            enemyHitBox = enemy.gameObject.transform.Find("Hitbox");
            
        }
        else
        {
            //if the enemy doesnt have a custom hitbox say that the enenemyhitbox is the enemy transform
            enemyHitBox = enemy.transform;
        }
        
       
            //create the bullet at the shoot area position
            Instantiate(bullet, shootarea);
            //tell the bullet that just spawned that the enemy is enemy hitbox
            bullet.gameObject.GetComponent<Bullet>().enemy_ = enemyHitBox;

            //if the enemy has a enemycontroller
            if (enemy.GetComponent<EnemyController>() == true)
            {
                //decrease enemy speed
                enemy.GetComponent<EnemyController>().speed = enemy.GetComponent<EnemyController>().speed / speedToDivideBy;               
            }

            
            //Rmove the enemy from the script, because it already been hit
            colliders.Remove(colliders[0]);
        
    }




    //this will save all colliders inside the range of the tower
    List<Collider> colliders = new List<Collider>();
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //if the current collider doesnt exist, add him
            if (!colliders.Contains(other) && other.CompareTag("Enemy")) { colliders.Add(other); }
            //if the first collider is null remove it
            if (colliders[0] == null)
            {
                colliders.Remove(colliders[0]);
            }

            //if the other has the tag enemy and the collider 0 isnt null 
            if (colliders[0].CompareTag("Enemy") && colliders[0] != null)
            {
                //call the function to shoot the bullet
                ShootBullet(bullet.transform, shootarea.transform, colliders[0].transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //remove the collider because he left the trigger
            colliders.Remove(other);
        }
    }


}
