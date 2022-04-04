using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    //this will be the bullet speed
    float bulletSpeed = 30;
    //if this is true, it move the bullet forward
    bool isShooting=false;
    //this will save the damage to give to the enemy
    [SerializeField]
    float damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        //let the code know that the bullet is been fired
        isShooting = true;
        //wait 2 seconds, then destroy
        StartCoroutine(WaitThenDestroy(2f));
    }

    // Update is called once per frame
    void Update()
    {
        //if this is true move the bullet
        if(isShooting==true)
        {
            //move bullet forward
            transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("asdasdasd");
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().hp -= damage;
            //destroy this object if the bullet collides with something
            Destroy(this.gameObject);
        }
      
    }

    // wait 2 then destroy
    private IEnumerator WaitThenDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
