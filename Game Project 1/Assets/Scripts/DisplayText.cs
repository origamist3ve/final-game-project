using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public string message = "Default message"; // The message to display
    public float displayTime = 5.0f; // The amount of time the message will be displayed

    private TextMeshProUGUI textComponent; // The text component used to display the message

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>(); // Get the text component from the object this script is attached to
        textComponent.text = message; // Set the initial text to the message inputted in the editor

        Invoke("HideMessage", displayTime); // Invoke the HideMessage function after the specified display time
    }

    void HideMessage()
    {
        Destroy(gameObject); // Destroy the text object
    }
}
