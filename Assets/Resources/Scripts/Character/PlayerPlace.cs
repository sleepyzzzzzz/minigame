using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace : MonoBehaviour {

    public GameObject BlueDoorPrefab;
    public GameObject RedDoorPrefab;

    private void Awake()
    {
        BlueDoorPrefab = GameObject.Find("BlueDoorPrefab");
        RedDoorPrefab = GameObject.Find("RedDoorPrefab");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            Instantiate(BlueDoorPrefab, transform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1))
        {
            Instantiate(RedDoorPrefab, transform.position, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RedDoorPrefab.transform.parent = this.transform;
            RedDoorPrefab.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = Input.mousePosition;
            RedDoorPrefab.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.DetachChildren();
            Vector3 dir = transform.TransformDirection(position);
            RedDoorPrefab.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        }
    }
}
