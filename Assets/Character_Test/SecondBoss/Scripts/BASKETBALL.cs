using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BASKETBALL_Manager
{
    public class BASKETBALL : MonoBehaviour
    {
        private int collision_num = 0;
        public static bool exist = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "ground")
            {
                collision_num++;
                Debug.Log(collision_num);
                if (collision_num == 3)
                {
                    Destroy(gameObject);
                    exist = false;
                }
            }
        }
    }
}