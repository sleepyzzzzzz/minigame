﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Clock;
using Controller;
using LevelManage;
using Level2Tool;
using wind;

enum Classmate_State
{
    Stay,
    Move,
    Attack,
    Defend
}

public class SecondBoss_Classmate : MonoBehaviour
{
    //自身组件
    private UnityArmatureComponent AnimComponent;
    private DragonBones.AnimationState AnimState;
    private UnityEngine.Transform PlayerTransfrom;//玩家位置

    public GameObject BlueBallPrefab;
    public GameObject BlackBallPrefab;
    UnityEngine.Transform go;
    private GameObject ball;
    [Space]
    public float RedZone_speed;
    public float GreenZone_speed;
    private bool over;
    private bool X_generate = false;
    private bool attacking = false;
    public static bool isdefending = false;
    [Space]
    public float RedZone_xmin;
    public float RedZone_xmax;
    public float GreenZone_xmin;
    public float GreenZone_xmax;
    [Space]
    public float AttackFrequency = 2.5f;
    public float ballspeed = 6f;
    private float MoveTargetX;
    private int round_attack = 0;
    private float WaitAttackTimer = 0f;
    private float WaitMove = 3f;
    private float WaitMoveTimer = 0f;
    WindManager windmanager;

    //动画名称
    private static readonly string IdleStr = "待机";
    private static readonly string WalkStr = "行走";
    private static readonly string AttackStr = "踢球";
    private static readonly string DefendStr = "防守";


    Classmate_State State = Classmate_State.Move;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        AnimComponent = GetComponent<UnityArmatureComponent>();
        AnimState = AnimComponent.animation.Play(IdleStr);
        Level2Manager.Instance().ReadyToShoot += ChangeToDefend;
        Level2Manager.Instance().ReadyToShoot += Trigger_Wind2;
        Level2Manager.Instance().ShootSuccess += ChangeToMove;
        Level2Manager.Instance().ShootFailed += ChangeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUp();
        switch (State)
        {
            case Classmate_State.Stay:
                if (AnimState.name != IdleStr)
                {
                    AnimState = AnimComponent.animation.Play(IdleStr);
                }
                break;
            case Classmate_State.Move:
                WaitMoveTimer += Time.deltaTime;
                if (WaitMoveTimer >= WaitMove)
                {
                    if(Mathf.Abs(transform.position.x - MoveTargetX) < 0.1f)
                    {
                        State = Classmate_State.Attack;//到达目标位置，进入攻击状态
                        round_attack = 2;
                    }
                    else
                    {
                        if (!X_generate)
                        {
                            MoveTargetX = Random.Range(RedZone_xmin, RedZone_xmax);
                            X_generate = true;
                        }
                        else
                        {
                            //朝目标移动
                            bool isRight = MoveTargetX > transform.position.x ? true : false;
                            AnimComponent.armature.flipX = isRight;
                            float MoveX = isRight ? RedZone_speed : -RedZone_speed;
                            transform.Translate(new Vector3(MoveX * Time.deltaTime, 0, 0));
                            if (AnimState.name != WalkStr)
                            {
                                AnimState = AnimComponent.animation.Play(WalkStr);
                            }
                        }
                    }
                }
                break;
            case Classmate_State.Attack:
                Trigger_Wind1();
                ////攻击状态下随时更新boss朝向
                AnimComponent.armature.flipX = PlayerTransfrom.position.x > transform.position.x ? true : false;
                WaitAttackTimer += Time.deltaTime;
                if (WaitAttackTimer >= AttackFrequency)
                {
                    if (AnimState.name != AttackStr)
                    {
                        AnimState = AnimComponent.animation.Play(AttackStr);
                        AnimState.timeScale = 0.6f;
                        attacking = true;
                    }
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            Gen_Blue();
                            WaitAttackTimer = 0;
                            break;
                        case 1:
                            Gen_Black();
                            WaitAttackTimer = 0;
                            break;
                    }
                    round_attack--;
                    if (round_attack == 0)
                    {
                        attacking = false;
                        MoveTargetX = Random.Range(RedZone_xmin, RedZone_xmax);
                        WaitMoveTimer = 0f;
                        State = Classmate_State.Move;
                    }
                }
                else
                {
                    if (AnimState.name != IdleStr && !attacking)
                    {
                        AnimState = AnimComponent.animation.Play(IdleStr);
                    }
                }
                break;
            case Classmate_State.Defend:
                DestroyBallWhenDefend("BlueBall");
                DestroyBallWhenDefend("BlackBall");
                if (Mathf.Abs(transform.position.x - MoveTargetX) < 0.1f)
                {
                    AnimComponent.armature.flipX = PlayerTransfrom.position.x > transform.position.x ? true : false;
                    if (AnimState.name != DefendStr)
                    {
                        isdefending = true;
                        AnimState = AnimComponent.animation.Play(DefendStr);
                    }
                }
                else
                {
                    Moving(GreenZone_speed);
                    if (AnimState.name != WalkStr)
                    {
                        AnimState = AnimComponent.animation.Play(WalkStr);
                    }
                }
                break;
        }
    }

    private void Moving(float speed)
    {
        bool isRight = MoveTargetX > transform.position.x ? true : false;
        AnimComponent.armature.flipX = isRight;
        float MoveX = isRight ? speed : -speed;
        transform.Translate(new Vector3(MoveX * Time.deltaTime, 0, 0));
        WaitMoveTimer = 0f;
    }

    private void Gen_Blue()
    {
        Vector3 pos = this.transform.position;
        if (AnimState.name != AttackStr)
        {
            AnimState = AnimComponent.animation.Play(AttackStr);
        }
        UnityEngine.Transform go = Level2Manager.Instance().InstalizeBall(BallType.BlueBall, new Vector3(pos.x, pos.y, pos.z - 2.5f)).transform;
        Adjust_Ball(go);
    }

    private void Gen_Black()
    {
        Vector3 pos = this.transform.position;
        if (AnimState.name != AttackStr)
        {
            AnimState = AnimComponent.animation.Play(AttackStr);
        }
        UnityEngine.Transform go = Level2Manager.Instance().InstalizeBall(BallType.BlackBall, new Vector3(pos.x, pos.y, pos.z - 2.5f)).transform;
        Adjust_Ball(go);
    }

    private void Adjust_Ball(UnityEngine.Transform go)
    {
        go.rotation = Quaternion.LookRotation(PlayerTransfrom.position - transform.position);
        go.Rotate(0, -90, 0);
        go.SetParent(transform);
        Balls balls = go.GetComponent<Balls>();
        balls.Speed = ballspeed;
    }

    private void ChangeToDefend()
    {
        MoveTargetX = Random.Range(GreenZone_xmin, GreenZone_xmax);
        State = Classmate_State.Defend;
    }

    private void ChangeToMove()
    {
        MoveTargetX = Random.Range(RedZone_xmin, RedZone_xmax);
        State = Classmate_State.Move;
        isdefending = false;
    }

    private void DestroyBallWhenDefend(string tag)
    {
        GameObject cur_ball = GameObject.FindGameObjectWithTag("BlueBall");
        if (cur_ball != null)
        {
            Destroy(cur_ball);
        }
    }

    private void Trigger_Wind1()
    {
        if (Level2Manager.ShootSuccessNum == 1 || Level2Manager.ShootSuccessNum == 2)
        {
            if (Random.value < 0.5)
            {
                //触发风
            }
        }
        //if (Random.value < 0.5)
        //{
        //    Debug.Log("aa wind");
        //    //触发风
        //    Vector3 v = new Vector3(-10, 0, 0);
        //    windmanager.CreateWind(v);
        //}
        //else
        //{
        //    windmanager.StopWind();
        //}
    }

    private void Trigger_Wind2()
    {
        if (Level2Manager.ShootSuccessNum == 2)
        {
            if (Random.value < 0.5)
            {
                //触发风
                //Vector3 v = new Vector3(-10, 0, 0);
                //Vector3 w = windmanager.CreateWind(v);
            }
            else
            {
                //windmanager.StopWind();
            }
        }
    }

    private void TimeUp()
    {
        over = Timing.over;
        if (over)
        {
            State = Classmate_State.Stay;
        }
    }
}