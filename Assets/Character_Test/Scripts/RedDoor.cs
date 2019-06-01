using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedDoorPrefab
{
    public class RedDoor : MonoBehaviour
    {
        //public GameObject RedDoorPrefab;
        private GameObject red;
        public static bool rdoor_placed = false;
        public bool hit = false;

        public GameObject Initialize(GameObject doorprefab, Vector3 place_pos)
        {
            red = Instantiate(doorprefab, place_pos, Quaternion.identity);
            rdoor_placed = true;
            return red;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            hit = true;
        }
    }
}
//public class RedDoor : MonoBehaviour
//{
//    //public GameObject RedDoorPrefab;
//    private GameObject red;
//    public bool rdoor_placed = false;
//    public bool hit = false;

//    public GameObject Initialize(GameObject doorprefab, Vector3 place_pos)
//    {
//        red = Instantiate(doorprefab, place_pos, Quaternion.identity);
//        rdoor_placed = true;
//        return red;
//    }

//    public void OnTriggerEnter2D(Collider2D collision)
//    {
//        hit = true;
//    }
//}