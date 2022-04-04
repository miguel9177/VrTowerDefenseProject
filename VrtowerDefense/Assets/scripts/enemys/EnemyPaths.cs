using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaths : MonoBehaviour
{
    [SerializeField]
    //this will save the game manager
    GameManager gamemanager;

    public Transform[] Paths;

    //this function will remove the player hp, its called from the enemy that has passed troghout the percorse and reached its destination, i do this here, because its the only gameobject the enemy has a conection with
    public void ReducePlayerHp(int attack)
    {
        //decrease player hp
        gamemanager.hp = gamemanager.hp - attack;
        //call the function that will call the other function to update the text
        gamemanager.UpdateHpText();
    }

    //this function is called by the enemy when he dies
    public void EnemyDied()
    {
        gamemanager.ZombieDied();
    }
}
