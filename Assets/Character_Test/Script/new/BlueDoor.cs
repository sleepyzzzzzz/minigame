using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    public GameObject BlueDoorPrefab;
    private GameObject blue;
    public bool bdoor_placed = false;
    public bool hit = false;

    public GameObject Initialize(Vector3 place_pos)
    {
        blue = Instantiate(BlueDoorPrefab, place_pos, Quaternion.identity);
        bdoor_placed = true;
        return blue;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
    }
}
