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
        // ��ȡ Trail Renderer �������������������һ��
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }

        // ���� Trail Renderer �Ĳ���
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
        // ���������Ӵ�����ƽ�ɫ���ƶ������磺
        // transform.Translate(Vector2.right * Time.deltaTime);
    }
}
