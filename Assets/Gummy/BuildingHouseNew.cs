using UnityEngine;
using System.Collections.Generic;

public class BuildingHouseNew : MonoBehaviour
{
    public GameObject specificGameObject;
    public Vector3[] vector3Positions;
    public int cost;
    int money = PersistentData.Gummy;
    private List<Vector3> occupiedPositions = new List<Vector3>();

    public void OnButtonClick()
    {
        // Ensure there are positions available
        if (vector3Positions.Length == 0)
        {
            Debug.LogWarning("No available positions. Please assign positions in the inspector.");
            return;
        }

        // Check if Gummy is greater than or equal to the cost
        if (money >= cost)
        {
            // Randomly choose a position from the array
            Vector3 selectedPosition = GetRandomAvailablePosition();

            // Check if a valid position is found
            if (selectedPosition != Vector3.zero)
            {
                // Instantiate the specific game object at the selected position
                GameObject newObject = Instantiate(specificGameObject, selectedPosition, Quaternion.identity);

                // Mark the position as occupied
                MarkPositionAsOccupied(selectedPosition);
                DontDestroyOnLoad(newObject);
                // Update the "Gummy" variable and subtract the cost
                PersistentData.Gummy -= cost;
                Debug.Log("Gummy variable updated: " + PersistentData.Gummy);
            }
            else
            {
                Debug.LogWarning("No available positions. All positions are occupied.");
            }
        }
        else
        {
            Debug.LogWarning("Not enough Gummy to build. Need at least " + cost + " Gummy.");
        }
    }

    private Vector3 GetRandomAvailablePosition()
    {
        // Shuffle the array to get a random order
        List<Vector3> shuffledPositions = new List<Vector3>(vector3Positions);
        shuffledPositions.Shuffle();

        // Iterate through the shuffled positions and return the first unoccupied position
        foreach (Vector3 position in shuffledPositions)
        {
            if (!IsPositionOccupied(position))
            {
                return position;
            }
        }

        // Return Vector3.zero if all positions are occupied
        return Vector3.zero;
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Check if the position is in the list of occupied positions
        return occupiedPositions.Contains(position);
    }

    private void MarkPositionAsOccupied(Vector3 position)
    {
        // Add the position to the list of occupied positions
        occupiedPositions.Add(position);
    }
}

// Extension method to shuffle a list
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
