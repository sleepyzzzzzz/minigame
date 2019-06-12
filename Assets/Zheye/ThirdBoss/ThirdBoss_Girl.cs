using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;



public class ThirdBoss_Girl : MonoBehaviour
{

    TeacherState State = TeacherState.Move;//boss状态

    private float MoveTargetX;//移动目标位置
    private UnityEngine.Transform PlayerTransfrom;//玩家位置
    public bool isRedDoor;//这一轮是什么门

    //自身组件
    private UnityArmatureComponent AnimComponent;
    private DragonBones.AnimationState AnimState;

    //远近距离范围
    public float CloseDistance = 4;
    public float FarDistance = 8;

    //限定移动边界
    public float MoveXRightLimit = 14f;
    public float MoveXLeftLimit = -15f;
    [Space]
    public float AttackFrequency = 3f;//攻击频率
    private float WaitAttackTimer = 0;//攻击间隔计时器
    private int LeftAttackCount = 0;//本轮剩余攻击次数
    public float MoveSpeed = 2;//移动速度
    [Space]
    //飞行道具相关属性
    public float BigSize=1.5f;
    public float SmallSize = 0.8f;
    public Vector2 ThrowVector = new Vector2(0.8f, 1);
    public float PlaneThrowPower = 400;

    //飞行道具预制体
    public GameObject PaperPlanePrefab;

    //动画名称
    private static readonly string WalkStr = "Walk";
    private static readonly string IdleStr = "Idle";
    private static readonly string ThrowStr = "Throw";

    private float ThrowTimer = 0;
    private bool isThrowing = false;


    void Start()
    {
        PlayerTransfrom = GameObject.FindGameObjectWithTag(GlobalTags.Player).transform;
        AnimComponent = GetComponent<UnityArmatureComponent>();
        GotoNextTurn();
        AnimState = AnimComponent.animation.Play(IdleStr);
    }

    void Update()
    {
        Act();
    }

    //根据当前状态行动
    private void Act()
    {
        switch (State)
        {
            case TeacherState.Attack:
                {
                    //攻击状态下随时更新boss朝向
                    AnimComponent.armature.flipX = PlayerTransfrom.position.x > transform.position.x ? true : false;

                    WaitAttackTimer += Time.deltaTime;
                    if (isThrowing) ThrowTimer += Time.deltaTime;
                    if (ThrowTimer > 1f)
                    {
                        ThrowTimer = 0;
                        isThrowing = false;
                    }
                    if (WaitAttackTimer > AttackFrequency)//计时器计满，开始攻击
                    {
                        if (AnimState.name != ThrowStr)
                        {
                            AnimState = AnimComponent.animation.Play(ThrowStr);
                            isThrowing = true;
                        }
                        //计时器清零，攻击次数减少
                        WaitAttackTimer = 0;
                        LeftAttackCount--;
                        //处理攻击
                        Invoke("Attack", 0.7f);

                        if (LeftAttackCount <= 0)//本轮攻击结束
                        {
                            GotoNextTurn();
                        }
                    }
                    else
                    {
                        if (AnimState.name != IdleStr && !isThrowing)
                        {
                            AnimState = AnimComponent.animation.Play(IdleStr);
                        }
                    }
                }
                break;
            case TeacherState.Move:
                {
                    if (Mathf.Abs(transform.position.x - MoveTargetX) < 0.1f)
                    {
                        State = TeacherState.Attack;//到达目标位置，进入攻击状态
                    }
                    else
                    {
                        //朝目标移动
                        bool isRight = MoveTargetX > transform.position.x ? true : false;
                        AnimComponent.armature.flipX = isRight;
                        float MoveX = isRight ? MoveSpeed : -MoveSpeed;
                        transform.Translate(new Vector3(MoveX * Time.deltaTime, 0, 0));
                        if (AnimState.name != WalkStr)
                        {
                            AnimState = AnimComponent.animation.Play(WalkStr);
                        }
                    }
                }
                break;
        }
    }



    //进入下一轮
    private void GotoNextTurn()
    {
        //随机传送门种类
        //更新攻击方式
        //计算目标位置
        //刷新攻击次数
        LeftAttackCount = 2;
        if (Random.value > 0.5f)
        {
            isRedDoor = true;

            float px = PlayerTransfrom.position.x;
            if (Mathf.Abs(px - transform.position.x) > CloseDistance)
            {
                bool isRight = px > transform.position.x ? true : false;
                MoveTargetX = Mathf.Clamp(isRight ? px - CloseDistance : px + CloseDistance, MoveXLeftLimit, MoveXRightLimit);
                State = TeacherState.Move;
            }
        }
        else
        {
            isRedDoor = false;

            float px = PlayerTransfrom.position.x;
            if (Mathf.Abs(px - transform.position.x) < FarDistance)
            {
                bool isRight = px > transform.position.x ? true : false;
                MoveTargetX = Mathf.Clamp(isRight ? px - FarDistance : px + FarDistance, MoveXLeftLimit, MoveXRightLimit);
                State = TeacherState.Move;
            }
        }
    }

    //单个纸飞机攻击（随机大小）
    private void Attack()
    {
        //生成纸飞机并朝向主角
        UnityEngine.Transform go = Instantiate(PaperPlanePrefab, transform.position, Quaternion.LookRotation(PlayerTransfrom.position - transform.position)).transform;
        go.Rotate(0, -90, 0);
        go.SetParent(transform);
        Rigidbody2D rg = go.GetComponent<Rigidbody2D>();
        //根据角度和力量将物体抛掷出去
        int xDir = PlayerTransfrom.position.x > transform.position.x ? 1 : -1;
        Vector2 vec = ThrowVector * PlaneThrowPower * new Vector2(xDir, 1);
        rg.AddForce(vec, ForceMode2D.Force);
        if (Random.value > 0.5f)//随机纸飞机大小
        {
            go.localScale *= SmallSize;
        }
        else
        {
            go.localScale *= BigSize;
        }
    }

}
