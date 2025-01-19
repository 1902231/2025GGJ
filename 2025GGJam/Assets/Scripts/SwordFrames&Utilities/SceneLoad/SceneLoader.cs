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
        //�첽���س��� ��ɺ�ִ��onCompleted����
        public void LoadSceneAsync(string targetSceneName, UnityAction beforeStart, UnityAction onCompleted)
        {
            StartCoroutine(IELoadSceneAsync(targetSceneName, beforeStart, onCompleted));
        }
        //�첽���س��� ��ִ��beforeStart����,��ɺ�ִ��onCompleted����

        public void LoadScene(string targetSceneName, UnityAction onCompleted)
        {
            SceneManager.LoadScene(targetSceneName);
            onCompleted?.Invoke();
        }//ͬ�����س���






        private IEnumerator IELoadSceneAsync(string targetSceneName, UnityAction onCompleted)
        {

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);//�첽��������
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", asyncOperation.progress);
                //֪ͨUI���н���������
                yield return null; // �ȴ���һ֡

            }
            asyncOperation.allowSceneActivation = true;
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", 1);
                //�������
                yield return null; // �ȴ���һ֡  
            }

            onCompleted?.Invoke();
            //ִ�г����������֮����߼�

        }
        private IEnumerator IELoadSceneAsync(string targetSceneName, UnityAction beforeStart, UnityAction onCompleted)
        {

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);//�첽��������
            asyncOperation.allowSceneActivation = false;
            beforeStart?.Invoke();
            while (asyncOperation.progress < 0.9f)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", asyncOperation.progress);
                //֪ͨUI���н���������
                yield return null; // �ȴ���һ֡

            }
            asyncOperation.allowSceneActivation = true;
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance.TriggerEvent<float>("UIUpdateOnSceneLoading", 1);
                //�������
                yield return null; // �ȴ���һ֡  
            }

            onCompleted?.Invoke();
            //ִ�г����������֮����߼�

        }


    }
}

