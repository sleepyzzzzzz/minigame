using UnityEngine;

public class Chalk : MonoBehaviour {
    public float Speed=5;
    public bool isBigChalk;

    void Update()
    {
        transform.Translate(Speed*Time.deltaTime, 0, 0);
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.collider.tag)
        {
            case "Player":
                Level1Manager.Instance.PlayerGetHurt();
                Destroy(gameObject);
                break;
            case "ground":
                Destroy(gameObject);
                break;
            case "RedPortal":
            case "BluePortal":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
