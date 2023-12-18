using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    public static int Gummy = 100;
    public static float persistentHappiness = 100;
    public static float persistentHygiene = 100;
    public static float persistentHunger = 100;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Gummy = data.Gummy;
        persistentHappiness = data.persistentHappiness;
        persistentHygiene = data.persistentHygiene;
        persistentHunger = data.persistentHunger;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        // Load the last scene
        SceneManager.LoadScene(data.lastSceneIndex);
    }

    void Awake()
    {
        // Ensure that this object persists across scenes
        DontDestroyOnLoad(this.gameObject);
    }
}