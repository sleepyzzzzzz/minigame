using System.Collections;
using System;
using UnityEngine;
using wind;
using Level2Tool;

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
        ShootFailed
    }
    /// <summary>观察者模式，当监听到场景内关键事件发生后广播给所有订阅者
    /// 
    /// </summary>
    public class Level2Manager : UnitySingleton<Level2Manager>
    {
        /// <summary>传送门累计获取的足球数
        /// 
        /// </summary>
        public static int ChargeNum = 0;
        /// <summary>玩家射门成功的次数
        /// 
        /// </summary>
        private static int ShootSuccessNum = 0;
        /// <summary>第二关道具（除了传送门）的prefab资源路径
        /// 
        /// </summary>
        public static string toolpath = "Prefabs/Level2Prefab";

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
        /// <summary>发送消息
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void BroadCast(Action target)
        {
            if(target!=null)
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
        public void InstalizeBall(BallType type,Vector3 transform)
        {
            
            GameObject Ball = Instantiate(Resources.Load(toolpath + type.ToString()), transform, Quaternion.identity) as GameObject;
            Ball.GetComponent<Ball>().ballType = type;

            
        }


    }

}

