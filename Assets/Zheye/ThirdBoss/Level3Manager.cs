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
    public GameObject TipsUI;
    public GameObject[] hpImages;

    private bool ListenKey = false;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        TipsUI.SetActive(true);
        Time.timeScale = 0;
        TipsUI.GetComponentInChildren<Button>().onClick.AddListener(() => { Destroy(TipsUI); Time.timeScale = 1; });
    }

    private void Update()
    {
        if(ListenKey&&Input.anyKeyDown)
        {
            Time.timeScale = 1;
            LoadEndCG();
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3-boss");
        PlayerController.State = Player_State.Alive;
    }

    public void Level3Win()
    {
        StartCoroutine(ImageAlphaAnim(mask, 1, 1.5f));
        WinUI.SetActive(true);
        ListenKey = true;
    }

    public void LoadEndCG()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndCG");
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
