using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class SpawnBullets : MonoBehaviour
{
    [SerializeField]
    //this will get the bullet game object
    GameObject bullet;
    //this will controll the fire rate
    bool canShoot = true;

    //this two will make it possible for us to know if the right controller trigger was pressed
    public InputActionReference triggerAmount;
    public ActionBasedController rightController;

    void Start()
    {
        triggerAmount.action.performed += TriggerPressedDown;
        triggerAmount.action.canceled += TriggerPressedDown;
    }
    //this will get called when the trigger is pressed
    void TriggerPressedDown(InputAction.CallbackContext context)
    {
        //get how much of the trigger was pressed
        float triggerValue = context.ReadValue<float>();
        Debug.Log("Left trigger value: " + triggerValue);
        //if the trigger was clicked more then 0.5
        if (triggerValue > 0.5f)
        {
            //if the player can shoot (this is what controls the fire rate
            if (canShoot == true)
            {
                //send a haptic impulse (controller vibration)
                rightController.SendHapticImpulse(0.5f, 0.1f);
            
                //start couroutine to shoot bullet
                StartCoroutine(FireRateControl());
            }
        }
           
    }

    //this function will make the bullet spawn
    public void ShootBullet()
    {
        // spawn the bullet
       GameObject bulletSpawned = Instantiate(bullet, this.transform);
        //remove the parent of the bullet so that it doesnt have a parent, if it had the weapon parent, it would move if the weapon moved aswell
        bulletSpawned.transform.parent = null;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator FireRateControl()
    {
        //say that the player cant shoot again
        canShoot = false;
        //spawn bullet
        ShootBullet();
        //yield on a new YieldInstruction that waits for 5 seconds, i do this for the player to not be able to shoot "infinite" bullets
        yield return new WaitForSeconds(0.3f);
        
        //tell the user that he can shoot again
        canShoot = true;

    }
}
