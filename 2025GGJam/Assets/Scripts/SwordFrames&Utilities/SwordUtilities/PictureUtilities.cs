//using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
namespace SwordFrames
{
    public class PictureUtilities : SingletonMonoBaseAuto<PictureUtilities>
    {

        public Texture2D nowTex2d;//截图缓冲区 此处 缓冲区用词不准确 确切来说是这个类持有的Texture对象 在外部可以访问这个对象 从而得到当前屏幕的截图

        public string PicturePath = Application.dataPath + "/ScreenSaveData";

        public Sprite GetSprite(Texture2D texture)
        { //Texture2D转Sprite,返回转后的Sprite
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //Debug.Log(nowTex2d+"有图片啊");
                return sprite;
            }
            else
            {
                Debug.Log("所给的TEX2D为空,无法执行GetSprite");
                return null;
            }
        }
        public void GetSpriteList(List<Texture2D> texList, List<Sprite> sprList)
        {//把Texture2D图片列表转换为Sprite图片列表,返回列表
            if (texList != null)
            {
                sprList.Clear();
                foreach (Texture2D tex in texList)
                {
                    sprList.Add(GetSprite(tex));
                }
            }
        }
        public void ScreenCaptureAuto()
        {
            StartCoroutine(IEScreenCaptureAuto());
            //自动 截图并保存到文件夹中
        }
        public IEnumerator IEScreenCaptureAuto()
        {

            yield return StartCoroutine(IEScreenCapture());
            SaveScreen();

        }

        public void UpdateScreenCapature()
        {//更新截图缓冲区
            StartCoroutine(IEScreenCapture());
        }
        public void UpdateScreenCapature(UnityAction callBack)
        {//更新截图缓冲区
            StartCoroutine(IEScreenCapture(callBack));
        }//加上回调函数的重载
        public IEnumerator IEScreenCapture(UnityAction callBack)
        {
            yield return new WaitForEndOfFrame();
            nowTex2d = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
            //截图返回Texture图像
            callBack();
        }//加上回调函数重载
        public IEnumerator IEScreenCapture()
        {
            yield return new WaitForEndOfFrame();
            nowTex2d = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
            //截图返回Texture图像
        }
        public void SaveScreen()
        {//保存缓冲区的屏幕截图
            ContainFolder();
            if (nowTex2d != null)
            {


                byte[] bytes = nowTex2d.EncodeToPNG();
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";

                //加入时间戳 ,写入图片
                File.WriteAllBytes(Path.Combine(PicturePath, pathStr), bytes);


            }
            else
            {
                Debug.Log("当前Tex2D是空的");
            }
        }
        public bool ContainPicture()
        {//判断是否存在至少一张图片
            string path = PicturePath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // 判断文件夹是否存在 不存在就创建
            }

            string[] files = Directory.GetFiles(path, "*.png");  //判断是否至少有一张Png格式的图片
            return files.Length > 0;


        }
        public void ContainFolder()
        {//判断是否存在文件夹

            if (!Directory.Exists(PicturePath))
            {
                Directory.CreateDirectory(PicturePath); // 判断文件夹是否存在 不存在就创建文件夹
            }
            else
            {
                return;
            }
        }


        public void LoadMultiple(List<Texture2D> list, bool isSprite = false) 
        {
            list.Clear();
            if (ContainPicture())
            {
                foreach (string file in Directory.GetFiles(PicturePath, "*.png"))
                {



                    var texture = LoadTextureFromImage(file);

                    list.Add(texture);

                }
            }
            else
            {
                Debug.Log("不存在图片");
            }
        }
        public static Texture2D LoadTextureFromImage(string loadPath)
        {
            //图片转Texture2D
            var imageData = File.ReadAllBytes(loadPath);
            var texture = new Texture2D(8, 8);
            texture.LoadImage(imageData);
            //报错改成LoadRawTextureData
            return texture;
        }
    }
}
