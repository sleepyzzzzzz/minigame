using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BASKETBALL_Manager;
using Controller;

public class Level2backmanager : MonoBehaviour
{
    private static Level2backmanager _instance;

    public static Level2backmanager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Level2backmanager();
            }
            return _instance;
        }
    }

    public float acornsfallspeed;
    public float basketballfallspeed;
    [Space]
    public Transform player;
    public Transform Wall;
    public float Zone_xmin;
    public float Zone_xmax;
    private GameObject[] all_acorns = new GameObject[9];
    [Space]
    public Vector2[] all_values;
    private GameObject Basketball;

    public static bool create1;
    public static bool create2;
    public static bool create3;
    public static bool create4;
    public static bool create5;
    public static bool create6;
    public static bool create7;
    public static bool create8;
    public static bool create9;

    //tutorial
    [Space]
    private int hurtCount = 0;
    public GameObject[] hpImages;
    public GameObject TipsUI;

    private string acornspath = "Prefabs/AcornsPrefab";

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TipsUI.SetActive(true);
        Time.timeScale = 0;
        TipsUI.GetComponentInChildren<Button>().onClick.AddListener(() => { Destroy(TipsUI); Time.timeScale = 1; });
        Basketball = GameObject.FindGameObjectWithTag("basketball");
        create1 = false;
        create2 = false;
        create3 = false;
        create4 = false;
        create5 = false;
        create6 = false;
        create7 = false;
        create8 = false;
        create9 = false;
    }

    // Update is called once per frame
    void Update()
    {
        BasketBallFall();
        if (!create1)
        {
            StartCoroutine(Acorns1());
        }
        if (!create2)
        {
            StartCoroutine(Acorns2());
        }
        if (!create3)
        {
            StartCoroutine(Acorns3());
        }
        if (!create4)
        {
            StartCoroutine(Acorns4());
        }
        if (!create5)
        {
            StartCoroutine(Acorns5());
        }
        if (!create6)
        {
            StartCoroutine(Acorns6());
        }
        if (!create6)
        {
            StartCoroutine(Acorns7());
        }
        if (create8)
        {
            StartCoroutine(Acorns8());
        }
        if (!create9)
        {
            StartCoroutine(Acorns9());
        }
    }

    private IEnumerator Acorns1()
    {
        create1 = true;
        yield return new WaitForSeconds(all_values[0].x);
        string name = acornspath + "/橡果1";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[0] = one_acorns;
        yield return new WaitForSeconds(all_values[0].y);
        ACORNS acorns1 = all_acorns[0].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns2()
    {
        create2 = true;
        yield return new WaitForSeconds(all_values[1].x);
        string name = acornspath + "/橡果2";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[1] = one_acorns;
        yield return new WaitForSeconds(all_values[1].y);
        ACORNS acorns1 = all_acorns[1].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns3()
    {
        create3 = true;
        yield return new WaitForSeconds(all_values[2].x);
        string name = acornspath + "/橡果3";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[2] = one_acorns;
        yield return new WaitForSeconds(all_values[2].y);
        ACORNS acorns1 = all_acorns[2].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns4()
    {
        create4 = true;
        yield return new WaitForSeconds(all_values[3].x);
        string name = acornspath + "/橡果4";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[3] = one_acorns;
        yield return new WaitForSeconds(all_values[3].y);
        ACORNS acorns1 = all_acorns[3].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns5()
    {
        create5 = true;
        yield return new WaitForSeconds(all_values[4].x);
        string name = acornspath + "/橡果5";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[4] = one_acorns;
        yield return new WaitForSeconds(all_values[4].y);
        ACORNS acorns1 = all_acorns[4].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns6()
    {
        create6 = true;
        yield return new WaitForSeconds(all_values[5].x);
        string name = acornspath + "/橡果6";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[5] = one_acorns;
        yield return new WaitForSeconds(all_values[5].y);
        ACORNS acorns1 = all_acorns[5].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns7()
    {
        create7 = true;
        yield return new WaitForSeconds(all_values[6].x);
        string name = acornspath + "/橡果7";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[6] = one_acorns;
        yield return new WaitForSeconds(all_values[6].y);
        ACORNS acorns1 = all_acorns[6].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns8()
    {
        create8 = true;
        yield return new WaitForSeconds(all_values[7].x);
        string name = acornspath + "/橡果8";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[7] = one_acorns;
        yield return new WaitForSeconds(all_values[7].y);
        ACORNS acorns1 = all_acorns[7].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns9()
    {
        create9 = true;
        yield return new WaitForSeconds(all_values[8].x);
        string name = acornspath + "/橡果9";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one, one.transform.position, Quaternion.identity);
        all_acorns[8] = one_acorns;
        yield return new WaitForSeconds(all_values[8].y);
        ACORNS acorns1 = all_acorns[8].GetComponent<ACORNS>();
        acorns1.Speed = acornsfallspeed;
    }

    private void BasketBallFall()
    {
        if (player.position.x <= Zone_xmax && player.position.x >= Zone_xmin && BASKETBALL.exist)
        {
            Basketball.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Pow(3, 0.5f), -1) * basketballfallspeed;
            Basketball.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void Level2LoseAsync()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2")
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        PlayerController.State = Player_State.Alive;
    }

    public void PlayerGetHurt()
    {
        hpImages[hurtCount].SetActive(false);
        hurtCount++;
        if (hurtCount >= 3)
        {
            PlayerController.State = Player_State.Dead;
            Invoke("Level2LoseAsync", 2f);
            hurtCount = 0;
        }
    }
}