﻿using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using wind;
using Level2Tool;
using Controller;

namespace LevelManage
{
    /// <summary>所有事件的枚举类型
    /// 
    /// </summary>
    public enum ActionType
    {
        HitPlayer,
        HitPortal,
        Charge,
        ReadyToShoot,
        ShootSuccess,
        ShootFailed,
    }
    /// <summary>观察者模式，当监听到场景内关键事件发生后广播给所有订阅者
    /// 
    /// </summary>
    public class Level2Manager : UnitySingleton<Level2Manager>
    {
        private bool ListenKey = false;

        /// <summary>传送门累计获取的足球数
        /// 
        /// </summary>
        public static int ChargeNum = 0;
        /// <summary>玩家射门成功的次数
        /// 
        /// </summary>
        public static int ShootSuccessNum = 0;
        /// <summary>第二关道具（除了传送门）的prefab资源路径
        /// 
        /// </summary>
        public static string toolpath = "Prefabs/BallPrefab/";

        /// <summary>击中玩家
        /// 
        /// </summary>
        public event Action HitPlayer;
        /// <summary>击中传送门
        /// 
        /// </summary>
        public event Action HitPortal;
        /// <summary>科技足球充能
        /// 
        /// </summary>
        public event Action Charge;
        /// <summary>冲能完毕准备射门
        /// 
        /// </summary>
        public event Action ReadyToShoot;
        /// <summary>射门成功
        /// 
        /// </summary>
        public event Action ShootSuccess;
        /// <summary>射门失败
        /// 
        /// </summary>
        public event Action ShootFailed;

        public static bool win = false;
        public Image mask;
        public GameObject WinUI;
        public GameObject TipsUI;
        private int hurtCount = 0;
        public GameObject[] hpImages;
        public Text GoalText;

        private void Awake()
        {
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2-boss")
            InstalizeBall(BallType.TechBall, new Vector3(4.88f, 13.85f, -1f));
            TipsUI.SetActive(true);
            Time.timeScale = 0;
            TipsUI.GetComponentInChildren<Button>().onClick.AddListener(() => { Destroy(TipsUI); Time.timeScale = 1; });
        }

        private void FixedUpdate()
        {
            if (ShootSuccessNum == 3)
            {
                Level2BossWin();
            }
            if(ListenKey&&Input.anyKeyDown)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
            }
        }

        /// <summary>发送消息
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void BroadCast(Action target)
        {
            if (target != null)
            {
                target();
            }
        }
        /// <summary>留给外部调用的接口，根据传入的枚举判断广播哪一个事件
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ProcessMessage(ActionType message)
        {
            switch (message)
            {
                case ActionType.HitPlayer:
                    BroadCast(HitPlayer);
                    break;
                case ActionType.HitPortal:
                    BroadCast(HitPortal);
                    break;
                case ActionType.Charge:
                    BroadCast(Charge);
                    break;
                case ActionType.ReadyToShoot:
                    BroadCast(ReadyToShoot);
                    break;
                case ActionType.ShootFailed:
                    BroadCast(ShootFailed);
                    break;
                case ActionType.ShootSuccess:
                    BroadCast(ShootSuccess);
                    break;
                default:
                    break;
            }
        }
        /// <summary>根据传入足球类型和坐标生成足球
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transform"></param>
        public GameObject InstalizeBall(BallType type, Vector3 transform)
        {
            GameObject Ball = Instantiate(Resources.Load(toolpath + type.ToString()), transform, Quaternion.identity) as GameObject;
            Ball.GetComponent<Ball>().ballType = type;
            return Ball;
        }

        private void Level2BossLoseAsync()
        {
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name== "Level2-boss")
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2-boss");
            PlayerController.State = Player_State.Alive;
        }

        public void Level2BossWin()
        {
            win = true;
            StartCoroutine(ImageAlphaAnim(mask, 1, 1.5f));
            WinUI.SetActive(true);
            ListenKey = true;
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

        public void PlayerGetHurt()
        {
            hpImages[hurtCount].SetActive(false);
            hurtCount++;
            if (hurtCount >= 3)
            {
                Invoke("Level2BossLoseAsync", 2f);
                hurtCount = 0;
                ChargeNum = 0;
                ShootSuccessNum = 0;
            }
        }

        public void GoalIn()
        {
            GoalText.text = ShootSuccessNum + "/3";
        }
    }
}