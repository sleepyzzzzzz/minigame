using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;

public class Level3Manager : MonoBehaviour
{
    private static Level3Manager _instance;

    public static Level3Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Level3Manager();
            }
            return _instance;
        }
    }

    private int hurtCount = 0;
    private int PlaneCount = 0;

    public Text PlaneText;

    public Image mask;
    public GameObject WinUI;

    public GameObject[] hpImages;

    private void Awake()
    {
        _instance = this;
    }



    public void PlayerGetHurt()
    {
        hpImages[hurtCount].SetActive(false);
        hurtCount++;
        if (hurtCount >= 3)
        {
            Invoke("Level3LoseAsync", 2f);
            hurtCount = 0;
            PlaneCount = 0;
        }
    }


    public void GetPlane()
    {
        PlaneCount++;
        PlaneText.text = PlaneCount + "/3";
        if (PlaneCount >= 3) Level3Win();
    }

    public void Level3LoseAsync()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3—boss");
        PlayerController.State = Player_State.Alive;
    }

    public void Level3Win()
    {
        StartCoroutine(ImageAlphaAnim(mask, 1, 1.5f));
        WinUI.SetActive(true);
    }

    IEnumerator ImageAlphaAnim(Image image, float EndValue, float time)
    {
        int timer = 0;
        int frameCount = (int)(time / Time.fixedDeltaTime);
        float Step = (EndValue - image.color.a) / frameCount;
        while (timer < frameCount)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Step);
            timer++;
            yield return 0;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, EndValue);
    }


}
