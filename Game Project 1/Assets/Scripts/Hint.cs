using UnityEngine;
using UnityEngine.SceneManagement;

public class Hint : MonoBehaviour
{
    /*public tmp text*/
    public TMPro.TextMeshProUGUI player1hints;
    public TMPro.TextMeshProUGUI player2hints;
    public TMPro.TextMeshProUGUI hint_textbox;
    public TMPro.TextMeshProUGUI restart_textbox;

    /* hide hints on start*/
    void Start()
    {
        player1hints.enabled = false;
        player2hints.enabled = false;
        restart_textbox.enabled = false;
        hint_textbox.text = "PRESS 'H' TO DISPLAY CONTROLS";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            player1hints.enabled = !player1hints.enabled;
            player2hints.enabled = !player2hints.enabled;
            restart_textbox.enabled = !restart_textbox.enabled;
            if (player1hints.enabled)
                hint_textbox.text = "PRESS 'H' TO HIDE CONTROLS";
            else
                hint_textbox.text = "PRESS 'H' TO DISPLAY CONTROLS";
        }
    }
}