using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField]
    //this will store the object to rotate, like this we can have a tower who have a canon, and the only thing that rotates is the canon, if the tower doesnt have this, it will just get this transform, and rotate this instead
    Transform canonToRotate = null;
    [SerializeField]
    //this will get the bullet that is going to be used to shoot
    GameObject bullet;
    [SerializeField]
    //this is where the bullets will come out of
    GameObject shootarea;
    [SerializeField]
    //this variable will change the behavier of this script to create a laser from the tower to the enemy
    bool isLaserTower = false;
    [SerializeField]
    //this will save the damage to take from enemies
    float damage;
    [SerializeField]
    //this is going to control the rate of fire if i have 3, it will shoot wait 3 seconds and then shoot again
    float rateOfFire=3f;
    //this variable is going to be used for the towers who dont have a lazer, it will basically be false when we shoot, wait seconds and then be true again
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        //if i didnt setup a canon to rotate just say that this is the object that will rotate
        if(canonToRotate==null)
        {
            canonToRotate = this.transform;
        }
    }

    /*************this void will strecth an object from one place to another (lazer), to make this function work i used this websites: 
     * https://answers.unity.com/questions/1172414/scale-and-position-object-between-two-points.html
     * https://answers.unity.com/questions/744735/how-to-make-a-object-face-another-object.html
     * https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
     * https://answers.unity.com/questions/1007585/reading-and-setting-asn-objects-global-scale-with.html
     * then i choose the parts that i needed, tweeked it and its working*/
    void Make_Lazer_Stretch_From_One_Place_To_Another(Transform bullet, Transform shootarea, Transform enemy)
    {
        //activate the bullet gameobject
        bullet.gameObject.SetActive(true);
        //i do this to check if theres any custom hitbox on the enemy
        Transform enemyHitBox;
        //if we find a child with the name Hitbox
        if(enemy.Find("Hitbox"))
        {
            //save the childs transform 
            enemyHitBox = enemy.gameObject.transform.Find("Hitbox");

        }
        else
        {
            //if the enemy doesnt have a custom hitbox say that the enenemyhitbox is the enemy transform
            enemyHitBox = enemy.transform;
        }
        //This will save the center posicion (the midle position between two objects
        Vector3 centerPos = new Vector3(shootarea.transform.position.x + enemyHitBox.transform.position.x, shootarea.transform.position.y + enemyHitBox.transform.position.y, shootarea.transform.position.z + enemyHitBox.transform.position.z) / 2;
        //move the bullet to the midle of the enemy and the shoot area
        bullet.transform.position = centerPos;
        //make the bullet look at the enemy
        bullet.transform.LookAt(new Vector3(enemyHitBox.transform.position.x, enemyHitBox.transform.position.y, enemyHitBox.transform.position.z), transform.up);
        //isto vai dizer a distancia entre  a bala e o inimigo
        float dist = Vector3.Distance(new Vector3(enemyHitBox.transform.position.x, enemyHitBox.transform.position.y, enemyHitBox.transform.position.z), shootarea.transform.position);
        //change the z axi to stretch from the tower to the enemy
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, dist);

        //if the enemy has a enemycontroller
        if (enemy.GetComponent<EnemyController>() == true)
        {
            //decrease enemy hp
            enemy.GetComponent<EnemyController>().hp -= damage * Time.deltaTime;
            if(enemy.GetComponent<EnemyController>().hp <=0)
            {
                bullet.gameObject.SetActive(false);
                //if the tower killed the enemy
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
        //this will make the canon to look at the enemy
        canonToRotate.transform.LookAt(new Vector3(enemyHitBox.transform.position.x, enemyHitBox.transform.position.y, enemyHitBox.transform.position.z), transform.up);
        //if the player can shoot again
        if (canShoot==true)
        {
            //create the bullet at the shoot area position
            Instantiate(bullet, shootarea);
            //tell the bullet that just spawned that the enemy is enemy hitbox
            bullet.gameObject.GetComponent<Bullet>().enemy_=enemyHitBox;

            //if the enemy has a enemycontroller
            if (enemy.GetComponent<EnemyController>() == true)
            {
                //decrease enemy hp
                enemy.GetComponent<EnemyController>().hp -= damage;

                if (enemy.GetComponent<EnemyController>().hp <= 0)
                {    
                    //if the tower killed the enemy
                    colliders.Remove(colliders[0]);
                }
            }

            //then it will do a coroutine that will make the tower wait seconds and then the variable can i shoot gets true again, for us to be able to shoot again
            StartCoroutine(ShootWaitThenShoot(rateOfFire));
        }
    }


    IEnumerator ShootWaitThenShoot(float rateOfFire_)
    {
        //tell the code that you cannot shoot
        canShoot = false;

        //wait rateoffire seconds
        yield return new WaitForSeconds(rateOfFire_);

        //change this bool so that we can shoot again
        canShoot = true;
    }

    //this will save all colliders inside the range of the tower
    List<Collider> colliders = new List<Collider>();
    // Update is called once per frame
    void Update()
    {
        //if theres items on the colliders list
        if(colliders.Count!=0)
        {
            //if the first collider is null remove it and make the bullet not active i do this because if the enemy is deleted by something other then the tower it will be removed from the list aswell
            if (colliders[0] == null)
            {
                colliders.Remove(colliders[0]);
                //if its a lazer tower
                if(isLaserTower==true)
                {
                    bullet.SetActive(false);
                }
            }
        }

        
       
    }

   
    

 
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //if the current collider doesnt exist, add him
            if (!colliders.Contains(other) && other.CompareTag("Enemy")) { colliders.Add(other); }
            //if the first collider is null remove it and make the bullet inactive i do this because if the enemy is deleted by something other then the tower it will be removed from the list aswell
            if (colliders[0] == null)
            {
                colliders.Remove(colliders[0]);
                if (isLaserTower == true)
                {
                    bullet.SetActive(false);
                }
            }

            //if the other has the tag enemy and the collider 0 isnt null 
            if (colliders[0].CompareTag("Enemy") && colliders[0] != null)
            {
                //check if this is a lazer tower if it is call the function that will make the tower work
                if (isLaserTower == true)
                {
                    Make_Lazer_Stretch_From_One_Place_To_Another(bullet.transform, shootarea.transform, colliders[0].transform);
                }
                //if its not an lazer tower the tower needs to shoot bullets
                else
                {
                    //if we can shoot again
                    if (canShoot == true)
                    {
                        //call the function to shoot the bullet
                        ShootBullet(bullet.transform, shootarea.transform, colliders[0].transform);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //remove the collider because he left the trigger
            colliders.Remove(other);
            if (isLaserTower == true)
            {
                //if an enemy leaves the trigger deactivate the bullet
                if (isLaserTower == true)
                {
                    bullet.gameObject.SetActive(false);
                }
            }
        }
    }


}
