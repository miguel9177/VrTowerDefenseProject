using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesRestartGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //this will restart the scene i saw this code here https://stackoverflow.com/questions/41644156/unity3d-restart-current-scene/41644224
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
