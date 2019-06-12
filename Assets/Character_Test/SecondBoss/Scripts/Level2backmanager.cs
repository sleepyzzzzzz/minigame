using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BASKETBALL_Manager;

public class Level2backmanager : MonoBehaviour
{

    public float acornsfallspeed;
    public float acornsfallfrequency;
    public float basketballfallspeed;
    public float WaitFall = 3f;
    private float WaitFallTimer = 0f;
    public float WaitLoad = 3f;
    private float WaitLoadTimer = 0f;
    private bool round_over;
    private bool load = false;
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
        load = true;
        round_over = false;
        Basketball = GameObject.FindGameObjectWithTag("basketball");
    }

    // Update is called once per frame
    void Update()
    {
        if (load && !round_over)
        {
            WaitFallTimer += Time.deltaTime;
            if (WaitFallTimer >= WaitFall)
            {
                StartCoroutine(AcornsFall());
                WaitFallTimer = 0f;
            }
        }
        else
        {
            WaitLoadTimer += Time.deltaTime;
            if (WaitLoadTimer >= WaitLoad)
            {
                LoadAcorns();
                WaitLoadTimer = 0f;
                round_over = false;
                load = true;
            }
        }
        BasketBallFall();
    }

    private IEnumerator AcornsFall()
    {
        for (int i = 0; i < acorn.Length; i++)
        {
            if (i == (acorn.Length - 1) && acorn[i].destroy)
            {
                round_over = true;
                load = false;
            }
            if (!acorn[i].destroy)
            {
                acorn[i].Speed = acornsfallspeed;
                acorn[i].falling = true;
                yield return new WaitForSeconds(acornsfallfrequency);
            }
        }
    }

    private void LoadAcorns()
    {
        for (int i = 0; i < acorn.Length; i++)
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