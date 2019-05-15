using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D player;

    private float JumpForce = 30f;
    public float speed = 30f;
    public float crouch_speed = 0.5f;
    public float current_speed = 0f;
    public float horizontal = 0.0f;
    public bool jump = false;
    public bool crouch = false;
    private Vector2 ini_state;

    private bool isGround = true;
    public Transform target_pos;
    public float checkr;
    public LayerMask whatisGround;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        ini_state = GetComponent<BoxCollider2D>().size;
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(target_pos.position, checkr, whatisGround);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            jump = true;
            player.AddForce(new Vector2(0, JumpForce));
            isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
            GetComponent<BoxCollider2D>().size = new Vector2(ini_state.x, ini_state.y / 2);
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(ini_state.x, ini_state.y);
        }
    }
}