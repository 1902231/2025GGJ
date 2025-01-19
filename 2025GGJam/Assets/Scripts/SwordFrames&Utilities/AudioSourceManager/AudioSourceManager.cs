using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SwordFrames
{
    public class AudioSourceManager : SingletonMonoBaseAuto<AudioSourceManager>
    {
        private AudioSource bkMusic;
        private float bkVolume = 0.8f;

        public GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();
        public float soundVolume = 0.8f;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        //public AudioSourceManager()
        //{
        //    MonoManager.Instance.AddUpdateListener(Update);
        //}

        public void Update()
        {
            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
        }

        public void PlayBkMusic(string name)
        {
            if (bkMusic == null)
            {
                GameObject obj = new GameObject();
                obj.name = "BkMusic";
                bkMusic = obj.AddComponent<AudioSource>();
                bkMusic.playOnAwake = false;
            }
            ResourceLoad.Instance.LoadResourcesAsync<AudioClip>("AudioSources/BkMusic/" + name,
                (clip) =>
                {
                    if (clip != null)
                    {
                        bkMusic.clip = clip;
                        bkMusic.loop = true;
                        bkMusic.volume = bkVolume;
                        bkMusic.Play();
                    }
                    else
                    {
                        Debug.LogError("Failed to load background music: " + name);
                    }
                });
        }

        public void ChangeBkValue(float value)
        {
            this.bkVolume = value;
            if (bkMusic != null)
            {
                bkMusic.volume = bkVolume;
            }
        }

        public void StopBkMusic()
        {
            if (bkMusic != null)
            {
                bkMusic.Stop();
            }
        }

        public void PauseBkMusic()
        {
            if (bkMusic != null)
            {
                bkMusic.Pause();
            }
        }
        
        public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callBack = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject("Sound");
                DontDestroyOnLoad(soundObj);  // 确保 soundObj 不被销毁
            }

            ResourceLoad.Instance.LoadResourcesAsync<AudioClip>("AudioSources/Sound/" + name,
                (clip) =>
                {
                    if (clip != null)
                    {
                        if (soundObj != null)
                        {
                            AudioSource source = soundObj.AddComponent<AudioSource>();
                            source.clip = clip;
                            source.loop = isLoop;
                            source.playOnAwake = false;
                            source.volume = soundVolume;
                            source.Play();
                            soundList.Add(source);
                            callBack?.Invoke(source);
                        }
                        else
                        {
                            Debug.LogError("Sound object is null");
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to load sound: " + name);
                    }
                });
        }

        public void StopSound(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                source.Stop();
                GameObject.Destroy(source);
            }
        }

        public void ChangeSoundVolume(float volume)
        {
            foreach (var clip in soundList)
            {
                clip.volume = volume;
            }
        }
    }
}