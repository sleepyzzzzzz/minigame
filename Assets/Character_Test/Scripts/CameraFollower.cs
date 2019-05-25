using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Transform player;
    public float smooth = 5.5f;
    public float XAxis;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x - player.position.x) > XAxis)
        {
            pos.x = Mathf.Lerp(pos.x, player.position.x + 2f, smooth * Time.deltaTime);
        }
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
    }
}
