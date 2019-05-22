using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wind
{
    /// <summary>定义了风力的大小和生成
    /// 
    /// </summary>
    public class WindManager : MonoBehaviour
    {
        private Vector3 wind;
        /// <summary>根据传入的vector写入wind
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Vector3 CreateWind(Vector3 vector)
        {
            wind = vector;
            return vector;
        }
        /// <summary>取消场景内风力作用
        /// 
        /// </summary>
        /// <returns></returns>
        public bool StopWind()
        {
            wind = Vector3.zero;
            return true;
        }
    }
}

