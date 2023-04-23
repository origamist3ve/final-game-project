using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if 'space' is pressed, restart the level
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // get the current scene name
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            // load the current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
