using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class ACORNS : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Translate(0, -Speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                Destroy(gameObject);
                PlayerController.acorns_hit_count++;
                Recreate();
                break;
            case "ground":
                Destroy(gameObject, 0.5f);
                Recreate();
                break;
            default:
                break;
        }
    }

    private void Recreate()
    {
        if (this.tag == "acorns1") Level2backmanager.create1 = false;
        if (this.tag == "acorns2") Level2backmanager.create2 = false;
        if (this.tag == "acorns3") Level2backmanager.create3 = false;
        if (this.tag == "acorns4") Level2backmanager.create4 = false;
        if (this.tag == "acorns5") Level2backmanager.create5 = false;
        if (this.tag == "acorns6") Level2backmanager.create6 = false;
        if (this.tag == "acorns7") Level2backmanager.create7 = false;
        if (this.tag == "acorns8") Level2backmanager.create8 = false;
        if (this.tag == "acorns9") Level2backmanager.create9 = false;
    }
}
