using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Get the current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Reload the current scene by its index
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}