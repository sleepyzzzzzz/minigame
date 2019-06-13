using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiccontrol : MonoBehaviour
{

    //音源AudioSource相当于播放器，而音效AudioClip相当于磁带
    public AudioSource music;
    public AudioClip jump;//这里我要给主角添加跳跃的音效
    public AudioClip put;//放置传送门

    private void Awake()
    {

        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        jump = Resources.Load<AudioClip>("music/3");
        put = Resources.Load<AudioClip>("music/2");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            music.clip = put;
            music.Play();
           

        }

        if (Input.GetMouseButtonDown(1))
        {
            music.clip = put;
            music.Play();


        }
        if (Input.GetKeyDown(KeyCode.W))//如果输入w
        {

            //把音源music的音效设置为jump
            music.clip = jump;
            //播放音效
           music.Play();


        }
    }
}
