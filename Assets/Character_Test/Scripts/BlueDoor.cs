using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransferManage;

namespace BlueDoorPrefab
{
    public class BlueDoor : MonoBehaviour
    {
        //public GameObject BlueDoorPrefab;
        private GameObject blue;
        public static bool bdoor_placed = false;
        public bool hit = false;

        public GameObject Initialize(GameObject doorprefab, Vector3 place_pos)
        {
            blue = Instantiate(doorprefab, place_pos, Quaternion.identity);
            bdoor_placed = true;
            return blue;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            hit = true;
        }
    }
}
//public class BlueDoor : MonoBehaviour
//{
//    //public GameObject BlueDoorPrefab;
//    private GameObject blue;
//    public bool bdoor_placed = false;
//    public bool hit = false;

//    public GameObject Initialize(GameObject doorprefab, Vector3 place_pos)
//    {
//        blue = Instantiate(doorprefab, place_pos, Quaternion.identity);
//        bdoor_placed = true;
//        return blue;
//    }

//    public void OnTriggerEnter2D(Collider2D collision)
//    {
//        hit = true;
//    }
//}