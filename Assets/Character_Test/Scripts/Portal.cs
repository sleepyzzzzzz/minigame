using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransferManage;
using System;
using Test;

namespace portal
{
   
    public class Portal : MonoBehaviour
    {
        public static TransType type = TransType.Ready;
        private void Awake()
        {

        }
        private void Update()
        {
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("进入碰撞");
            switch (type)
            {
                case TransType.Ready:
                    if (CheckTag(collision.gameObject.tag) && GameObject.FindGameObjectsWithTag(this.tag).Length == 2)
                    {
                        Debug.Log("进入入口门");
                        type = TransType.Transfering;
                        string i = this.tag;
                        this.tag = "Untagged";
                        GameObject target = GameObject.FindGameObjectWithTag(i);
                        collision.gameObject.transform.position = target.transform.position;
                        this.tag = i;
                    }
                    else if (CheckTag(collision.gameObject.tag) && (this.tag == "RedPortal" && GameObject.FindGameObjectsWithTag("BluePortal").Length == 1))
                    {
                        Debug.Log("进入入口门");
                        type = TransType.Transfering;
                        GameObject target = GameObject.FindGameObjectWithTag("BluePortal");
                        collision.gameObject.transform.position = target.transform.position;
                    }
                    else if (CheckTag(collision.gameObject.tag) && (this.tag == "BluePortal" && GameObject.FindGameObjectsWithTag("RedPortal").Length == 1))
                    {
                        Debug.Log("进入入口门");
                        type = TransType.Transfering;
                        GameObject target = GameObject.FindGameObjectWithTag("RedPortal");
                        collision.gameObject.transform.position = target.transform.position;
                    }
                    else
                    {
                        Debug.Log("非法碰撞");
                    }
                    break;
                case TransType.Transfering:
                    Debug.Log("进入出口门");
                    break;
                case TransType.Over:
                    Debug.Log("等待离开出口门");
                    break;


            }
           

        }
        /// <summary>判断碰撞体tag是否在允许传送的枚举类型里
        /// 
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool CheckTag(string tagname)
        {
            foreach (TransTag tag in Enum.GetValues(typeof(TransTag)))
                {
                if(tagname.Equals(tag.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            switch (type)
            {
                case TransType.Ready:
                    Debug.Log("离开入口门");
                    break;
                case TransType.Transfering:
                    Debug.Log("准备离入口门");
                    type = TransType.Over;
                    break;
                case TransType.Over:
                    Debug.Log("离开出口门，传送结束");
                    type = TransType.Ready;
                    break;

            }

        }

    }
}

