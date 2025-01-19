using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SwordFrames;
using Unity.VisualScripting;

public class ChangLevelPanel :BasePanel
{
    public Button level_1;
    public Button level_2;
    public Button level_3;
    public Button level_4;
    public Button level_5;
    public Button level_6;
    public Button btn_close;
    
    protected override void Init()
    {
        level_1.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui2");
            SceneManager.LoadScene("Level1");
            UIManager.Instance.HidePanel<ChangLevelPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
        });
        
        level_2.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui2");
            SceneManager.LoadScene("Level2");
            UIManager.Instance.HidePanel<ChangLevelPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
        });
        
        level_3.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.PlaySound("ui2");
            SceneManager.LoadScene("Level3");
            UIManager.Instance.HidePanel<ChangLevelPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
        });
        
        btn_close.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChangLevelPanel>();
            SceneManager.LoadScene("BeginScene");
        });
    }
}
