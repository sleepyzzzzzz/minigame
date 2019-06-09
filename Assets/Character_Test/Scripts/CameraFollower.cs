using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Transform player;
    private float smooth = 5.5f;
    public float xmin;
    public float xmax;

    private void FixedUpdate()
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
