using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;

public class Level1Manager : MonoBehaviour
{
    private static Level1Manager _instance;

    public static Level1Manager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new Level1Manager();
            }
            return _instance;
        }
    }

    private int hurtCount = 0;
    private int BookCount = 0;

    public Text BookText;

    public Image mask;
    public GameObject WinUI;
    public GameObject TipsUI;

    public GameObject[] hpImages;

    private bool ListenAnyKey = false;
    
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
        if(ListenAnyKey&&Input.anyKeyDown)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        }
    }

    public void PlayerGetHurt()
    {
        hpImages[hurtCount].SetActive(false);
        hurtCount++;
        if (hurtCount >= 3)
        {
            Invoke("Level1LoseAsync", 2f);
            hurtCount = 0;
            BookCount = 0;
        }
    }


    public void GetBook()
    {
        BookCount++;
        BookText.text = BookCount + "/3";
        if (BookCount >= 3) Level1Win();
    }

    public void Level1LoseAsync()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1—boss");
        PlayerController.State = Player_State.Alive;
    }

    public void Level1Win()
    {
        StartCoroutine(ImageAlphaAnim(mask, 1, 1.5f));
        WinUI.SetActive(true);
        ListenAnyKey = true;
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
