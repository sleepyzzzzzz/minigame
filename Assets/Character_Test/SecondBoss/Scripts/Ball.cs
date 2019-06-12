using System;
using System.Collections.Generic;
using UnityEngine;
using LevelManage;
using Placer;

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
        private int hit_ground_num;

        private void Awake()
        {
            hit_ground_num = 0;
            Level2Manager.Instance().HitPortal += new Action(OnHitPortal);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch(collision.gameObject.tag)
            {
                //Todo:这里略啰嗦，因为“碰撞两种传送门”应该是同一件事，但现在为红蓝门定义了两个不同的tag只好写成两个case，以后考虑优化 2019.6.1
                //忽然想起来碰撞传送门这件事应该是transfermanager去处理的，考虑删掉这两个case，2019.6.2
                case "BluePortal":
                    if (this.ballType == BallType.BlackBall)
                    {
                        Level2Manager.Instance().ProcessMessage(ActionType.HitPortal);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().SetBlueCold();
                    }
                    else if (this.ballType == BallType.BlueBall)
                    {
                        Destroy(gameObject);
                        OnHitPortal();
                    }
                    break;
                case "RedPortal":
                    if (this.ballType == BallType.BlackBall)
                    {
                        Level2Manager.Instance().ProcessMessage(ActionType.HitPortal);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().SetRedCold();
                    }
                    else if (this.ballType == BallType.BlueBall)
                    {
                        Destroy(gameObject);
                        OnHitPortal();
                    }
                    break;
                case "Goal":
                    if(this.ballType == BallType.TechBall)
                    {
                        if (SecondBoss_Classmate.AnimState.name != "防守")
                        {
                            Level2Manager.Instance().ProcessMessage(ActionType.ShootSuccess);
                        }
                        Destroy(gameObject);
                        Level2Manager.Instance().InstalizeBall(BallType.TechBall, new Vector3(4.88f, 13.85f, -1f));
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().enabled = true;
                    }
                    break;
                case "Player":
                    if(this.ballType != BallType.TechBall)
                    {
                        Level2Manager.Instance().ProcessMessage(ActionType.HitPlayer);
                    }
                    break;
                case "ground":
                    if (this.ballType == BallType.TechBall)
                    {
                        hit_ground_num++;
                        if (hit_ground_num == 3)
                        {
                            Level2Manager.Instance().ProcessMessage(ActionType.ShootFailed);
                            Destroy(gameObject);
                            Level2Manager.Instance().InstalizeBall(BallType.TechBall, new Vector3(4.88f, 13.85f, -1f));
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().enabled = true;
                        }
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
            Vector3 size = GameObject.FindGameObjectWithTag("TechBall").transform.localScale;
            Level2Manager.ChargeNum++;
            switch (Level2Manager.ChargeNum)
            {
                case 1://传送门接住足球后为科技足球充能
                    Level2Manager.Instance().ProcessMessage(ActionType.Charge);
                    GameObject.FindGameObjectWithTag("TechBall").transform.localScale = new Vector3(size.x * 1.2f, size.y * 1.2f, size.z * 1.2f);
                    break;
                case 2://同上
                    Level2Manager.Instance().ProcessMessage(ActionType.Charge);
                    GameObject.FindGameObjectWithTag("TechBall").transform.localScale = new Vector3(size.x * 1.5f, size.y * 1.5f, size.z * 1.5f);
                    break;
                case 3://充能数为3，通知订阅者做射门准备
                    Level2Manager.Instance().ProcessMessage(ActionType.ReadyToShoot);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().enabled = false;
                    break;
            }
        }
    }
}