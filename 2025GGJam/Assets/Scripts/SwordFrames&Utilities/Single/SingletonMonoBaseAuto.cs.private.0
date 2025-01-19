using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBaseAuto<T> : MonoBehaviour where T : MonoBehaviour
{
    //这个不用挂载在空物体上，调的时候自动创建
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                //创建物体
                GameObject go = new GameObject();
                //设定名字
                go.name=typeof(T).ToString();
                //添加组件
                instance=go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
}
