using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour {

    private Rigidbody2D player;
    private float jumpForce;
    private float speed;

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }

        set
        {
            jumpForce = value;
        }
    }

    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody2D>();
        jumpForce = 500;
        speed = 150;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.AddForce(new Vector2(0, jumpForce));
            // player.MovePosition(new Vector2(player.position.x, player.position.y + speed * Time.deltaTime * 2));
        }

	}
}