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


        //public static bool ifLoad = false;//这个暂时没啥用
      
         
        public string jsonPath;
        public string multipleJsonPath;

        private void Awake()
        {
            jsonPath = Application.dataPath + "/SaveData";//所在文件夹
            multipleJsonPath = Application.dataPath + "/MultipleSaveData";


        }
        //public void SetData()//保存时先设置数据再保存
        //{





        //}
        public bool ContainJson(bool multiple = false)
        {
            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // 判断文件夹是否存在 不存在就创建文件夹
            }
            if (multiple)
            {
                string[] files = Directory.GetFiles(path, "*.json");  //多存档 判断文件夹中是否存在至少一个json文件
                return files.Length > 0;
            }
            else
            {
                return File.Exists(Path.Combine(path, "Data.json")); //单存档 判断文件夹中是否存在Data.json文件
            }
        }

        public void SaveData<T>(T data, bool multiple = false)
        {
            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // 判断文件夹是否存在 不存在就创建文件夹
            }
            string json = JsonUtility.ToJson(data, true);
            if (!multiple)
            {

                File.WriteAllText(Path.Combine(path, "Data.json"), json);
            }
            //根据参数来判断存档路径

            //SetData();



            //单存档
            else
            {//多存档
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "Data.json";
                //不带冒号 是合法的
                File.WriteAllText(Path.Combine(path, pathStr), json);
                //加入时间戳
            }
            //写入文件
        }

        public void Load<T>(T data)
        {//读取单存档，返回data数据
            if (!ContainJson())
            {
                Debug.Log("存档不存在啊");
                return;
            }


            string json = File.ReadAllText(Path.Combine(jsonPath, "Data.json"));

            JsonUtility.FromJsonOverwrite(json, data);


        }
        public void LoadMultiple<T>(List<T> list) where T : new()
        {//读取多个存档，返回data列表
            list.Clear();
            if (ContainJson(true))
            {
                foreach (string file in Directory.GetFiles(multipleJsonPath, "*.json"))
                {//Directory.GetFiles
                 //这是一个静态方法，用于获取指定路径下所有匹配指定模式的文件。这里使用"*.json"作为模式，表示获取所有扩展名为.json的文件。
                 // Debug.Log("Reading file: " + file);
                    string json = File.ReadAllText(file);
                    //Debug.Log("File content: " + json);
                    //  T singleData = new T();
                    T singleData = JsonUtility.FromJson<T>(json);
                    //T singleData = JsonConvert.DeserializeObject<T>(json);
                    list.Add(singleData);
                    //逐个读取

                }
            }
            else
            {
                Debug.Log("不存在路径");
            }
        }



        public void DeBugTime()
        {
            Debug.Log(DateTime.Now.ToString());
        }

    }
}



