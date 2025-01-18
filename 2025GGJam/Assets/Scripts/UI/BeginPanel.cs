using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btn_Start;
    public Button btn_Setting;
    public Button btn_Quit;

    protected override void Init()
    {
        btn_Start.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<BeginPanel>();
            //显示选关面板
            UIManager.Instance.ShowPanel<ChangLevelPanel>();
        });
        
        btn_Setting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });
        
        btn_Quit.onClick.AddListener(() =>
        {
            //退出游戏
            Application.Quit();
        });
    }

  
}
