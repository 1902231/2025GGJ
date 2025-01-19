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

        public Texture2D nowTex2d;//��ͼ������ �˴� �������ôʲ�׼ȷ ȷ����˵���������е�Texture���� ���ⲿ���Է���������� �Ӷ��õ���ǰ��Ļ�Ľ�ͼ

        public string PicturePath = Application.dataPath + "/ScreenSaveData";

        public Sprite GetSprite(Texture2D texture)
        { //Texture2DתSprite,����ת���Sprite
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //Debug.Log(nowTex2d+"��ͼƬ��");
                return sprite;
            }
            else
            {
                Debug.Log("������TEX2DΪ��,�޷�ִ��GetSprite");
                return null;
            }
        }
        public void GetSpriteList(List<Texture2D> texList, List<Sprite> sprList)
        {//��Texture2DͼƬ�б�ת��ΪSpriteͼƬ�б�,�����б�
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
            //�Զ� ��ͼ�����浽�ļ�����
        }
        public IEnumerator IEScreenCaptureAuto()
        {

            yield return StartCoroutine(IEScreenCapture());
            SaveScreen();

        }

        public void UpdateScreenCapature()
        {//���½�ͼ������
            StartCoroutine(IEScreenCapture());
        }
        public void UpdateScreenCapature(UnityAction callBack)
        {//���½�ͼ������
            StartCoroutine(IEScreenCapture(callBack));
        }//���ϻص�����������
        public IEnumerator IEScreenCapture(UnityAction callBack)
        {
            yield return new WaitForEndOfFrame();
            nowTex2d = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
            //��ͼ����Textureͼ��
            callBack();
        }//���ϻص���������
        public IEnumerator IEScreenCapture()
        {
            yield return new WaitForEndOfFrame();
            nowTex2d = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
            //��ͼ����Textureͼ��
        }
        public void SaveScreen()
        {//���滺��������Ļ��ͼ
            ContainFolder();
            if (nowTex2d != null)
            {


                byte[] bytes = nowTex2d.EncodeToPNG();
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";

                //����ʱ��� ,д��ͼƬ
                File.WriteAllBytes(Path.Combine(PicturePath, pathStr), bytes);


            }
            else
            {
                Debug.Log("��ǰTex2D�ǿյ�");
            }
        }
        public bool ContainPicture()
        {//�ж��Ƿ��������һ��ͼƬ
            string path = PicturePath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // �ж��ļ����Ƿ���� �����ھʹ���
            }

            string[] files = Directory.GetFiles(path, "*.png");  //�ж��Ƿ�������һ��Png��ʽ��ͼƬ
            return files.Length > 0;


        }
        public void ContainFolder()
        {//�ж��Ƿ�����ļ���

            if (!Directory.Exists(PicturePath))
            {
                Directory.CreateDirectory(PicturePath); // �ж��ļ����Ƿ���� �����ھʹ����ļ���
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
                Debug.Log("������ͼƬ");
            }
        }
        public static Texture2D LoadTextureFromImage(string loadPath)
        {
            //ͼƬתTexture2D
            var imageData = File.ReadAllBytes(loadPath);
            var texture = new Texture2D(8, 8);
            texture.LoadImage(imageData);
            //����ĳ�LoadRawTextureData
            return texture;
        }
    }
}
