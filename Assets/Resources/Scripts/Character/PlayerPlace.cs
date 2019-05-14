using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace : MonoBehaviour {

    public GameObject blue_door;
    public GameObject red_door;

    private void Awake()
    {
        blue_door = GameObject.Find("Blue_door");
        red_door = GameObject.Find("Red_door");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            Instantiate(blue_door, transform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1))
        {
            Instantiate(red_door, transform.position, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(0))
        {
            red_door.transform.parent = this.transform;
            red_door.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = Input.mousePosition;
            red_door.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.DetachChildren();
            Vector3 dir = transform.TransformDirection(position);
            red_door.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        }
    }
}
