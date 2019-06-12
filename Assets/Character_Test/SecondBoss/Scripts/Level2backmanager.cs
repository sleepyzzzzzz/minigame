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
                LoadAcorns();
                WaitLoadTimer = 0f;
                round_over = false;
            }
        }
        BasketBallFall();
    }


    private IEnumerator AcornsFall()
    {
        for (int j = 0; j < acorn.Length; j++)
        {
            acorn[j].Speed = acornsfallspeed;
            acorn[j].falling = true;
            yield return new WaitForSeconds(acornsfallfrequency);
            if (j == acorn.Length - 1 && acorn[j].destroy)
            {
                round_over = true;
                acorn = null;
                acorn = new ACORNS[9];
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