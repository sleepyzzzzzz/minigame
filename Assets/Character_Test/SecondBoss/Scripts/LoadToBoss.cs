using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Placer;

public class LoadToBoss : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2-boss");
        }
    }
}
