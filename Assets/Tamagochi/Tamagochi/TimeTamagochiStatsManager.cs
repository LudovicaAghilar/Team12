using UnityEngine;

public class TimeTamagochiStatsManager : MonoBehaviour
{
    public static TimeTamagochiStatsManager Instance { get; private set; }

    private float time;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTime(float value)
    {
        time = value;
    }

    public float GetTime()
    {
        return time;
    }
}
