using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        }
    }
}
