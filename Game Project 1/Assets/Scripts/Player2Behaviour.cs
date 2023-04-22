using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Behaviour : MonoBehaviour
{
    public AudioClip jumpSound;         // Added
    public AudioClip onSound;         // Added

    AudioSource src;         // Added


    public Camera CameraPlayer1;
    public float speed = 5;
    public float jump_force = 5;

    public GameObject magnetCollider;
    private bool magnetActive = true;

    public float rotationSpeed = 90; // Speed of rotation in degrees per second

    // Controls
    private Dictionary<string, KeyCode> keyCodes = new Dictionary<string, KeyCode>();

    private bool grounded = true;
    public float groundedThreshold = 0.7f;
    private int groundedCounter = 0;
    private HashSet<GameObject> groundedObjects = new HashSet<GameObject>();

    private bool aimMode = false;

    void Start()
    {
        src = GetComponent<AudioSource>();      // Added

        keyCodes.Add("up", KeyCode.UpArrow);
        keyCodes.Add("left", KeyCode.LeftArrow);
        keyCodes.Add("down", KeyCode.DownArrow);
        keyCodes.Add("right", KeyCode.RightArrow);
        keyCodes.Add("magnet", KeyCode.RightShift);
        #if UNITY_STANDALONE_OSX
            keyCodes.Add("aim", KeyCode.RightCommand);
        #else
            keyCodes.Add("aim", KeyCode.RightControl);
        #endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        }

        if (Input.GetKey(keyCodes["up"]) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump_force);
        }

        if (Input.GetKey(keyCodes["down"]))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
        }

        if (!aimMode)
        {
            if (Input.GetKey(keyCodes["left"]))
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            }

            if (Input.GetKey(keyCodes["right"]))
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            if (Input.GetKey(keyCodes["left"]))
            {
                magnetCollider.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(keyCodes["right"]))
            {
                magnetCollider.transform.RotateAround(transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }

        if (!(Input.GetKey(keyCodes["left"]) ^ Input.GetKey(keyCodes["right"])))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKeyDown(keyCodes["magnet"]))
        {
            src.PlayOneShot(onSound);       // added
            magnetActive = !magnetActive;
            magnetCollider.SetActive(magnetActive);
        }

        if (Input.GetKeyDown(keyCodes["aim"]))
        {
            aimMode = !aimMode;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > groundedThreshold)
                {
                    if (groundedObjects.Add(collision.gameObject))
                    {
                        groundedCounter++;
                    }
                    break;
                }
            }
        }
        UpdateGroundedStatus();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            if (groundedObjects.Remove(collision.gameObject))
            {
                groundedCounter--;
            }
        }
        UpdateGroundedStatus();
    }

    private void UpdateGroundedStatus()
    {
        grounded = groundedCounter > 0;
    }

}
