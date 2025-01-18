using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btn_Pause;
    protected override void Init()
    {
        btn_Pause.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<PausePanel>();
        });
    }
}
