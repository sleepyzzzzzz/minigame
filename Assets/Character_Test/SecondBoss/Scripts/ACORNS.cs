using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACORNS : MonoBehaviour
{
    public float Speed;
    public bool falling = false;
    public bool destroy = false;

    void Update()
    {
        transform.Translate(0, -Speed * Time.deltaTime, 0);
        if (falling)
        {
            Destroy(gameObject, 5f);
            destroy = true;
            falling = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                Destroy(gameObject);
                destroy = true;
                falling = false;
                break;
            case "ground":
                Destroy(gameObject, 0.1f);
                destroy = true;
                falling = false;
                break;
            default:
                break;
        }
    }
}
