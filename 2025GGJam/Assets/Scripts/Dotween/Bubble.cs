using UnityEngine;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    public float animationDuration = 1f;
    public float finalScale = 1.5f;
    public Color startColor = Color.clear;
    public Color endColor = Color.clear;
    public GameObject bubblePrefab;
    private Renderer bubbleRenderer;
    private bool animationPlaying = false;
    private Vector3 spawnPosition;

    void Start()
    {
        bubbleRenderer = GetComponent<Renderer>();
        if (bubbleRenderer == null)
        {
            Debug.LogError("Renderer component not found on the bubble prefab.");
            return;
        }
        spawnPosition = transform.position;
        StartBubbleAnimation();
    }

    void StartBubbleAnimation()
    {
        if (animationPlaying) return;
        animationPlaying = true;
        transform.localScale = Vector3.zero;
        bubbleRenderer.material.color = startColor;

        // 播放动画
        transform.DOScale(finalScale, animationDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(OnAnimationComplete);

        bubbleRenderer.material.DOColor(endColor, animationDuration);

        Color color = bubbleRenderer.material.color;
        color.a = 0f;
        bubbleRenderer.material.color = color;
        bubbleRenderer.material.DOFade(1f, animationDuration);
    }

    void OnAnimationComplete()
    {
        // 动画完成后创建泡泡预制体
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        // 销毁当前正在播放动画的对象
        Destroy(gameObject);
    }
}