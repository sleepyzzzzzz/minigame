using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            place_pos.x += 2;
            place_pos.z += 1;
            blue = bd.Initialize(place_pos);
        }
        //放置红门
        if (Input.GetKey(KeyCode.R) && !rd.rdoor_placed)
        {
            Vector3 place_pos = this.transform.position;
            place_pos.x += 2;
            place_pos.z -= 3;
            red = rd.Initialize(place_pos);
        }
        //鼠标右键
        if (Input.GetMouseButtonDown(1))
        {
            if (!rd.rdoor_placed)
            {
                Vector3 place_pos = this.transform.position;
                place_pos.x += 2;
                place_pos.z -= 3;
                red = rd.Initialize(place_pos);
            }
            Vector3 position = Input.mousePosition;
            transform.DetachChildren();
            //抛物线投掷定点
            Vector3 red_pos = red.transform.position;
            position.y = red_pos.y;

            float x = (position.x - red.transform.position.x) * 0.5f;
            red.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, x * 2f) * Mathf.Tan(Mathf.PI * 55 / 180), ForceMode2D.Force);
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