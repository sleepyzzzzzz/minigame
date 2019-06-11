using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1ToLevel2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1—boss");
        }
    }
}
