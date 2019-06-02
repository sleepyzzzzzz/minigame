﻿using UnityEngine;

public class Chalk : MonoBehaviour {
    public float Speed=5;
    public bool isBigChalk;

    void Update()
    {
        transform.Translate(Speed*Time.deltaTime, 0, 0);
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Chalk Collision:"+coll.collider.tag);
        switch(coll.collider.tag)
        {
            case "Player":
                //扣除生命值
                if(!isBigChalk)Destroy(gameObject);
                break;
            case "ground":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}