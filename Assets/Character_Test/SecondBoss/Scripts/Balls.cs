using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
            case "ground":
                Destroy(gameObject);
                break;
            case "BluePortal":
                if(this.tag == "BlueBall")
                {
                    Destroy(gameObject, 0.1f);
                }
                break;
            default:
                break;
        }
    }
}
