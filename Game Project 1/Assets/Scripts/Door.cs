using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;
    private int players_entered = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // destroy the player
            Destroy(collision.gameObject);
            players_entered++;
            if (players_entered == 2)
            {
                SceneManager.LoadScene(nextScene);
            }

        }
    }
}
