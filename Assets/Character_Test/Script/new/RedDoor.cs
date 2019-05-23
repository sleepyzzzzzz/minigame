using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    public GameObject RedDoorPrefab;
    private GameObject red;
    public bool rdoor_placed = false;
    public bool hit = false;

    public GameObject Initialize(Vector3 place_pos)
    {
        red = Instantiate(RedDoorPrefab, place_pos, Quaternion.identity);
        rdoor_placed = true;
        return red;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
    }
}