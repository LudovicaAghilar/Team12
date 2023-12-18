using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToCity : MonoBehaviour
{
    public void OnPlayButton()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(data.lastSceneIndex);
    }
    public void ResumeGame()
    {
        SumPause.Status = false; // Set pause status to false
    }

}
