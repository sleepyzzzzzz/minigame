using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    Alive,
    Dead
}

public class PlayerController : MonoBehaviour {

    private Rigidbody2D player;
    private Animator player_animator;
    private float speed = 3f;
    private float crouch_speed = 1.5f;
    public float JumpForce;
    private bool facing_right = true;
    private bool facing_left = false;
    //private int crouch_count = 0;

    private bool walk;
    Player_State State = Player_State.Alive;



    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        switch (State)
        {
            case Player_State.Dead:
                player_animator.SetFloat("speed", 0);
                walk = false;
                break;
            case Player_State.Alive:
                Action();
                break;
        }
    }

    void Action()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.A))
        {
            walk = true;
            if (facing_right)
            {
                Flip();
            }
            facing_right = false;
            facing_left = true;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            walk = true;
            if (facing_left)
            {
                Flip();
            }
            facing_right = true;
            facing_left = false;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        Move_Anim();
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            player.AddForce(new Vector2(0, JumpForce));
        }
        Jump_Anim(vertical);
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (crouch_count == 0)
        //    {
        //        BoxCollider2D b = transform.GetComponent<BoxCollider2D>();
        //        transform.GetComponent<BoxCollider2D>().size = new Vector2(b.size.x, b.size.y * 0.5f);
        //        crouch_count += 1;
        //    }
        //    Crouch_Anim(horizontal);
        //}
        //else if (Input.GetKeyUp(KeyCode.S))
        //{
        //    BoxCollider2D b = transform.GetComponent<BoxCollider2D>();
        //    transform.GetComponent<BoxCollider2D>().size = new Vector2(b.size.x, b.size.y * 2f);
        //    player_animator.SetBool("crouch", false);
        //    crouch_count = 0;
        //}
    }

    void Move_Anim()
    {
        if (walk)
        {
            player_animator.SetFloat("speed", Mathf.Abs(2 * speed));
            walk = false;
        }
        else
        {
            player_animator.SetFloat("speed", Mathf.Abs(0 * speed));
        }
    }

    void Jump_Anim(float vertical)
    {
        player_animator.SetFloat("verti", vertical);
    }

    //void Crouch_Anim(float horizontal)
    //{
    //    player_animator.SetBool("crouch", true);
    //    player_animator.SetFloat("speed", Mathf.Abs(horizontal * crouch_speed));
    //}

    void Flip()
    {
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        this.transform.localScale = scale;
    }

    bool isGrounded()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        Vector2 start = new Vector2(bounds.center.x, bounds.min.y - bounds.size.y * 0.1f);
        RaycastHit2D hit = Physics2D.Linecast(start, bounds.center);
        return (hit.collider.gameObject != gameObject);
    }
}