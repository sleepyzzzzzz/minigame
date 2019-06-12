using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Placer;


public class Level1SpecialPortal : MonoBehaviour
{
    public bool isRed;
    private KeyCode ListenKey;
    private Animator anim;
    public AudioClip PortalAudio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ListenKey = isRed ? KeyCode.Mouse1 : KeyCode.Mouse0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(ListenKey))
        {
            anim.SetTrigger("Go");
            GetComponent<Collider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(PortalAudio, transform.position);
        }
    }

    public void AnimEnd()
    {
        if(isRed)
        {
            if(GameObject.FindGameObjectWithTag("BluePortal")!=null)
            {
                GameObject.FindGameObjectWithTag("BluePortal").transform.position = transform.position;
            }
            else
            {
                TransferManage.TransferManager.instalize(TransferManage.PortalType.Blue,transform.position);
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("RedPortal") != null)
            {
                GameObject.FindGameObjectWithTag("RedPortal").transform.position = transform.position;
            }
            else
            {
                TransferManage.TransferManager.instalize(TransferManage.PortalType.Red, transform.position);
            }
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>().Level1DoorSpawn(isRed ? TransferManage.PortalType.Blue : TransferManage.PortalType.Red);
        Destroy(gameObject);
    }
}
