using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CZF
{
    public class StartAnim : MonoBehaviour
    {
        /// <summary>
        /// 开始界面UI
        /// </summary>
        private GameObject StartAnimUI;
        private Button StartButton;

        /// <summary>
        /// 界面渐隐
        /// </summary>
        private Image mask;
        public float MaskAnimSpeed = 1f;
        private bool IsAniming = false;
        private float TargetMaskAlpha = 0f;

        void Start()
        {
            StartAnimUI = transform.Find("StartUI").gameObject;
            StartButton = transform.Find("StartUI").Find("StartButton").GetComponent<Button>();
            StartButton.onClick.AddListener(OnStartButtonClick);
            mask = transform.Find("mask").GetComponent<Image>();
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
                    StartCGAnim();
                }
            }
        }

        public void OnStartButtonClick()
        {
            IsAniming = true;
            TargetMaskAlpha = 1f;
        }

        public void StartCGAnim()
        {

        }
    }
}

