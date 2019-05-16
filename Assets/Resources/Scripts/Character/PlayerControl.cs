using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator player_animator;

    public float JumpForce = 30f;
    public float speed = 30f;
    public float crouch_speed = 0.5f;
    private bool jump = false;
    private bool crouch = false;
    private bool facing_left = false;
    private bool facing_right = true;

    private bool isGround = true;
    public Transform target_pos;
    public float checkr;
    public LayerMask whatisGround;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(target_pos.position, checkr, whatisGround);
        if (Input.GetKey(KeyCode.A))
        {
            if (facing_right)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            facing_right = false;
            facing_left = true;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (facing_left)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            facing_right = true;
            facing_left = false;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            jump = true;
            player_animator.SetBool("Jump", true);
            player.AddForce(new Vector2(0, JumpForce));
            isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
            // GetComponent<BoxCollider2D>().size = new Vector2(ini_state.x, ini_state.y * 0.5f);
            player_animator.SetBool("crouch", true);
            transform.Translate(Vector3.right * crouch_speed * Time.deltaTime);
        }
    }
}