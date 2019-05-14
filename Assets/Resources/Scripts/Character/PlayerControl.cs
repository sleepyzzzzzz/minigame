using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rig;

    private float JumpForce = 30f;
    public float speed = 30f;
    public float crouch_speed = 0.5f;
    public float current_speed = 0f;
    public float horizontal = 0.0f;
    public bool jump = false;
    public bool crouch = false;

    private bool isGround = true;
    public Transform target_pos;
    public float checkr;
    public LayerMask whatisGround;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(target_pos.position, checkr, whatisGround);
        horizontal = Input.GetAxis("Horizontal");
        current_speed = horizontal * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            jump = true;
            rig.velocity = Vector2.up * JumpForce;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        MOVE_AD();
        JUMP();
        CROUCH();
    }

    public void MOVE_AD ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void JUMP ()
    {
        if (isGround && jump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            isGround = false;
        }
    }

    public void CROUCH ()
    {
        current_speed *= crouch_speed;
        rig.velocity *= current_speed;
    }
}