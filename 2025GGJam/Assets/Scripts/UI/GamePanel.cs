using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public string sceneName;
    
    public Button btn_Pause;
    public Button btn_Reloading;
    
    protected override void Update()
    {
        base.Update();
        sceneName = SceneManager.GetActiveScene().name;
    }

    protected override void Init()
    {
        btn_Pause.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<PausePanel>();
        });
        
        btn_Reloading.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneName);
            UIManager.Instance.ShowPanel<GamePanel>();
        });
    }
    
   
}
