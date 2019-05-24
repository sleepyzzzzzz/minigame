using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class PlayerPlace : MonoBehaviour
{
    private BlueDoor bd;
    private RedDoor rd;
    public GameObject BlueDoor;
    public GameObject RedDoor;
    private GameObject blue;
    private GameObject red;

    private void Start()
    {
        bd = BlueDoor.GetComponent<BlueDoor>();
        rd = RedDoor.GetComponent<RedDoor>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.T) && !bd.bdoor_placed)
        {
            Vector3 place_pos = this.transform.position;
            place_pos.x += 2 * (PlayerController.facing_right ? 1 : -1);
            blue = bd.Initialize(place_pos);
        }
        //放置红门
        if (Input.GetKey(KeyCode.R) && !rd.rdoor_placed)
        {
            Vector3 place_pos = this.transform.position;
            place_pos.x += 2 * (PlayerController.facing_right ? 1 : -1);
            red = rd.Initialize(place_pos);
        }
        //鼠标右键
        if (Input.GetMouseButtonDown(1))
        {
            if (!rd.rdoor_placed)
            {
                Vector3 place_pos = this.transform.position;
                place_pos.x += 2 * (PlayerController.facing_right ? 1 : -1);
                red = rd.Initialize(place_pos);
            }
            transform.DetachChildren();
            //抛物线投掷定点
            Vector2 red_pos = red.transform.position;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = 55 * Mathf.Deg2Rad;
            int direction = position.x < red_pos.x ? 1 : -1;
            Vector2 dirc = new Vector2(direction / Mathf.Tan(angle), 1);
            float dist = Vector2.Distance(position, red_pos);
            float initialV = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * 10 * Mathf.Pow(dist, 2)) / (dist * Mathf.Tan(angle)));
            red.GetComponent<Rigidbody2D>().AddForce(dirc * initialV * 40f, ForceMode2D.Force);
        }
        //鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            if (bd.bdoor_placed)
            {
                Destroy(blue, 0.1f);
                bd.bdoor_placed = false;
            }
            if (rd.rdoor_placed)
            {
                Destroy(red, 0.1f);
                rd.rdoor_placed = false;
            }
        }
    }
}