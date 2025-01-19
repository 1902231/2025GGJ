using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBaseAuto<T> : MonoBehaviour where T : MonoBehaviour
{
    //������ù����ڿ������ϣ�����ʱ���Զ�����
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                //��������
                GameObject go = new GameObject();
                //�趨����
                go.name=typeof(T).ToString();
                //������
                instance=go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
}
