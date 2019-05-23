using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace_old : MonoBehaviour {

    public GameObject BlueDoorPrefab;
    public GameObject RedDoorPrefab;
    public GameObject BlueDoor;
    public GameObject RedDoor;
    private bool bdoor_placed = false;
    private bool rdoor_placed = false;

    // Update is called once per frame
    void Update () {
        // 放置蓝门
        if (Input.GetKey(KeyCode.T) && !bdoor_placed)
        {
            bdoor_placed = true;
            Vector2 place_pos = transform.position;
            place_pos.x += 2;
            BlueDoor = Instantiate(BlueDoorPrefab, place_pos, Quaternion.identity);
        }
        //放置红门
        if (Input.GetKey(KeyCode.R) && !rdoor_placed)
        {
            rdoor_placed = true;
            Vector2 place_pos = transform.position;
            place_pos.x += 2;
            RedDoor = Instantiate(RedDoorPrefab, place_pos, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(1) && rdoor_placed)
        {
            if (!rdoor_placed)
            {
                rdoor_placed = true;
                Vector2 place_pos = transform.position;
                place_pos.x += 2;
                RedDoor = Instantiate(RedDoorPrefab, place_pos, Quaternion.identity);
            }
            Vector3 position = Input.mousePosition;
            RedDoorPrefab.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.DetachChildren();
            Vector3 dir = transform.TransformDirection(position);
            RedDoorPrefab.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Force);
            rdoor_placed = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (bdoor_placed)
            {
                BlueDoor.transform.parent = this.transform;
                Destroy(BlueDoor, 0.1f);
                bdoor_placed = false;
            }
            if (rdoor_placed)
            {
                RedDoor.transform.parent = this.transform;
                Destroy(RedDoor, 0.1f);
                rdoor_placed = false;
            }
        }
    }
}
