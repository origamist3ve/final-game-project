using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        }
    }
}