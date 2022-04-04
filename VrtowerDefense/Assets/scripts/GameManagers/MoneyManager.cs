using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    //this will store the current money of the player, its initial value is the starting player money
    public float currentMoney = 20000f;

    [SerializeField]
    //this is the money won by each zombie kill
    float zombieKilledReward = 100f;

    [SerializeField]
    //this is the money won by each Round Won
    float roundFinishReward = 2000f;

    [SerializeField]
    //this will create a connection between this script and the moneymanager script
    MenuController menucontrol;

    // Start is called before the first frame update
    void Start()
    {
        //at the begining call the function that will write the current money on the txt
        menucontrol.UpdateTxtMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this script is called when a zombie is killed
    public void ZombieKilled()
    {
        //add the reward for killing a zombie
        currentMoney = currentMoney + zombieKilledReward;
        //this will update the money
        menucontrol.UpdateTxtMoney(currentMoney);
    }

    //this script is called when the round finishes
    public void RoundFinished()
    {
        //add the reward for finishing a round
        currentMoney = currentMoney + roundFinishReward;
        //this will update the money
        menucontrol.UpdateTxtMoney(currentMoney);
    }

    //this function is called by the game manager and will be called everytime a player buys a tower or  trap
    public void DecreaseMoney(int moneyToDecrease)
    {
        Debug.Log("MONEY DECREEASED" + moneyToDecrease);
        currentMoney = currentMoney - moneyToDecrease;
        menucontrol.UpdateTxtMoney(currentMoney);
    }
}
