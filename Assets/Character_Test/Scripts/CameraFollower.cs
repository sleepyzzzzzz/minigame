using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManage;

public class CameraFollower : MonoBehaviour {

    public Transform player;
    private float smooth = 5.5f;
    public float xmin;
    public float xmax;
    private bool follow = true;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2-boss")
        {
            Level2Manager.Instance().ReadyToShoot += ChangeMargin;
            Level2Manager.Instance().ShootSuccess += BackToFollow;
            Level2Manager.Instance().ShootFailed += BackToFollow;
        }
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
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
    }

    private void BackToFollow()
    {
        follow = true;
    }
}
