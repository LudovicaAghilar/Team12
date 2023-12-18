using UnityEngine;
using UnityEngine.UI;

public class DisplayValue : MonoBehaviour
{
    // Reference to the Text component
    private Text textComponent;

    void Start()
    {
        // Get the Text component attached to this GameObject
        textComponent = GetComponent<Text>();

        // Ensure the Text component is found
        if (textComponent == null)
        {
            Debug.LogError("Text component not found on the GameObject.");
        }
    }

    void Update()
    {
        // Update the text to display the current value of myVariable
        if (textComponent != null)
        {
            textComponent.text = "Gummy: " + PersistentData.Gummy.ToString();
        }
    }
}

