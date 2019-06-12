using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    public string NextSceneName;
    public AudioClip LevelEndClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if (LevelEndClip != null)
            {
                AudioSource.PlayClipAtPoint(LevelEndClip, transform.position);
                Invoke("go", 1f);
            }
            else go();
        }
    }

    void go()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
    }
}
