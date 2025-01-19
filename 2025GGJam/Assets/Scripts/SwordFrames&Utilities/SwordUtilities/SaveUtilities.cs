using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System;
namespace SwordFrames
{

    public class SaveUtilities : SingletonMonoBaseAuto<SaveUtilities>
    {


        //public static bool ifLoad = false;//�����ʱûɶ��
      
         
        public string jsonPath;
        public string multipleJsonPath;

        private void Awake()
        {
            jsonPath = Application.dataPath + "/SaveData";//�����ļ���
            multipleJsonPath = Application.dataPath + "/MultipleSaveData";


        }
        //public void SetData()//����ʱ�����������ٱ���
        //{





        //}
        public bool ContainJson(bool multiple = false)
        {
            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // �ж��ļ����Ƿ���� �����ھʹ����ļ���
            }
            if (multiple)
            {
                string[] files = Directory.GetFiles(path, "*.json");  //��浵 �ж��ļ������Ƿ��������һ��json�ļ�
                return files.Length > 0;
            }
            else
            {
                return File.Exists(Path.Combine(path, "Data.json")); //���浵 �ж��ļ������Ƿ����Data.json�ļ�
            }
        }

        public void SaveData<T>(T data, bool multiple = false)
        {
            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // �ж��ļ����Ƿ���� �����ھʹ����ļ���
            }
            string json = JsonUtility.ToJson(data, true);
            if (!multiple)
            {

                File.WriteAllText(Path.Combine(path, "Data.json"), json);
            }
            //���ݲ������жϴ浵·��

            //SetData();



            //���浵
            else
            {//��浵
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "Data.json";
                //����ð�� �ǺϷ���
                File.WriteAllText(Path.Combine(path, pathStr), json);
                //����ʱ���
            }
            //д���ļ�
        }

        public void Load<T>(T data)
        {//��ȡ���浵������data����
            if (!ContainJson())
            {
                Debug.Log("�浵�����ڰ�");
                return;
            }


            string json = File.ReadAllText(Path.Combine(jsonPath, "Data.json"));

            JsonUtility.FromJsonOverwrite(json, data);


        }
        public void LoadMultiple<T>(List<T> list) where T : new()
        {//��ȡ����浵������data�б�
            list.Clear();
            if (ContainJson(true))
            {
                foreach (string file in Directory.GetFiles(multipleJsonPath, "*.json"))
                {//Directory.GetFiles
                 //����һ����̬���������ڻ�ȡָ��·��������ƥ��ָ��ģʽ���ļ�������ʹ��"*.json"��Ϊģʽ����ʾ��ȡ������չ��Ϊ.json���ļ���
                 // Debug.Log("Reading file: " + file);
                    string json = File.ReadAllText(file);
                    //Debug.Log("File content: " + json);
                    //  T singleData = new T();
                    T singleData = JsonUtility.FromJson<T>(json);
                    //T singleData = JsonConvert.DeserializeObject<T>(json);
                    list.Add(singleData);
                    //�����ȡ

                }
            }
            else
            {
                Debug.Log("������·��");
            }
        }



        public void DeBugTime()
        {
            Debug.Log(DateTime.Now.ToString());
        }

    }
}



