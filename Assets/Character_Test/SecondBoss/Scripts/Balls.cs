using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public float Speed = 5;

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball Collision:" + collision.collider.tag);
        switch (collision.collider.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
            case "ground":
                Destroy(gameObject);
                break;
            case "BluePortal":
                Destroy(gameObject, 0.1f);
                break;
            default:
                break;
        }
    }
}
