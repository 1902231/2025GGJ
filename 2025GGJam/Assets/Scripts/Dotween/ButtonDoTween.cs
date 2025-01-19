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
            // ��ȡ����� EventTrigger ���
            EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

            // ����һ���µ� EventTrigger.Entry ���� onPointerEnter �¼�
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) =>
            {
                // �����ͣʱ�����Ŷ������������ݴ�����Ч��
                button.transform.DOScale(1.2f, 0.5f)
                              .SetEase(Ease.OutBack);
            });
            eventTrigger.triggers.Add(pointerEnterEntry);

            // ����һ���µ� EventTrigger.Entry ���� onPointerExit �¼�
            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((data) =>
            {
                // ����뿪ʱ�����Żָ�����
                button.transform.DOScale(1f, 0.2f);
            });
            eventTrigger.triggers.Add(pointerExitEntry);

            // �����������ť�����ʱ��С�� 0.9 ����Ȼ��ָ�ԭ��С
            button.onClick.AddListener(() =>
            {
                button.transform.DOScale(0.9f, 0.1f)
                          .OnComplete(() =>
                          {
                              button.transform.DOScale(1f, 0.1f);
                          });
            });

            // �ƶ���������ť�ӳ�ʼλ�������ƶ� 20 ����λ��Ȼ���ٻص���ʼλ��
            button.transform.DOMoveY(button.transform.position.y + 20f, 1f)
                    .SetLoops(-1, LoopType.Yoyo);

            // ���뵭����������ť�ڲ�͸���� 0.5 �� 1 ֮��ѭ��
            var image = button.GetComponent<Image>();
            if (image != null)
            {
                image.DOFade(0.5f, 1f)
                  .SetLoops(-1, LoopType.Yoyo);
            }
        }
    }
}
