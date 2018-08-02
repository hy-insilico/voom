using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DebugPath : MonoBehaviour {

    // Use this for initialization
    void Start() {
        string path = "";
        //内部ストレージ
        // /data/app/com.kennkino.test-1/base.apk
        string str = Application.dataPath + "\n";
        //外部ストレージ
        // /storage/emulated/0/Android/data/com.kennkino.test/files/
        str = str + Application.persistentDataPath + "\n";
        /*
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Enviromnment"))
        using (AndroidJavaObject joExDIr = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            path = joExDIr.Call<string>("toString") + "/jp.co.cname.app/";
        }
        path += System.DateTime.Now.Ticks.ToString() + ".png";
        */
        /*
        string[] stCurrentDir = System.IO.Directory.GetFileSystemEntries(@"/mnt/sdcard/oculus/Screenshots/", "*");
        string err = "";
        if (stCurrentDir.Length != 0)
        {
            err = stCurrentDir[0] + "\n";

            for (int i = 1; i < stCurrentDir.Length; i++)
            {
                err = err + stCurrentDir[i] + "\n";
            }
        }
        */
        this.GetComponent<Text>().text = str + path;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
