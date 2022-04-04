using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  
    //isto vai guardar o gameobject que tem o script dos paths
    [SerializeField]
    public GameObject EnemyPathGameObject;
    //isto vai ficar igual as paths do scritpt enemypath do object EnemyPathGameObject
    Transform[] paths;
    //isto vai guardar em qual path o enimigo vai
    int currentPath=0;

    [SerializeField]
    //this will change the player speed
    public float speed=1;

    [SerializeField]
    //this will store the enemy hp
    public float hp=100;

    [SerializeField]
    //this will store the enemy attack (hp removed from player)
    int attack = 30;

    //this will be used for me to know if the enemy is aerial or not, i access this on spawn enemy controller
    public bool IsAerial=false;

    // Start is called before the first frame update
    void Start()
    {
        //isto vai fazer com que a variavel paths[] fique com todos os paths
        paths = EnemyPathGameObject.gameObject.GetComponent<EnemyPaths>().Paths;
        speed = speed / 100;
    }

    void Move()
    {
        //this will make this object move towards the current path and not change the y axys (because we dont want the enemy to move higher or lower
        this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(paths[currentPath].position.x, this.transform.position.y, paths[currentPath].position.z), speed);
        //this will save the rotation needed for the player to look at path is going
        Quaternion targetRotation = Quaternion.LookRotation(paths[currentPath].position - transform.position);
        //this will rotate the enemy
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.01f);
    }

    
    // Update is called once per frame
    void Update()
    {
        //if the enemy has 0 or less hp call the function die
        if (hp<=0)
        {
            Die();
        }

        if (currentPath >= paths.Length)
        {
            //this will reduce the player hp
            EnemyPathGameObject.GetComponent<EnemyPaths>().ReducePlayerHp(attack);
            //***********adicionar aqui o codigo de tirar vida ao jogador*****************//
            Destroy(this.gameObject);
        }
        else
        {
            //isto vai verificar se o jogador esta o pe do path
            if ((this.transform.position - paths[currentPath].position).sqrMagnitude<3*3)
            { 
                //if the player is close change path
                currentPath = currentPath + 1;
            
            }
        }
        
    }

    void Die()
    {
        //if the enemy has an animation component
        if(this.gameObject.GetComponent<Animation>()==true)
        {
            //this will check if the enemy has the death clip
            if (this.gameObject.GetComponent<Animation>().GetClip("Death"))
            {
                //this will play the animation
                this.gameObject.GetComponent<Animation>().Play("Death");

                //this will call the ienumerator that will make a yield return of the seconds of the death animation
                StartCoroutine(WaitThenKill(this.gameObject.GetComponent<Animation>().GetClip("Death").length));
                return;
            }
            
        }
        //this will call the enemypath script, because that script has a connection to the gamemanager
        EnemyPathGameObject.GetComponent<EnemyPaths>().EnemyDied();
        Destroy(this.gameObject);
       

    }

    IEnumerator WaitThenKill(float TimeToWaitThenKill)
    {
        //wait timetowaitperspawn seconds then do the code below
        yield return new WaitForSeconds(TimeToWaitThenKill);
        //this will call the enemypath script, because that script has a connection to the gamemanager
        EnemyPathGameObject.GetComponent<EnemyPaths>().EnemyDied();
        Destroy(this.gameObject);
        
    }

    private void FixedUpdate()
    {
        if (currentPath < paths.Length)
        {
            Move();
        }
    }
}
