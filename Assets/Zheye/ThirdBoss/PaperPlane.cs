using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : MonoBehaviour
{
    public bool isBigPlane;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                if (!isBigPlane) Level3Manager.Instance.PlayerGetHurt();
            Destroy(gameObject); break;
            case "ground":Destroy(gameObject); break;
            default:break;
        }

    }
}
