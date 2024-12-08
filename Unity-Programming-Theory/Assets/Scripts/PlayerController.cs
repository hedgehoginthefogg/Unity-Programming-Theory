using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float yBounds;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get value for A/D or Left/Right arrows - 0 not pressed, 1 pressed
        transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0, 0); // Move player based on input and speed variable
    }
}
