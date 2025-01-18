using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Slider sli_Music;
    public Slider sli_Sound;
    public Button btn_Close;

    protected override void Init()
    {
        MusicData data = GameDataMgr.Instance.musicData;
        sli_Music.value = data.musicVolume;
        sli_Sound.value = data.soundVolume;

        sli_Music.onValueChanged.AddListener((arg0) =>
        {
            BKMusic.Instance.Change(arg0);
            GameDataMgr.Instance.musicData.musicVolume = arg0;
        });

        sli_Sound.onValueChanged.AddListener((arg0) =>
        {
            BKMusic.Instance.Change(arg0);
            GameDataMgr.Instance.musicData.soundVolume = arg0;
        });

        btn_Close.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMusic();
            UIManager.Instance.HidePanel<SettingPanel>();
            
            if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" ||  SceneManager.GetActiveScene().name == "Level3")
            {
                UIManager.Instance.ShowPanel<PausePanel>();
            }
        });
    }
}
