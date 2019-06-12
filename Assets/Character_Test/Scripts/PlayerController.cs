using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    Alive,
    Dead,
}

public enum Collision_Object
{
    SmallChalk,
    BlueBall,
    BlackBall
}

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D player;
        private static Animator player_animator;
        private float speed = 4.5f;
        private float crouch_speed = 1.5f;
        public float JumpForce = 90f;
        public static bool facing_right = true;
        public static bool facing_left = false;

        private int first_chalk_hit_count = 0;
        private int ball_hit_count = 0;
        private int plane_hit_count = 0;

        private bool walk;
        private bool hurt;
        public static Player_State State = Player_State.Alive;
        Collision_Object Collide = Collision_Object.SmallChalk;

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
                case Player_State.Alive:
                    Action();
                    break;
                case Player_State.Dead:
                    player_animator.SetBool("running", false);
                    player_animator.SetBool("hurt", false);
                    player_animator.SetFloat("verti", 0.0f);
                    player_animator.SetBool("fail", true);
                    break;
            }
        }

        void Action()
        {
            float horizontal = Input.GetAxis("Horizontal");
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
            if (Input.GetKey(KeyCode.W) && isGrounded())
            {
                player.AddForce(new Vector2(0, JumpForce));
                player_animator.SetFloat("verti", 1.5f);
            }
            Move_Anim();
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
            Jump_Anim();
        }

        void Jump_Anim()
        {
            if (isGrounded()){
                player_animator.SetFloat("verti", 0.0f);
            }
            else{
                player_animator.SetFloat("verti", 1.5f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case "SmallChalk":
                    first_chalk_hit_count++;
                    hurt = true;
                    break;
                case "BlueBall":
                    ball_hit_count++;
                    hurt = true;
                    break;
                case "BlackBall":
                    ball_hit_count++;
                    hurt = true;
                    break;
                case "Plane":
                    plane_hit_count++;
                    hurt = true;
                    break;
            }
            Hurt_Anim();
            Dead_State(collision.collider.tag);
        }

        private void Hurt_Anim()
        {
            if (hurt)
            {
                player_animator.SetBool("hurt", true);
                hurt = false;
            }
            else
            {
                player_animator.SetBool("hurt", false);
            }
        }

        public static void Got_Hurt(bool hurt_boolean)
        {
            player_animator.SetBool("hurt", hurt_boolean);
        }

        public void Dead_State(string tag)
        {
            switch (tag)
            {
                case "SmallChalk":
                    if (first_chalk_hit_count == 3)
                    {
                        State = Player_State.Dead;
                        if (facing_left) { facing_right = true;facing_left = false; }
                    }
                    break;
                case "BlueBall":
                    if (ball_hit_count == 2)
                    {
                        State = Player_State.Dead;
                    }
                    break;
                case "BlackBall":
                    if (ball_hit_count == 2)
                    {
                        State = Player_State.Dead;
                    }
                    break;
                case "Plane":
                    if(plane_hit_count==3)
                    {
                        State = Player_State.Dead;
                        if (facing_left) { facing_right = true; facing_left = false; }
                    }
                    break;
            }
        }
    }
}