using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManage;

namespace Level2Tool
{
    public enum BallType
    {
        BlueBall,
        BlackBall
    }
    public class Ball : MonoBehaviour
    {
        public BallType ballType {  set;private get; }
        private void Awake()
        {
            //Todo：订阅事件

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch(collision.gameObject.tag)
            {
                //Todo:这里略啰嗦，因为“碰撞两种传送门”应该是同一件事，但现在为红蓝门定义了两个不同的tag只好写成两个case，以后考虑优化 2019.6.1
                case "BluePortal":
                    //Todo:撞击到蓝门的具体实现
                    break;
                case "RedPortal":
                    //Todo:撞击到红门的具体实现
                    break;
                case "Goal":
                    //Todo:撞击到球门的具体实现
                    break;
                case "Player":
                    //Todo:撞击到玩家的具体实现
                default:
                    break;
            }
            

        }
    }
}


