using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace TransferManage
{
    /// <summary>传送门类型枚举
    /// 
    /// </summary>
    public enum PortalType
    {
        Red,
        Blue
    }
    /// <summary>允许被传送的tag的枚举
    /// 
    /// </summary>
    public enum TransTag
    {
        Player,
    }
    /// <summary>角色传送状态枚举
    /// 
    /// </summary>
    public enum TransType
    {
        Ready,
        Transfering,
        Over
    }
    public class TransferManager : MonoBehaviour
    {
        private BlueDoor bd;
        private RedDoor rd;
        public GameObject BlueDoor;
        public GameObject RedDoor;
        private GameObject BlueDoorPrefab;
        private GameObject RedDoorPrefab;
        private GameObject blue;
        private GameObject red;
        private Animator player_animator;
        private bool placeing;
        private bool throwing;
        private bool withdraw;

        private string doorpath = "Prefabs/DoorPrefab";

        /// <summary>传送门资源路径
        /// 
        /// </summary>
        void Awake()
        {
            bd = BlueDoor.GetComponent<BlueDoor>();
            rd = RedDoor.GetComponent<RedDoor>();
            player_animator = GetComponent<Animator>();
            BlueDoorPrefab = Resources.Load(doorpath + "/" + PortalType.Blue.ToString() + "DoorPrefab") as GameObject;
            RedDoorPrefab = Resources.Load(doorpath + "/" + PortalType.Red.ToString() + "DoorPrefab") as GameObject;
        }

        public void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.T) && !bd.bdoor_placed)
            {
                placeing = true;
                Vector3 place_pos = this.transform.position;
                place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
                place_pos.y -= 1.2f;
                blue = bd.Initialize(BlueDoorPrefab, place_pos);
            }
            //放置红门
            if (Input.GetKey(KeyCode.R) && !rd.rdoor_placed)
            {
                placeing = true;
                Vector3 place_pos = this.transform.position;
                place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
                place_pos.y -= 1.2f;
                red = rd.Initialize(RedDoorPrefab, place_pos);
            }
            Place_Anim();
            //鼠标右键
            if (Input.GetMouseButtonDown(1))
            {
                if (!rd.rdoor_placed)
                {
                    Vector3 place_pos = this.transform.position;
                    place_pos.x += 2 * (PlayerController.facing_right ? 1 : -1);
                    red = rd.Initialize(RedDoorPrefab, place_pos);
                }
                throwing = true;
                transform.DetachChildren();
                //抛物线投掷定点
                Vector2 red_pos = red.transform.position;
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = 55 * Mathf.Deg2Rad;
                int direction = position.x > red_pos.x ? 1 : -1;
                Vector2 dirc = new Vector2(direction / Mathf.Tan(angle), 1);
                float dist = Vector2.Distance(position, red_pos);
                float initialV = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * 10 * Mathf.Pow(dist, 2)) / (dist * Mathf.Tan(angle)));
                red.GetComponent<Rigidbody2D>().AddForce(dirc * initialV * 40f, ForceMode2D.Force);
            }
            Throw_Anim();
            //鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                if (bd.bdoor_placed)
                {
                    withdraw = true;
                    Destroy(blue, 0.1f);
                    bd.bdoor_placed = false;
                }
                if (rd.rdoor_placed)
                {
                    withdraw = true;
                    Destroy(red, 0.1f);
                    rd.rdoor_placed = false;
                }
            }
            Withdraw_Anim();
        }

        private void Place_Anim()
        {
            if (placeing && (bd.bdoor_placed || rd.rdoor_placed))
            {
                player_animator.SetBool("place", true);
                placeing = false;
            }
            else
            {
                player_animator.SetBool("place", false);
            }
        }

        private void Throw_Anim()
        {
            if (throwing)
            {
                player_animator.SetBool("throw", true);
                throwing = false;
            }
            else
            {
                player_animator.SetBool("throw", false);
            }
        }

        private void Withdraw_Anim()
        {
            if (withdraw)
            {
                player_animator.SetBool("withdraw", true);
                withdraw = false;
            }
            else
            {
                player_animator.SetBool("withdraw", false);
            }
        }
    }
}




// original
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace TransferManage
//{
//    /// <summary>传送门类型枚举
//    /// 
//    /// </summary>
//    public enum PortalType
//    {
//        Red,
//        Blue
//    }
//    /// <summary>允许被传送的tag的枚举
//    /// 
//    /// </summary>
//    public enum TransTag
//    {
//        Player,
//    }
//    /// <summary>角色传送状态枚举
//    /// 
//    /// </summary>
//    public enum TransType
//    {
//        Ready,
//        Transfering,
//        Over
//    }
//    public class TransferManager : MonoBehaviour
//    {
//        /// <summary>传送门资源路径
//        /// 
//        /// </summary>
//        private string doorpath = "Prefabs/DoorPrefab";
//        void Awake()
//        {

//        }
//        void Update()
//        {
//            ///鼠标左键抬起后，在鼠标所点击的世界坐标生成红门
//            ///Todo:测试用，后期和主角交互的同学对接
//            if (Input.GetMouseButtonUp(0))
//            {
//                Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                Debug.Log(vector);
//                instalize(PortalType.Red, vector);
//            }



//        }
//        /// <summary>
//        /// 根据传入类型在场景中加载传送门
//        /// </summary>
//        /// <param name="type"></param>
//        private void instalize(PortalType type, Vector2 transform)
//        {
//            Debug.Log(type + "门初始化");
//            Instantiate(Resources.Load(doorpath + "/" + type.ToString() + "DoorPrefab"), transform, Quaternion.identity);

//        }
//    }

//}