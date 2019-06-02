﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using TransferManage;

namespace Placer
{
    public class PlayerPlace : MonoBehaviour
    {
        private GameObject blue;
        private GameObject red;
        private bool bdoor_placed = false;
        private bool rdoor_placed = false;

        private Animator player_animator;
        private bool placeing;
        private bool throwing;
        private bool withdraw;

        private void Start()
        {
            player_animator = GetComponent<Animator>();
        }

        public void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.T) && !bdoor_placed)
            {
                placeing = true;
                Vector3 place_pos = this.transform.position;
                place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
                place_pos.y -= 1.2f;
                blue = TransferManager.instalize(PortalType.Blue, place_pos);
                bdoor_placed = true;
            }
            //放置红门
            if (Input.GetKey(KeyCode.R) && !rdoor_placed)
            {
                placeing = true;
                Vector3 place_pos = this.transform.position;
                place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
                place_pos.y -= 1.2f;
                red = TransferManager.instalize(PortalType.Red, place_pos);
                rdoor_placed = true;
            }
            Place_Anim();
            //鼠标右键
            if (Input.GetMouseButtonDown(1))
            {
                if (!rdoor_placed)
                {
                    Vector3 place_pos = this.transform.position;
                    place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
                    red = TransferManager.instalize(PortalType.Red, place_pos);
                    rdoor_placed = true;
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
                if (bdoor_placed)
                {
                    withdraw = true;
                    Destroy(blue, 0.1f);
                    bdoor_placed = false;
                }
                if (rdoor_placed)
                {
                    withdraw = true;
                    Destroy(red, 0.1f);
                    rdoor_placed = false;
                }
            }
            Withdraw_Anim();
        }

        private void Place_Anim()
        {
            if (placeing && (bdoor_placed || rdoor_placed))
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
//public class PlayerPlace : MonoBehaviour
//{
//    private GameObject blue;
//    private GameObject red;
//    public static bool bdoor_placed = false;
//    public static bool rdoor_placed = false;

//    private Animator player_animator;
//    private bool placeing;
//    private bool throwing;
//    private bool withdraw;

//    private void Start()
//    {
//        player_animator = GetComponent<Animator>();
//    }

//    public void FixedUpdate()
//    {
//        if (Input.GetKey(KeyCode.T) && !bdoor_placed)
//        {
//            placeing = true;
//            Vector3 place_pos = this.transform.position;
//            place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
//            place_pos.y -= 1.2f;
//            blue = TransferManager.instalize(PortalType.Blue, place_pos);
//            bdoor_placed = true;
//        }
//        //放置红门
//        if (Input.GetKey(KeyCode.R) && !rdoor_placed)
//        {
//            placeing = true;
//            Vector3 place_pos = this.transform.position;
//            place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
//            place_pos.y -= 1.2f;
//            red = TransferManager.instalize(PortalType.Red, place_pos);
//            rdoor_placed = true;
//        }
//        Place_Anim();
//        //鼠标右键
//        if (Input.GetMouseButtonDown(1))
//        {
//            if (!rdoor_placed)
//            {
//                Vector3 place_pos = this.transform.position;
//                place_pos.x += 3 * (PlayerController.facing_right ? 1 : -1);
//                red = TransferManager.instalize(PortalType.Red, place_pos);
//                rdoor_placed = true;
//            }
//            throwing = true;
//            transform.DetachChildren();
//            //抛物线投掷定点
//            Vector2 red_pos = red.transform.position;
//            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            float angle = 55 * Mathf.Deg2Rad;
//            int direction = position.x > red_pos.x ? 1 : -1;
//            Vector2 dirc = new Vector2(direction / Mathf.Tan(angle), 1);
//            float dist = Vector2.Distance(position, red_pos);
//            float initialV = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * 10 * Mathf.Pow(dist, 2)) / (dist * Mathf.Tan(angle)));
//            red.GetComponent<Rigidbody2D>().AddForce(dirc * initialV * 40f, ForceMode2D.Force);
//        }
//        Throw_Anim();
//        //鼠标左键
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (bdoor_placed)
//            {
//                withdraw = true;
//                Destroy(blue, 0.1f);
//                bdoor_placed = false;
//            }
//            if (rdoor_placed)
//            {
//                withdraw = true;
//                Destroy(red, 0.1f);
//                rdoor_placed = false;
//            }
//        }
//        Withdraw_Anim();
//    }

//    private void Place_Anim()
//    {
//        if (placeing && (bdoor_placed || rdoor_placed))
//        {
//            player_animator.SetBool("place", true);
//            placeing = false;
//        }
//        else
//        {
//            player_animator.SetBool("place", false);
//        }
//    }

//    private void Throw_Anim()
//    {
//        if (throwing)
//        {
//            player_animator.SetBool("throw", true);
//            throwing = false;
//        }
//        else
//        {
//            player_animator.SetBool("throw", false);
//        }
//    }

//    private void Withdraw_Anim()
//    {
//        if (withdraw)
//        {
//            player_animator.SetBool("withdraw", true);
//            withdraw = false;
//        }
//        else
//        {
//            player_animator.SetBool("withdraw", false);
//        }
//    }
//}