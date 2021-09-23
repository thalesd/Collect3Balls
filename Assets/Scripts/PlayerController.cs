using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1.5f, 10f)]
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2();

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (input.magnitude > 0.1)
        {
            transform.Translate(input * speed * Time.deltaTime);
        }
    }
}
