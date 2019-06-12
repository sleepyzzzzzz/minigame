using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManage;

public class CameraFollower : MonoBehaviour {

    public Transform player;
    private float smooth = 5.5f;
    public float xmin;
    public float xmax;
    public static bool follow;

    private void Start()
    {
        follow = true;
        Level2Manager.Instance().ReadyToShoot += ChangeMargin;
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            Vector3 pos = this.transform.position;
            if (Mathf.Abs(pos.x - player.position.x) > 0.2f)
            {
                pos.x = Mathf.Lerp(pos.x, player.position.x, smooth * Time.deltaTime);
            }
            pos.x = Mathf.Clamp(pos.x, xmin, xmax);
            this.transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        }
    }

    private void ChangeMargin()
    {
        follow = false;
        Debug.Log("dsads");
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
    }
}
