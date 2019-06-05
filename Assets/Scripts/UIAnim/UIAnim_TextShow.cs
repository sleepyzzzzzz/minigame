using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace CZF
{
    public class UIAnim_TextShow : MonoBehaviour
    {
        public float WordDivide = 70;//字间距
        public float SingleWordShowTime = 0.2f;//每字显现间隔时间
        public float TextFadeTime = 2f;//一行字消失时间
        public float WordStartShowY = 20f;//字开始出现的高度
        public float SwitchLineTime = 0.8f;//每行字暂停时间
        public float TextExistTime = 1f;//每行字停留时间

        public GameObject TextPrefab;

        Vector2 StartPos = Vector2.zero;

        public string[] strArray = new string[3] { "这里是动画测试", "只需呼吸", "你为什么这么紧张" };

        List<Text> texts = new List<Text>();


        void Start()
        {
            InitText();
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                string str = strArray[i];
                for (int j = 0; j < str.Length; j++)
                {
                    texts[j].text = str[j].ToString();
                    texts[j].color = new Color(1, 1, 1, 0);
                    RectTransform rect = texts[j].GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, WordStartShowY);
                    StartCoroutine(TextAlphaAnim(texts[j], 1, SingleWordShowTime));
                    StartCoroutine(SingleWordMoveDown(rect, StartPos.y, SingleWordShowTime));
                    yield return new WaitForSeconds(SingleWordShowTime);
                }
                yield return new WaitForSeconds(TextExistTime);
                for (int j = 0; j < str.Length; j++)
                {
                    StartCoroutine(TextAlphaAnim(texts[j], 0, TextFadeTime));
                }
                yield return new WaitForSeconds(TextFadeTime);
                yield return new WaitForSeconds(SwitchLineTime);
            }
        }

        void InitText()
        {
            int maxL = 0;
            foreach (var item in strArray)
            {
                if (item.Length > maxL)
                    maxL = item.Length;
            }
            for (int i = 0; i < maxL; i++)
            {
                GameObject go = Instantiate(TextPrefab, transform);
                go.GetComponent<RectTransform>().anchoredPosition = StartPos + Vector2.right * WordDivide * i;
                texts.Add(go.GetComponent<Text>());
            }
        }


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

