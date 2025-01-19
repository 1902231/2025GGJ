using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // 新增命名空间引用
using SwordFrames;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel
{
    public Button btn_Start;
    public Button btn_Setting;
    public Button btn_Quit;
    public Button btn_Choose;
    
    protected override void Init()
    {
        btn_Start.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui1");
            SceneManager.LoadScene("Level1");
            UIManager.Instance.HidePanel<BeginPanel>();
        });

        btn_Setting.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui1");
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btn_Quit.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui1");
            // 退出游戏
            Application.Quit();
        });
        
        btn_Choose.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui1");
            UIManager.Instance.ShowPanel<ChangLevelPanel>();
            UIManager.Instance.HidePanel<BeginPanel>();
        });
    }
}
