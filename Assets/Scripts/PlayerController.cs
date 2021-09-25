using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float baseSpeed = 5f;
    private float startingLifetime = 3f;

    [Range(1.5f, 10f)]
    public float speed = 5f;

    public float remainingLifetime = 3f;

    public Text Countdown;

    // Start is called before the first frame update
    void Awake()
    {
        speed = baseSpeed;
        remainingLifetime = startingLifetime;
    }

    private void Start()
    {
        Countdown = GameObject.FindGameObjectWithTag("CountdownTimer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GetIsGameRunning())
        {
            return; //blocks everything if game is not running
        }

        Vector2 input = new Vector2();

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (input.magnitude > 0)
        {
            transform.Translate(input * speed * Time.deltaTime);
        }

        if(remainingLifetime <= 0)
        {
            GameManager.instance.EndGame();

            remainingLifetime = 0;
        }
        else
        {
            remainingLifetime -= Time.deltaTime;
        }

        Countdown.text = remainingLifetime.ToString("0.##");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var otherCollider = collision.gameObject.GetComponent<ICollectible>();

        if (otherCollider != null)
        {
            otherCollider.Collect();

            remainingLifetime = startingLifetime;
        }
    }
}
