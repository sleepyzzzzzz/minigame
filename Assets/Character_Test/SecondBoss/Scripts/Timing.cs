using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManage;

namespace Clock
{
    public class Timing : MonoBehaviour
    {
        public float level_time;
        public Text TimeText;
        public static bool over = false;

        // Start is called before the first frame update
        void Start()
        {
            TimeText.text = string.Format("剩余时间:\n{0:D1}:{1:D2}", (int)level_time / 60, (int)level_time % 60);
            StartCoroutine("Counting");
        }

        private void Update()
        {
            StopTiming();
        }

        private IEnumerator Counting()
        {
            while (level_time > 0)
            {
                yield return new WaitForSeconds(1);
                level_time--;
                TimeText.text = string.Format("剩余时间:\n{0:D1}:{1:D2}", (int)level_time / 60, (int)level_time % 60);
            }
            Debug.Log("over");
            over = true;
        }

        public void StopTiming()
        {
            if (Level2Manager.win)
            {
                over = true;
                StopCoroutine("Counting");
            }
        }
    }
}