using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.collider.tag)
        {
            case "ground":
                Destroy(gameObject);
                break;
            case "BluePortal":
            case "RedPortal":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}


