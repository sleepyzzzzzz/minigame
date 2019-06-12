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

        public float ImageFadeTime = 1f;
        public float ImageShowTime = 1f;
        public float SwitchImageTime = 0.8f;
        public float ImageExistTime = 1f;
        private Image mask;

        void Start()
        {
            mask = transform.Find("mask").GetComponent<Image>();
            CGImages = transform.Find("CGAnim").GetComponentsInChildren<Image>();
            StartCoroutine(ShowStartCG());
        }


        IEnumerator ShowStartCG()
        {
            for (int i = 0; i < CGImages.Length; i++)
            {
                StartCoroutine(ImageAlphaAnim(CGImages[i], 1, ImageShowTime));
                yield return new WaitForSeconds(ImageShowTime + ImageExistTime + SwitchImageTime);
            }
            StartCoroutine(ImageAlphaAnim(mask, 1, ImageFadeTime));
            yield return new WaitForSeconds(ImageFadeTime);
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
    }
}

