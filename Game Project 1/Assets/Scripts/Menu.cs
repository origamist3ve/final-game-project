using UnityEngine;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
        }
    }
}