using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Book Collision:" + coll.collider.tag);
        switch (coll.collider.tag)
        {
            case "ground":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}


