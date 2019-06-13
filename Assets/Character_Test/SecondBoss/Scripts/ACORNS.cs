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
        Debug.Log("dsads");
        transform.Translate(0, -Speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
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
