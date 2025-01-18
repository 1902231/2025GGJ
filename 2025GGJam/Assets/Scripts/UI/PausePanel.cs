using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    public Button btn_Setting;
    public Button btn_Back;
    public Button btn_Close;
    
    protected override void Init()
    {
        btn_Setting.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<PausePanel>();
            UIManager.Instance.ShowPanel<SettingPanel>();
        });
        
        btn_Back.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<PausePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        
        btn_Close.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<PausePanel>();
        });
    }
}
