using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    public GameObject lrPrefab;
    private GameObject lrObj;
    private LineRenderer lr;
    private bool draw = false;

    private void Update()
    {
        Vector3 startpos = this.transform.position;
        Vector3 endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            InitializeLine();
            draw = true;
        }
        if (draw)
        {
            lr.SetVertexCount(2);
            lr.SetPosition(0, startpos);
            lr.SetPosition(1, endpos);
        }
        if (Input.GetMouseButtonUp(0) && draw)
        {
            this.transform.GetComponent<Rigidbody2D>().isKinematic = false;
            Destroy(lrObj, 0.1f);
            Vector3 direction = endpos - startpos;
            this.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x, direction.y, direction.z) * 2.5f;
            draw = false;
        }
    }

    private void InitializeLine()
    {
        lrObj = Instantiate(lrPrefab, this.transform);
        lr = lrObj.GetComponent<LineRenderer>();
        lr.SetColors(Color.red, Color.red);
        lr.SetWidth(0.08f, 0.08f);
    }
}