using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BASKETBALL_Manager;

public class Level2backmanager : MonoBehaviour
{

    public float acornsfallspeed;
    public float acornsfallfrequency;
    public float basketballfallspeed;
    private bool round_over = false;
    private bool reload;
    private float WaitFall = 5f;
    private float WaitFallTimer = 0f;
    private float WaitLoad = 5f;
    private float WaitLoadTimer = 0f;
    [Space]
    public Transform player;
    public float Zone_xmin;
    public float Zone_xmax;
    private GameObject[] all_acorns;
    private GameObject Basketball;
    public Transform Wall;
    public ACORNS[] acorn = null;

    private string acornspath = "Prefabs/AcornsPrefab";

    // Start is called before the first frame update
    void Start()
    {
        LoadAcorns();
        Basketball = GameObject.FindGameObjectWithTag("basketball");
    }

    // Update is called once per frame
    void Update()
    {
        WaitFallTimer += Time.deltaTime;
        if (WaitFallTimer >= WaitFall)
        {
            StartCoroutine(AcornsFall());
            WaitFallTimer = 0f;
        }
        if (round_over)
        {
            WaitLoadTimer += Time.deltaTime;
            if (WaitLoadTimer >= WaitLoad)
            {
                if (!reload)
                {
                    LoadAcorns();
                    reload = true;
                    WaitLoadTimer = 0f;
                }
                round_over = false;
            }
        }
        BasketBallFall();
    }


    private IEnumerator AcornsFall()
    {
        for (int i = 0; i < 9; i++)
        {
            acorn[i].Speed = acornsfallspeed;
            acorn[i].falling = true;
            yield return new WaitForSeconds(acornsfallfrequency);
            if (i == acorn.Length - 1 && acorn[i].destroy)
            {
                round_over = true;
                reload = false;
            }
        }
    }

    private void LoadAcorns()
    {
        for (int i = 0; i < 9; i++)
        {
            string name = acornspath + "/橡果" + (i + 1).ToString();
            GameObject one = Resources.Load(name) as GameObject;
            GameObject one_acorns = Instantiate(one);
            acorn[i] = one_acorns.GetComponent<ACORNS>();
        }
    }

    private void BasketBallFall()
    {
        if (player.position.x <= Zone_xmax && player.position.x >= Zone_xmin && BASKETBALL.exist)
        {
            Basketball.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Pow(3, 0.5f), -1) * basketballfallspeed;
            Basketball.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}