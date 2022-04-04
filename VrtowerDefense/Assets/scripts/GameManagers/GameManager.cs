using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    //this will save the towers parent, so that i can spawn them
    GameObject towerParent;
    [SerializeField]
    //this will save the player hp
    public int hp = 100;
    [SerializeField]
    //this is going to create a connection between the spawn enemys controller script and this script
    MenuController menucontrol;
    [SerializeField]
    //this is going to create a connection between the Money ManagerS script and this script
    MoneyManager moneyManage;
    [SerializeField]
    //this is going to create a connection between the spawn enemys controller script and this script
    SpawnEnemysController spawncontroller;
    [SerializeField]
    //this will say how many enemies can spawn at the begining of a round, it will increase has the rounds go by
    int initialEnemies=10;
    [SerializeField]
    //this will say how many enemies to add per round
    int enemiesToAddPerRound = 10;
    [SerializeField]
    //this will decrease the spawn rate
    float decreaseSpawnRate = 1.05f;
    //this store the currentenemies, its number is changed by the spawnEnemiesController script
    int currentEnemies = 0;
    //this will count the current round
    int currentRound = 0;
    //this bool is going to tell that code that the player lost
    bool playerLost=false;
    //i use this to make the update round finished money to only run once
    bool roundEnded = true;


    //this is called by the moveSpawnTowerPos
    public void SpawnTower(GameObject objectToSpawn_, Transform positionToSpawn_)
    {
        //spawn tower, then change its pos
        GameObject towerSpawned = Instantiate(objectToSpawn_, towerParent.transform);
        towerSpawned.transform.position = positionToSpawn_.localPosition;
        Debug.Log("YES");
    }

    //if this function is called (for example by the enemypaths script) it will say that another enemie has died
    public void ZombieDied()
    {
        //called the function that controlls the money and tell him that a zombie died
        moneyManage.ZombieKilled();
    }

    //this function is called when the round ends, it will say that another round has finished
    public void RoundEnd()
    {
        //called the function that controlls the money and tell him that a Round finished
        moneyManage.RoundFinished();
    }

    //this function is called when the player buys a tower (spawntowerController)
    public void DecreaseMoney(int moneyDecrease)
    {
        //called the function that decreases money
        moneyManage.DecreaseMoney(moneyDecrease);
    }

    //if this function is called (for example by the spawnEnemiesController script) it will say that another enemie has been spawned
    public void SpawnedAnEnemie()
    {
        currentEnemies = currentEnemies + 1;
    }

    //if this function is called (for example by the buttonStartRound script) it will start the next round
    public void NextRound()
    {
        //i put this to false for the roundended to be able to run again
        roundEnded = false;
        //increase the initial enemies for this round to have more enemies
        initialEnemies = initialEnemies + enemiesToAddPerRound;
        //decrease the time to wait per spawn
        spawncontroller.TimeToWaitPerSpawn = spawncontroller.TimeToWaitPerSpawn / decreaseSpawnRate;
        //this will say that the scene now have 0 enemies wich is true because the new round is going to start
        currentEnemies = 0;
        //unpause the round
        spawncontroller.pauseRound = false;

        //this will add 1 to the current round, because we have clicked start next round
        currentRound++;
        //this will call call the function to update the round text counter
        menucontrol.UpdateTxtRoundCounter(currentRound);

        //start the first spawn and then it will begin the loop
        spawncontroller.StartCoroutine(spawncontroller.WaitThenSpawn(spawncontroller.TimeToWaitPerSpawn));
        

    }

    //this will update the hp text
    public void UpdateHpText()
    {
        //call the function to update the hp text
        menucontrol.UpdateTxtHp(hp);
    }

    // Start is called before the first frame update
    void Start()
    {
        //i call the function to update the hp text at the begining aswell, so that it will write the hp of the user since the start
        menucontrol.UpdateTxtHp(hp);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player has 0 hp
        if(hp<=0)
        {
            //call the function that will activate the right buttons to restart the game
            menucontrol.PlayerLost();
            //this will call the function that will destroy every enemy and then it stops the spawning
            spawncontroller.DestroyEveryEnemie();
            playerLost = true;
        }    
        //if the round already spawned every enemie
        if(currentEnemies>=initialEnemies)
        {
            //stop the spawning
            spawncontroller.pauseRound = true;
            //if theres no more enemies on the scene show start next round button, and the player didnt lost the game
            if(spawncontroller.enemiesParent.childCount <= 0 && playerLost==false)
            {
                //if this code hasnt runned yet
                if(roundEnded==false)
                {
                    //i put this to true for this code to only run once
                    roundEnded = true;
                    //this will call the function that will contact money manager to say to him, that the round finished
                    RoundEnd();
                }
                //this will show the start round button, so that the user can go to the next round
                menucontrol.ShowBtnStartRound();

            }
        }
        
    }
}
