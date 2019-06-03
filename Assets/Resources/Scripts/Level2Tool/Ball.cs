﻿using System;
using System.Collections.Generic;
using UnityEngine;
using LevelManage;

namespace Level2Tool
{
    public enum BallType
    {
        BlueBall,
        BlackBall,
        TechBall
    }
    public class Ball : MonoBehaviour
    {
        public BallType ballType {  set;private get; }
        private void Awake()
        {
            Level2Manager.Instance().HitPortal += new Action(OnHitPortal);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch(collision.gameObject.tag)
            {
                //Todo:这里略啰嗦，因为“碰撞两种传送门”应该是同一件事，但现在为红蓝门定义了两个不同的tag只好写成两个case，以后考虑优化 2019.6.1
                //忽然想起来碰撞传送门这件事应该是transfermanager去处理的，考虑删掉这两个case，2019.6.2
                case "BluePortal":
                    Level2Manager.Instance().ProcessMessage(ActionType.HitPortal);
                    break;
                case "RedPortal":
                    Level2Manager.Instance().ProcessMessage(ActionType.HitPortal);
                    break;
                case "Goal":
                    if(this.ballType==BallType.TechBall)
                    {
                        Level2Manager.Instance().ProcessMessage(ActionType.ShootSuccess);
                    }
                    break;
                case "Player":
                    if(this.ballType==BallType.TechBall)
                    {
                        //Todo:角色进入科技足球
                    }
                    else
                    {
                        Level2Manager.Instance().ProcessMessage(ActionType.HitPlayer);
                    }
                    break;
                default:
                    break;
            }
            

        }
        /// <summary>足球击中传送门后场景的响应
        /// 
        /// </summary>
        private void OnHitPortal()
        {
            Level2Manager.ChargeNum++;
            switch (Level2Manager.ChargeNum)
            {
                case 1://传送门接住足球后为科技足球充能
                    Level2Manager.Instance().ProcessMessage(ActionType.Charge);
                    break;
                case 2://同上
                    Level2Manager.Instance().ProcessMessage(ActionType.Charge);
                    break;
                case 3://充能数为3，通知订阅者做射门准备
                    Level2Manager.Instance().ProcessMessage(ActionType.ReadyToShoot);
                    break;
            }

        }
    }
}


