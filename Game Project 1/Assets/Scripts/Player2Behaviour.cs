using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2Behaviour : MonoBehaviour
{

    public AudioClip jumpSound;
    public AudioClip onSound;

    AudioSource src;


    public Camera CameraPlayer1;
    public float speed = 5;
    public float jump_force = 5;

    public GameObject magnetCollider;
    private GameObject magnetPivot; // To rotate the magnet around the player without scaling issues
    private bool magnetActive = true;

    public float rotationSpeed = 90; // Speed of rotation in degrees per second

    // Controls
    private Dictionary<string, KeyCode> keyCodes = new Dictionary<string, KeyCode>();

    private bool grounded = true;
    public float groundedThreshold = 0.7f;
    private int groundedCounter = 0;
    private HashSet<GameObject> groundedObjects = new HashSet<GameObject>();

    private bool aimMode = false;


    private Animator animator;
    private bool jumping = false;

    void Start()
    {
        src = GetComponent<AudioSource>();

        keyCodes.Add("up", KeyCode.UpArrow);
        keyCodes.Add("left", KeyCode.LeftArrow);
        keyCodes.Add("down", KeyCode.DownArrow);
        keyCodes.Add("right", KeyCode.RightArrow);
        keyCodes.Add("magnet", KeyCode.RightShift);
        if (Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.OSXEditor)
        {
           keyCodes.Add("aim", KeyCode.RightCommand);
        }
        else
        {
            keyCodes.Add("aim", KeyCode.RightControl);
        }

        magnetActive = !magnetActive;
        magnetCollider.SetActive(magnetActive);

        // Create a new empty GameObject as a child of the player
        magnetPivot = new GameObject("MagnetPivot");
        magnetPivot.transform.SetParent(transform);
        magnetPivot.transform.localPosition = Vector3.zero;
        magnetPivot.transform.localRotation = Quaternion.identity;

        // Make magnetPivot the parent of the magnetCollider
        magnetCollider.transform.SetParent(magnetPivot.transform);


        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(keyCodes["up"]) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump_force);
            jumping = true;
            animator.SetBool("jumping", true);
            src.PlayOneShot(jumpSound);

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
                animator.SetBool("walking", true);
            }

            if (Input.GetKey(keyCodes["right"]))
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                animator.SetBool("walking", true);
            }
        }
        else
        {
            if (Input.GetKey(keyCodes["left"]))
            {
                magnetPivot.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(keyCodes["right"]))
            {
                magnetPivot.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }

        if (!(Input.GetKey(keyCodes["left"]) ^ Input.GetKey(keyCodes["right"])))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("walking", false);
        }

        if (Input.GetKeyDown(keyCodes["magnet"]))
        {
            src.PlayOneShot(onSound);
            magnetActive = !magnetActive;
            animator.SetBool("aiming", magnetActive);
            magnetCollider.SetActive(magnetActive);

            if (!magnetActive)
            {
                aimMode = false;
            }
        }

        if (Input.GetKeyDown(keyCodes["aim"]) && magnetActive)
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
        animator.SetBool("grounded", grounded);
        if (grounded)
        {
            if (jumping)
            {
                jumping = false;
                animator.SetBool("jumping", false);
            }
        }
    }


    private void changeAimState(bool state, Vector2 direction)
    {

    }

}
