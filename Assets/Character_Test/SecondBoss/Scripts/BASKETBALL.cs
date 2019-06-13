using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace BASKETBALL_Manager
{
    public class BASKETBALL : MonoBehaviour
    {
        private int collision_num = 0;
        public static bool exist = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case "Player":
                    Destroy(gameObject);
                    Level2backmanager.Instance.PlayerGetHurt();
                    PlayerController.acorns_hit_count++;
                    PlayerController.hurt = true;
                    break;
                case "ground":
                    collision_num++;
                    if (collision_num == 3)
                    {
                        Destroy(gameObject);
                        exist = false;
                    }
                    break;
            }
        }
    }
}