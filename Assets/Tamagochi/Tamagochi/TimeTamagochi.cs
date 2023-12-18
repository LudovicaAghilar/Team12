using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTamagochi : MonoBehaviour
{
    private float time = 0;
    private float timer = 0f;
    private float timeIncrementInterval = 60f;
    private int scene25Index = 25;
    private int scene36Index = 36;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == scene25Index)
        {
            // Scene with index 25 is loaded
            time = 0;
        }
        else if (scene.buildIndex == scene36Index)
        {
            // Scene with index 36 is loaded
            DecreaseStats();
        }
    }

    private void Update()
    {
        // Increase time every 60 seconds
        timer += Time.deltaTime;

        if (timer >= timeIncrementInterval)
        {
            time += 1;
            TimeTamagochiStatsManager.Instance.SetTime(time);

            // Debugging statement
            Debug.Log("Time: " + time);

            timer = 0f; // Reset the timer after each increment
        }
    }

    private void DecreaseStats()
    {
        float currentTime = TimeTamagochiStatsManager.Instance.GetTime();
        float happiness = PersistentData.persistentHappiness;
        float hygiene = PersistentData.persistentHygiene;
        float hunger = PersistentData.persistentHunger;

        // Subtract time from other three global variables
        happiness -= currentTime;
        hygiene -= currentTime;
        hunger -= currentTime;

        // Update the persistent values
        PersistentData.persistentHappiness = happiness;
        PersistentData.persistentHygiene = hygiene;
        PersistentData.persistentHunger = hunger;
    }
}
