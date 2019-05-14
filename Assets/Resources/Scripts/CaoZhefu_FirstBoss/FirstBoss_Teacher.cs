using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TeacherState//boss状态枚举
{
    Move,Attack
}

enum AttackMode//攻击模式枚举
{
    CloseAttck,FarAttack
}


public class FirstBoss_Teacher : MonoBehaviour {

    TeacherState State=TeacherState.Move;//boss状态
    AttackMode attackMode=AttackMode.CloseAttck;//攻击模式

    private float MoveTargetX;//移动目标位置
    private Transform PlayerTransfrom;//玩家位置
    public bool isRedDoor;//这一轮是什么门

    //自身组件
    private SpriteRenderer spriteRenderer;

    //远近距离范围
    public float CloseDistance=4;
    public float FarDistance=8;

    //限定移动边界
    public float MoveXRightLimit=8.35f;
    public float MoveXLeftLimit=-8.35f;
    [Space]
    public float AttackFrequency=3f;//攻击频率
    private float WaitAttackTimer = 0;//攻击间隔计时器
    private int LeftAttackCount = 0;//本轮剩余攻击次数
    public float MoveSpeed=2;//移动速度
    [Space]
    //飞行道具相关属性
    public float BigChalkSize=1.5f;
    public float BigChalkSpeed=5;
    public float SmallChalkSize=1;
    public float SmallChalkSpeed=8;
    public float ThreeSmallChalkAttackFrequency=0.5f;
    public float BookSize=1;
    public float BookThrowAngle=45;
    public float BookThrowPower=200;

    //飞行道具预制体
    public GameObject ChalkPrefab;
    public GameObject BookPrefab;

    void Start () {
        PlayerTransfrom = GameObject.FindGameObjectWithTag(GlobalTags.Player).transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GotoNextTurn();
    }
	
	void Update () {
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
                    spriteRenderer.flipX = PlayerTransfrom.position.x > transform.position.x ? true : false;

                    WaitAttackTimer += Time.deltaTime;
                    if (WaitAttackTimer > AttackFrequency)//计时器计满，开始攻击
                    {
                        //计时器清零，攻击次数减少
                        WaitAttackTimer = 0;
                        LeftAttackCount--;
                        //根据攻击模式处理攻击
                        if (attackMode == AttackMode.CloseAttck)
                        {
                            //近距攻击模式  1，2两种技能随机
                            if(Random.value>0.5f)
                            {
                                RandomSingleChalkAttack();
                            }
                            else
                            {
                                StartCoroutine(ThreeChalkAttack());
                                WaitAttackTimer -= ThreeSmallChalkAttackFrequency * 2;
                            }
                        }
                        else
                        {
                            //远距攻击模式   1,2,3三种技能随机
                            int index = Random.Range(0, 3);
                            switch(index)
                            {
                                case 0:
                                    RandomSingleChalkAttack();break;
                                case 1:
                                    StartCoroutine(ThreeChalkAttack());
                                    WaitAttackTimer -= ThreeSmallChalkAttackFrequency * 2;
                                    break;
                                case 2:
                                    BookAttack();
                                    break;
                            }
                        }
                        
                        if(LeftAttackCount<=0)//本轮攻击结束
                        {
                            GotoNextTurn();
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
                        bool isRight= MoveTargetX > transform.position.x ? true : false;
                        spriteRenderer.flipX = isRight;
                        float MoveX = isRight ? MoveSpeed : -MoveSpeed;
                        transform.Translate(new Vector3(MoveX * Time.deltaTime, 0, 0));
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
        if (Random.value>0.5f)
        {
            isRedDoor = true;
            attackMode = AttackMode.CloseAttck;

            float px = PlayerTransfrom.position.x;
            if (Mathf.Abs(px-transform.position.x)>CloseDistance)
            {
                bool isRight = px > transform.position.x ? true : false;
                MoveTargetX = Mathf.Clamp(isRight ? px - CloseDistance : px + CloseDistance, MoveXLeftLimit, MoveXRightLimit);
                State = TeacherState.Move;
            }
        }
        else
        {
            isRedDoor = false;
            attackMode = AttackMode.FarAttack;

            float px = PlayerTransfrom.position.x;
            if (Mathf.Abs(px - transform.position.x) < FarDistance)
            {
                bool isRight = px > transform.position.x ? true : false;
                MoveTargetX = Mathf.Clamp(isRight ? px - FarDistance : px + FarDistance, MoveXLeftLimit, MoveXRightLimit);
                State = TeacherState.Move;
            }
        }
    }

    //单个粉笔攻击（随机大小）
    private void RandomSingleChalkAttack()
    {
        //生成粉笔并朝向主角
        Transform go = Instantiate(ChalkPrefab,transform.position,Quaternion.LookRotation(PlayerTransfrom.position-transform.position)).transform;
        go.Rotate(0, -90, 0);
        go.SetParent(transform);
        Chalk chalk = go.GetComponent<Chalk>();
        if(isRedDoor)
        {
            if (Random.value > 0.5f)//随机粉笔种类
            {
                chalk.isBigChalk = false;
                go.localScale *= SmallChalkSize;
                chalk.Speed = SmallChalkSpeed;
            }
            else
            {
                chalk.isBigChalk = true;
                go.localScale *= BigChalkSize;
                chalk.Speed = BigChalkSpeed;
            }
        }
        else
        {
            chalk.isBigChalk = false;
            go.localScale *= SmallChalkSize;
            chalk.Speed = SmallChalkSpeed;
        }
    }

    //小粉笔三连击
    private IEnumerator ThreeChalkAttack()
    {
        for (int i = 0; i <= 2; i++)
        {
            Transform go = Instantiate(ChalkPrefab, transform.position, Quaternion.LookRotation(PlayerTransfrom.position - transform.position)).transform;
            go.Rotate(0, -90, 0);
            go.SetParent(transform);
            Chalk chalk = go.GetComponent<Chalk>();
            chalk.isBigChalk = false;
            go.localScale *= SmallChalkSize;
            chalk.Speed = SmallChalkSpeed;
            yield return new WaitForSeconds(ThreeSmallChalkAttackFrequency);
        }
    }

    //练习册攻击
    private void BookAttack()
    {
        Rigidbody2D go = Instantiate(BookPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        //根据角度和力量将物体抛掷出去
        int xDir = PlayerTransfrom.position.x > transform.position.x?1:-1;
        float rate=1f/Mathf.Tan(Mathf.PI * BookThrowAngle / 180f);//计算x向量和y向量比值
        Vector2 dir = new Vector2(xDir*rate, 1);
        go.AddForce(dir*BookThrowPower, ForceMode2D.Force);
    }

}
