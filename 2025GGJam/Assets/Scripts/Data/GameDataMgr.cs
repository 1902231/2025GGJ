using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance= new GameDataMgr();
    public static GameDataMgr Instance => instance;

    public MusicData musicData;

    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("Music");
    }

    public void SaveMusic()
    {
        JsonMgr.Instance.SaveData(musicData, "Music");
    }
}
