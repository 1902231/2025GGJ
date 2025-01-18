using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    
    private AudioSource bkSource;

    void Awake()
    {
        instance = this;
        bkSource = GetComponent<AudioSource>();

        MusicData data = GameDataMgr.Instance.musicData;
        Change(data.musicVolume);
    }

    public void Change(float volume)
    {
        bkSource.volume = volume;
    }
}
