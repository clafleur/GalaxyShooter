using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 5.0f;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        //horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        //vertical Movement
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //clamp movement of player on the y axis
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //clamp movement of player on the x axis
        if (transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
        }
    }
}
