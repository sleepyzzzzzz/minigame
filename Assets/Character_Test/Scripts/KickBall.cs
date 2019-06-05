using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class KickBall : MonoBehaviour {

//    private LineRenderer lr;
//    private float dragforce = 10f;
//    private bool drag = false;
//    private Vector3 initialmouseposition;
//    private Vector3 finalMousePosition = Vector3.zero;
//    int i = 0;

//    private void Start()
//    {
//        initialmouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        lr = transform.GetComponent<LineRenderer>();
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            lr = this.transform.GetComponent<LineRenderer>();
//            lr.SetColors(Color.blue, Color.blue);
//            lr.SetWidth(0.2f, 0.2f);
//            i = 0;
//        }
//        if (Input.GetMouseButton(0))
//        {
            //i++;
            //lr.SetVertexCount(i);
            //lr.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));
//        }
//        //if (Input.GetMouseButtonDown(0))
//        //{
//        //    drag = true;
//        //}
//        //if (Input.GetMouseButtonUp(0))
//        //{
//        //    drag = false;
//        //    //lastMousePosition = Vector3.zero;
//        //    float x = initialmouseposition.x - finalMousePosition.x;
//        //    float y = initialmouseposition.y - finalMousePosition.y;
//        //    Vector2 direction = new Vector2(x, y);
//        //    this.transform.GetComponent<Rigidbody2D>().AddForce(direction * dragforce, ForceMode2D.Force);
//        //}
//        //if (drag)
//        //{
//        //    //Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        //    //this.transform.position += offset;
//        //    if (finalMousePosition != Vector3.zero)
//        //    {
//        //        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - finalMousePosition;
//        //        this.transform.position += offset;
//        //    }
//        //    finalMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        //}
//    }


//}


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
            Destroy(lrObj, 0.1f);
            this.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(endpos.x, endpos.y) * 50, ForceMode2D.Force);
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