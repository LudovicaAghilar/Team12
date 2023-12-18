using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int Gummy;
    public float persistentHappiness;
    public float persistentHygiene;
    public float persistentHunger;
    public float[] position;
    public int lastSceneIndex;
    public string lastSceneName;

    public PlayerData(PersistentData gummy)
    {
        Gummy = PersistentData.Gummy;
        persistentHappiness = PersistentData.persistentHappiness;
        persistentHygiene = PersistentData.persistentHygiene;
        persistentHunger = PersistentData.persistentHunger;
        position = new float[3];
        position[0] = gummy.transform.position.x;
        position[1] = gummy.transform.position.y;
        position[2] = gummy.transform.position.z;
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        lastSceneName = SceneManager.GetActiveScene().name;
    }
}
