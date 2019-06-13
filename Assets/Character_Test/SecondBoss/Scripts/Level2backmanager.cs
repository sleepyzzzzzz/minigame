using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BASKETBALL_Manager;

public class Level2backmanager : MonoBehaviour
{

    public float acornsfallspeed;
    public float basketballfallspeed;
    [Space]
    public float fre_acornsfall1;
    public float fre_acornsgenr1;
    public float fre_acornsfall2;
    public float fre_acornsgenr2;
    public float fre_acornsfall3;
    public float fre_acornsgenr3;
    public float fre_acornsfall4;
    public float fre_acornsgenr4;
    public float fre_acornsfall5;
    public float fre_acornsgenr5;
    public float fre_acornsfall6;
    public float fre_acornsgenr6;
    public float fre_acornsfall7;
    public float fre_acornsgenr7;
    public float fre_acornsfall8;
    public float fre_acornsgenr8;
    public float fre_acornsfall9;
    public float fre_acornsgenr9;
    [Space]
    public Transform player;
    public float Zone_xmin;
    public float Zone_xmax;
    private GameObject[] all_acorns = new GameObject[9];
    private GameObject Basketball;
    public Transform Wall;
    //public ACORNS[] acorn = null;

    private string acornspath = "Prefabs/AcornsPrefab";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Acorns1");
        //StartCoroutine("Acorns2");
        //StartCoroutine("Acorns3");
        //StartCoroutine("Acorns4");
        //StartCoroutine("Acorns5");
        //StartCoroutine("Acorns6");
        //StartCoroutine("Acorns7");
        //StartCoroutine("Acorns8");
        //StartCoroutine("Acorns9");
        //LoadAcorns();
        Basketball = GameObject.FindGameObjectWithTag("basketball");
    }

    // Update is called once per frame
    void Update()
    {
        BasketBallFall();
    }

    private IEnumerator Acorns1()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果1";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns2()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果2";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns3()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果3";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns4()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果4";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns5()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果5";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns6()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果6";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns7()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果7";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns8()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果8";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private IEnumerator Acorns9()
    {
        yield return new WaitForSeconds(fre_acornsgenr1);
        string name = acornspath + "/橡果9";
        GameObject one = Resources.Load(name) as GameObject;
        GameObject one_acorns = Instantiate(one);
        ACORNS acorns1 = one.GetComponent<ACORNS>();
        yield return new WaitForSeconds(fre_acornsfall1);
        acorns1.Speed = acornsfallspeed;
    }

    private void LoadAcorns()
    {
        for (int i = 0; i < all_acorns.Length; i++)
        {
            string name = acornspath + "/橡果" + (i + 1).ToString();
            GameObject one = Resources.Load(name) as GameObject;
            GameObject one_acorns = Instantiate(one);
            all_acorns[i] = one_acorns;
            //acorn[i] = one_acorns.GetComponent<ACORNS>();
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