using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;

public class ButtonDoTween : MonoBehaviour
{
    public List<Button> buttonsToAnimate = new List<Button>();

    private void Start()
    {
        if (buttonsToAnimate.Count == 0)
        {
            Debug.LogWarning("No buttons selected to animate.");
            return;
        }

        foreach (Button button in buttonsToAnimate)
        {
            // 获取或添加 EventTrigger 组件
            EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

            // 创建一个新的 EventTrigger.Entry 用于 onPointerEnter 事件
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) =>
            {
                // 鼠标悬停时的缩放动画，类似泡泡吹出的效果
                button.transform.DOScale(1.2f, 0.5f)
                              .SetEase(Ease.OutBack);
            });
            eventTrigger.triggers.Add(pointerEnterEntry);

            // 创建一个新的 EventTrigger.Entry 用于 onPointerExit 事件
            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((data) =>
            {
                // 鼠标离开时的缩放恢复动画
                button.transform.DOScale(1f, 0.2f);
            });
            eventTrigger.triggers.Add(pointerExitEntry);

            // 点击动画：按钮被点击时缩小到 0.9 倍，然后恢复原大小
            button.onClick.AddListener(() =>
            {
                button.transform.DOScale(0.9f, 0.1f)
                          .OnComplete(() =>
                          {
                              button.transform.DOScale(1f, 0.1f);
                          });
            });

            // 移动动画：按钮从初始位置向上移动 20 个单位，然后再回到初始位置
            button.transform.DOMoveY(button.transform.position.y + 20f, 1f)
                    .SetLoops(-1, LoopType.Yoyo);

            // 淡入淡出动画：按钮在不透明度 0.5 和 1 之间循环
            var image = button.GetComponent<Image>();
            if (image != null)
            {
                image.DOFade(0.5f, 1f)
                  .SetLoops(-1, LoopType.Yoyo);
            }
        }
    }
}
