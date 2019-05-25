using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    Alive,
    Dead
}

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {

        private Rigidbody2D player;
        private Animator player_animator;
        private float speed = 3f;
        private float crouch_speed = 1.5f;
        private float JumpForce = 200f;
        public static bool facing_right = true;
        public static bool facing_left = false;

        private bool walk;
        Player_State State = Player_State.Alive;

        // Use this for initialization
        void Awake()
        {
            player = GetComponent<Rigidbody2D>();
            player_animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
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
        }

        void Move_Anim()
        {
            if (walk)
            {
                player_animator.SetBool("running", true);
                walk = false;
            }
            else
            {
                player_animator.SetBool("running", false);
            }
        }

        void Jump_Anim(float vertical)
        {
            player_animator.SetFloat("verti", vertical);
        }

        void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        bool isGrounded()
        {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            Vector2 start = new Vector2(bounds.center.x, bounds.min.y - bounds.size.y * 0.1f);
            RaycastHit2D hit = Physics2D.Linecast(start, bounds.center);
            return (hit.collider.gameObject != gameObject);
        }
    }
}