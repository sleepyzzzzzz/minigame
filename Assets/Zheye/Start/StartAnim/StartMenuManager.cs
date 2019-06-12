using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CZF
{
    public class StartMenuManager : MonoBehaviour
    {
        /// <summary>
        /// 开始界面UI
        /// </summary>
        private GameObject StartAnimUI;
        private Button StartButton;
        //private Button AboutButton;  关于

        /// <summary>
        /// 界面渐隐
        /// </summary>
        private Image mask;
        public float MaskAnimSpeed = 1f;
        private bool IsAniming = false;
        private float TargetMaskAlpha = 0f;

        /// <summary>
        /// StartCG
        /// </summary>
        private Image[] CGImages;
        public string[] CGTexts=new string[4] { "这是第一张图片", "这是第二张图片", "这是第三张图片", "这是第四张图片" };

        public float ImageFadeTime=1f;

        public float WordDivide = 70;//字间距
        public float SingleWordShowTime = 0.2f;//每字显现间隔时间
        public float PhraseShowTime = 1f;
        public float TextFadeTime = 2f;//一行字消失时间
        public float WordStartShowY = 20f;//字开始出现的高度
        public float SwitchLineTime = 0.8f;//每行字切换间隔时间
        public float TextExistTime = 1f;//每行字停留时间

        public Text ShowText;

        //Vector2 StartPos = Vector2.zero;
        //List<Text> texts = new List<Text>();

        private Image mask2;

        void Start()
        {
            StartAnimUI = transform.Find("StartUI").gameObject;
            StartButton = transform.Find("StartUI").Find("StartButton").GetComponent<Button>();
            StartButton.onClick.AddListener(OnStartButtonClick);
            mask = transform.Find("mask").GetComponent<Image>();
            mask2 = transform.Find("mask2").GetComponent<Image>();
            CGImages = transform.Find("CGAnim").GetComponentsInChildren<Image>();
            //StartPos = transform.Find("CGAnim").Find("TextAnim").GetComponent<RectTransform>().anchoredPosition;
            //InitText();
        }


        void Update()
        {
            if(IsAniming)
            {
                mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, 
                Mathf.Lerp(mask.color.a, TargetMaskAlpha, Time.deltaTime * MaskAnimSpeed));
                if(Mathf.Abs(mask.color.a-TargetMaskAlpha)<0.01f)
                {
                    StartAnimUI.SetActive(false);
                    mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, TargetMaskAlpha);
                    IsAniming = false;
                    StartCoroutine(ShowStartCG());
                    StartCoroutine(ShowCGText());
                }
            }
        }

        public void OnStartButtonClick()
        {
            IsAniming = true;
            TargetMaskAlpha = 1f;
        }

        IEnumerator ShowStartCG()
        {
            for (int i = 0; i < CGImages.Length; i++)
            {
                StartCoroutine(ImageAlphaAnim(CGImages[i], 1, PhraseShowTime));
                yield return new WaitForSeconds(PhraseShowTime+TextExistTime+TextFadeTime+SwitchLineTime);   
            }
            StartCoroutine(ImageAlphaAnim(mask2, 1, ImageFadeTime));
        }

        ////初始化文字组件
        //void InitText()
        //{
        //    int maxL = 0;
        //    foreach (var item in CGTexts)
        //    {
        //        if (item.Length > maxL)
        //            maxL = item.Length;
        //    }
        //    for (int i = 0; i < maxL; i++)
        //    {
        //        GameObject go = Instantiate(TextPrefab, transform);
        //        go.GetComponent<RectTransform>().anchoredPosition = StartPos + Vector2.right * WordDivide * i;
        //        texts.Add(go.GetComponent<Text>());
        //    }
        //}


        IEnumerator ImageAlphaAnim(Image image,float EndValue,float time)
        {
            int timer = 0;
            int frameCount = (int)(time / Time.fixedDeltaTime);
            float Step = (EndValue - image.color.a) / frameCount;
            while (timer < frameCount)
            {
                image.color = new Color(image.color.r,image.color.g,image.color.b,image.color.a+Step);
                timer++;
                yield return 0;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, EndValue);
        }

        //开始显示文字的协程
        IEnumerator ShowCGText()
        {
            for (int i = 0; i < CGTexts.Length; i++)
            {
                ShowText.text = CGTexts[i];
                StartCoroutine(TextAlphaAnim(ShowText, 1, PhraseShowTime));
                yield return new WaitForSeconds(PhraseShowTime + TextExistTime);
                StartCoroutine(TextAlphaAnim(ShowText, 0, TextFadeTime));
                yield return new WaitForSeconds(TextFadeTime + SwitchLineTime);   
                //string str = CGTexts[i];
                //for (int j = 0; j < str.Length; j++)
                //{
                //    texts[j].text = str[j].ToString();
                //    texts[j].color = new Color(1, 1, 1, 0);
                //    RectTransform rect = texts[j].GetComponent<RectTransform>();
                //    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, WordStartShowY);
                //    StartCoroutine(TextAlphaAnim(texts[j], 1, SingleWordShowTime));
                //    StartCoroutine(SingleWordMoveDown(rect, StartPos.y, SingleWordShowTime));
                //    yield return new WaitForSeconds(SingleWordShowTime);
                //}
                //yield return new WaitForSeconds(TextExistTime);
                //for (int j = 0; j < str.Length; j++)
                //{
                //    StartCoroutine(TextAlphaAnim(texts[j], 0, TextFadeTime));
                //}
                //yield return new WaitForSeconds(TextFadeTime);
                //yield return new WaitForSeconds(SwitchLineTime);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        //字符移动动画
        IEnumerator SingleWordMoveDown(RectTransform rect, float EndValue, float time)
        {
            int timer = 0;
            int frameCount = (int)(time / Time.fixedDeltaTime);
            float Step = (EndValue - rect.anchoredPosition.y) / frameCount;
            while (timer < frameCount)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + Step);
                timer++;
                yield return 0;
            }
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, EndValue);
        }

        //字符透明度动画
        IEnumerator TextAlphaAnim(Text text, float EndValue, float time)
        {
            int timer = 0;
            int frameCount = (int)(time / Time.fixedDeltaTime);
            float Step = (EndValue - text.color.a) / frameCount;
            while (timer < frameCount)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Step);
                timer++;
                yield return 0;
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, EndValue);
        }
    }
}

