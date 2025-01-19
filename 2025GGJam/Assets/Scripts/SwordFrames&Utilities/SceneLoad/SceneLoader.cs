using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace SwordFrames
{

    //using UnityAction;
    public class SceneLoader : SingletonMonoBaseAuto<SceneLoader>

    {

        public void LoadSceneAsync(string targetSceneName, UnityAction onCompleted)
        {
            StartCoroutine(IELoadSceneAsync(targetSceneName, onCompleted));
        }
        //异步加载场景 完成后执行onCompleted操作
        public void LoadSceneAsync(string targetSceneName, UnityAction beforeStart, UnityAction onCompleted)
        {
            StartCoroutine(IELoadSceneAsync(targetSceneName, beforeStart, onCompleted));
        }
        //异步加载场景 先执行beforeStart操作,完成后执行onCompleted操作

        public void LoadScene(string targetSceneName, UnityAction onCompleted)
        {
            SceneManager.LoadScene(targetSceneName);
            onCompleted?.Invoke();
        }//同步加载场景






        private IEnumerator IELoadSceneAsync(string targetSceneName, UnityAction onCompleted)
        {

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);//异步场景加载
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", asyncOperation.progress);
                //通知UI进行进度条更新
                yield return null; // 等待下一帧

            }
            asyncOperation.allowSceneActivation = true;
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", 1);
                //加载完成
                yield return null; // 等待下一帧  
            }

            onCompleted?.Invoke();
            //执行场景加载完成之后的逻辑

        }
        private IEnumerator IELoadSceneAsync(string targetSceneName, UnityAction beforeStart, UnityAction onCompleted)
        {

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);//异步场景加载
            asyncOperation.allowSceneActivation = false;
            beforeStart?.Invoke();
            while (asyncOperation.progress < 0.9f)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", asyncOperation.progress);
                //通知UI进行进度条更新
                yield return null; // 等待下一帧

            }
            asyncOperation.allowSceneActivation = true;
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", 1);
                //加载完成
                yield return null; // 等待下一帧  
            }

            onCompleted?.Invoke();
            //执行场景加载完成之后的逻辑

        }


    }
}

