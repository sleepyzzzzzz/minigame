using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Trampoline
{
    /// <summary>允许弹跳的gameobject的tag的枚举
    /// 
    /// </summary>
    public enum TramType
    {
        //Player,
        basketball
    }

    public class Trampoline : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckTag(collision.gameObject.tag))
            {
                Vector3 m_preVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;//上一帧速度
                ContactPoint2D contactPoint = collision.contacts[0];
                Vector3 newDir = Vector3.zero;
                Vector3 curDir = transform.TransformDirection(Vector3.forward);
                newDir = Vector3.Reflect(curDir, contactPoint.normal);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
                transform.rotation = rotation;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = newDir.normalized * m_preVelocity.x / m_preVelocity.normalized.x;
            }
        }
        /// <summary>判断碰撞体tag是否在允许传触发弹跳的枚举类型里
        /// 
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool CheckTag(string tagname)
        {
            foreach (TramType tag in Enum.GetValues(typeof(TramType)))
            {
                if (tagname.Equals(tag.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

    }
}

