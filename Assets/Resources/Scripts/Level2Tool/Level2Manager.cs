using System.Collections;
using System;
using UnityEngine;
using wind;

namespace LevelManage
{
    
    /// <summary>观察者模式，当监听到场景内关键事件发生后广播给所有订阅者
    /// 
    /// </summary>
    public class Level2Manager : UnitySingleton<Level2Manager>
    {
        private void Awake()
        {

        }
        /// <summary>击中玩家
        /// 
        /// </summary>
        private event Action HitPlayer;
        /// <summary>击中传送门
        /// 
        /// </summary>
        private event Action HitPortal;
        /// <summary>足球充能
        /// 
        /// </summary>
        private event Action Charge;
        /// <summary>射门成功
        /// 
        /// </summary>
        private event Action ShootSuccess;
        /// <summary>射门失败
        /// 
        /// </summary>
        private event Action ShootFailed;
        /// <summary>发送消息
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void SendMessage(Action target)
        {
            if(target!=null)
            {
                target();
            }
        }



    }

}

