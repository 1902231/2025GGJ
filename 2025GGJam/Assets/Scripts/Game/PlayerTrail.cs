using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterTrail : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    private void Start()
    {
        // 获取 Trail Renderer 组件，如果不存在则添加一个
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }

        // 配置 Trail Renderer 的参数
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        trailRenderer.startWidth = 0.5f;
        trailRenderer.endWidth = 0;
        trailRenderer.time = 0.5f;
        trailRenderer.minVertexDistance = 0.1f;
        trailRenderer.emitting = true;
        trailRenderer.startColor = Color.white;
        trailRenderer.endColor = Color.clear;
    }

    private void Update()
    {
        // 这里可以添加代码控制角色的移动，例如：
        // transform.Translate(Vector2.right * Time.deltaTime);
    }
}
