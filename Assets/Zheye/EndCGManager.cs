using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CZF
{
    public class EndCGManager : MonoBehaviour
    {
        /// <summary>
        /// StartCG
        /// </summary>
        private Image[] CGImages;
        public string[] CGTexts = new string[4] { "这是第一张图片", "这是第二张图片", "这是第三张图片", "这是第四张图片" };

        public float ImageFadeTime = 1f;

        public float PhraseShowTime = 1f;
        public float TextFadeTime = 2f;//一行字消失时间
        public float SwitchLineTime = 0.8f;//每行字切换间隔时间
        public float TextExistTime = 1f;//每行字停留时间

        public Text ShowText;
        private Image mask;

        void Start()
        {
            mask = transform.Find("mask").GetComponent<Image>();
            CGImages = transform.Find("CGAnim").GetComponentsInChildren<Image>();
            StartCoroutine(ShowStartCG());
            StartCoroutine(ShowCGText());
        }


        IEnumerator ShowStartCG()
        {
            for (int i = 0; i < CGImages.Length; i++)
            {
                StartCoroutine(ImageAlphaAnim(CGImages[i], 1, PhraseShowTime));
                yield return new WaitForSeconds(PhraseShowTime + TextExistTime + TextFadeTime + SwitchLineTime);
            }
            StartCoroutine(ImageAlphaAnim(mask, 1, ImageFadeTime));
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
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
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
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

