using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenAndroid : MonoBehaviour {

    private static Texture2D texture;
    public static Renderer display;

    public static Texture2D getTexture()
    {
        return texture;
    }

    public static void setTexture(Texture2D _texture)
    {
        texture = _texture;
    }

    public static void CaptureScreen(MonoBehaviour mb)
    {
        mb.StartCoroutine(CaptureScreenCoroutine());
    }

    static IEnumerator CaptureScreenCoroutine()
    {
        string filePath = "/mnt/sdcard/oculus/Screenshots/screenshots.png";// + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //string filePath = "./Assets/a.png";
        //texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        //texture.Apply();

        //byte[] pngData = texture.EncodeToPNG();
        //Object.Destroy(texture);
        //System.IO.File.WriteAllBytes(filePath, pngData);

        System.IO.FileStream binaryFile = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(binaryFile);
        Writer.Write(texture.EncodeToPNG());
        //Object.Destroy(texture);

        yield return new WaitForEndOfFrame();


        ScanMedia(filePath);

    }

    static void ScanMedia(string filePath)
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { filePath }, new string[] { "image/png" }, null);
        }
        Handheld.StopActivityIndicator();
    }

}
