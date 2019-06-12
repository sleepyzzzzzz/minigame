using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using TransferManage;
using UnityEngine.UI;

public enum Level
{
    Level1,
    Level2,
    Level3
}

namespace Placer
{
    public class PlayerSkill : MonoBehaviour
    {
        private GameObject blue;
        private GameObject red;
        private bool bdoor_placed = false;
        private bool rdoor_placed = false;

        private Animator player_animator;
        private bool placeing;
        private bool throwing;
        private bool withdraw;

        public static Level level = Level.Level1;

        //放置传送门冷却相关，冷却时间内不能放置
        private static float ColdTime = 5f; 
        private float BlueColdTimer = 0;
        private bool BlueisCold = false;
        private float RedColdTimer = 0;
        private bool RedisCold = false;
        public GameObject RedColdImg;
        private Text RedColdText;
        public GameObject BlueColdImg;
        private Text BlueColdText;

        public void SetRedCold() { RedisCold = true;RedColdImg.SetActive(true); }
        public void SetBlueCold() { BlueisCold = true;BlueColdImg.SetActive(true); }


        private void Start()
        {
            player_animator = GetComponent<Animator>();

            if(RedColdImg!=null)RedColdText = RedColdImg.transform.Find("Text").GetComponent<Text>();
            if(BlueColdImg!=null)BlueColdText = BlueColdImg.transform.Find("Text").GetComponent<Text>();
        }

        public void FixedUpdate()
        {
            BasicSkill();
            switch (level)
            {
                case Level.Level1:
                    Level1Skill();
                    break;
                case Level.Level2:
                    Level2Skill();
                    break;
                case Level.Level3:
                    break;
            }
        }

        private void BasicSkill()
        {
            //冷却相关
            if(RedisCold)
            {
                RedColdTimer += Time.fixedDeltaTime;
                RedColdText.text = ((int)(ColdTime-RedColdTimer)).ToString();
                if(RedColdTimer>ColdTime)
                {
                    RedColdTimer = 0;
                    RedisCold = false;
                    RedColdImg.SetActive(false);
                    if(rdoor_placed)rdoor_placed = false;
                }
            }
            if(BlueisCold)
            {
                BlueColdTimer += Time.fixedDeltaTime;
                BlueColdText.text = ((int)(ColdTime-BlueColdTimer)).ToString();
                if (BlueColdTimer > ColdTime)
                {
                    BlueColdTimer = 0;
                    BlueisCold = false;
                    BlueColdImg.SetActive(false);
                    if(bdoor_placed)bdoor_placed = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && !bdoor_placed&&!BlueisCold)
            {
                bdoor_placed = true;
                placeing = true;
                player_animator.SetBool("place", true);
                Vector3 place_pos = this.transform.position;
                place_pos.x += 2.5f * (PlayerController.facing_right ? 1 : -1);
                blue = TransferManager.instalize(PortalType.Blue, place_pos);
            }
            //E键回收
            if (Input.GetKey(KeyCode.E))
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

        private void Level1Skill()
        {
            if (Input.GetMouseButtonDown(1) && !rdoor_placed&&!RedisCold)
            {
                rdoor_placed = true;
                placeing = true;
                player_animator.SetBool("place", true);
                Vector3 place_pos = this.transform.position;
                place_pos.x += 2.5f * (PlayerController.facing_right ? 1 : -1);
                //place_pos.y -= 0.5f;
                red = TransferManager.instalize(PortalType.Red, place_pos);
            }
            Place_Anim();
        }

        private void Level2Skill()
        {
            //鼠标右键
            if (Input.GetMouseButtonDown(1) && !rdoor_placed)
            {
                Vector3 place_pos = this.transform.position;
                place_pos.x += 3.0f * (PlayerController.facing_right ? 1 : -1);
                place_pos.y -= 0.5f;
                red = TransferManager.instalize(PortalType.Red, place_pos);
                red.transform.localScale = new Vector3(1, 1, 1);
                rdoor_placed = true;
                placeing = true;
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
            Place_Anim();
        }

        private void Place_Anim()
        {
            if (placeing && (bdoor_placed || rdoor_placed))
            {
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

        public void Level1DoorSpawn(PortalType type)
        {
            if(type==PortalType.Blue)
            {
                bdoor_placed = true;
                blue = GameObject.FindGameObjectWithTag("BluePortal");
            }
            else
            {
                rdoor_placed = true;
                red = GameObject.FindGameObjectWithTag("RedPortal");
            }
        }
    }
}

