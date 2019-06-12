using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    public string NextSceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
        }
    }
}
