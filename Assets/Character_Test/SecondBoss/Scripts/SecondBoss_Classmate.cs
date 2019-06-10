using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Clock;
using Controller;
using LevelManage;
using Level2Tool;

enum Classmate_State
{
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

    //动画名称
    private static readonly string WalkStr = "行走";
    private static readonly string IdleStr = "待机";

    Classmate_State State = Classmate_State.Move;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        AnimComponent = GetComponent<UnityArmatureComponent>();
        AnimState = AnimComponent.animation.Play(IdleStr);
    }

    // Update is called once per frame
    void Update()
    {
        TimeUp();
        switch (State)
        {
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
                break;
            case Classmate_State.Attack:
                //攻击状态下随时更新boss朝向
                AnimComponent.armature.flipX = PlayerTransfrom.position.x > transform.position.x ? true : false;
                if (round_attack != 2)
                {
                    WaitAttackTimer += Time.deltaTime;
                }
                else
                {
                    WaitAttackTimer = AttackFrequency;
                }
                if (WaitAttackTimer >= AttackFrequency)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            Rand_Ball(BlueBallPrefab);
                            WaitAttackTimer = 0;
                            break;
                        case 1:
                            Rand_Ball(BlackBallPrefab);
                            WaitAttackTimer = 0;
                            break;
                    }
                    round_attack--;
                    if (round_attack == 0)
                    {
                        MoveTargetX = Random.Range(RedZone_xmin, RedZone_xmax);
                        State = Classmate_State.Move;
                        WaitMoveTimer = 0f;
                    }
                }
                else
                {
                    if (AnimState.name != IdleStr)
                    {
                        AnimState = AnimComponent.animation.Play(IdleStr);
                    }
                }
                break;
            case Classmate_State.Defend:
                break;
        }
    }

    private void Rand_Ball(GameObject ballprefab)
    {
        UnityEngine.Transform go = Instantiate(ballprefab, transform.position, Quaternion.LookRotation(PlayerTransfrom.position - transform.position)).transform;
        go.Rotate(0, -90, 0);
        go.SetParent(transform);
        Balls balls = go.GetComponent<Balls>();
        balls.Speed = ballspeed;
    }

    private void TimeUp()
    {
        over = Timing.over;
        if (over)
        {
            PlayerController.State = Player_State.Dead;
        }
    }
}