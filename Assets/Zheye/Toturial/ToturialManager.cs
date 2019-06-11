using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToturialManager : MonoBehaviour
{
    public string ToturialStr;
    public KeyCode ListenKey;
    private bool isListening=false;
    public GameObject ToturialUI;
    public GameObject mask;
    public bool IsBookTrigger=false;
    public Rigidbody2D book;

    private void Update()
    {
        if(isListening&&Input.GetKeyDown(ListenKey))
        {
            Time.timeScale = 1;
            mask.SetActive(false);
            ToturialUI.SetActive(false);
            if(IsBookTrigger)
            {
                book.gameObject.SetActive(true);
                book.AddForce(new Vector2(-600, 0));
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            //画面静止并提示
            Time.timeScale = 0;
            isListening = true;
            mask.SetActive(true);
            ToturialUI.SetActive(true);
            ToturialUI.transform.Find("Text").GetComponent<Text>().text = ToturialStr;
        }
    }
}
