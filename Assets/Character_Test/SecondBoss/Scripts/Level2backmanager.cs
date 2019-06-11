using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BASKETBALL_Manager;

public class Level2backmanager : MonoBehaviour
{

    public float acornsfallspeed;
    public float acornsfallfrequency;
    public float basketballfallspeed;
    [Space]
    public Transform player;
    public float Zone_xmin;
    public float Zone_xmax;
    private GameObject[] all_acorns;
    private GameObject Basketball;
    public Transform Wall;

    // Start is called before the first frame update
    void Start()
    {
        all_acorns = GameObject.FindGameObjectsWithTag("acorns");
        Basketball = GameObject.FindGameObjectWithTag("basketball");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 150 == 0)
        {
            StartCoroutine(AcornsFall());
        }
        BasketBallFall();
    }

    private IEnumerator AcornsFall()
    {
        for (int i = 0; i < all_acorns.Length; i++)
        {
            all_acorns[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, -acornsfallspeed);
            //Destroy(all_acorns[i], 3f);
            yield return new WaitForSeconds(acornsfallfrequency);
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