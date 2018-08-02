using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PngFileLoad : MonoBehaviour {

    string path = "/mnt/sdcard/oculus/Screenshots/b.png";
    public DebugText debugText;
    private Texture2D img;

    // Use this for initialization
    void Start () {
        img = ReadPng(path);
        Sprite sprite = Sprite.Create(
            texture: img,
            rect: new Rect(0, 0, img.width, img.height),
            pivot: new Vector3(0, 0, 0)
        );

        GetComponent<SpriteRenderer>().sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Texture2D getTexture()
    {
        return img;
    }

    byte[] ReadPngFile(string path)
    {
        FileStream fileStream;
        BinaryReader bin;
        byte[] values = null;

        try
        {
            fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            bin = new BinaryReader(fileStream);
            values = bin.ReadBytes((int)bin.BaseStream.Length);

            bin.Close();

        } catch (System.Exception e)
        {
            string[] stCurrentDir = null;
            try
            {
                stCurrentDir = System.IO.Directory.GetFiles(@"./","b.png",SearchOption.AllDirectories);
                //stCurrentDir = System.IO.Directory.GetFileSystemEntries(@"/", "*");
                //stCurrentDir = System.IO.Directory.GetFiles(@"/storage/self/", "*", System.IO.SearchOption.AllDirectories);

            } catch (System.Exception e2)
            {

            }
            string err = "de";
            if (stCurrentDir.Length != 0)
            {
                err = stCurrentDir[0] + "\n";

                for (int i = 1; i < stCurrentDir.Length; i++)
                {
                    err = err + stCurrentDir[i] + "\n";
                }
            }
            debugText.GetComponent<DebugText>().text = err;

        }

        return values;
    }

    Texture2D ReadPng(string path)
    {
        byte[] readBinary = ReadPngFile(path);

        int pos = 16;

        int width = 0;
        for (int i = 0; i < 4; i++)
        {
            width = width * 256 + readBinary[pos++];
        }

        int height = 0;
        for (int i = 0; i < 4; i++)
        {
            height = height * 256 + readBinary[pos++];
        }

        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(readBinary);

        return texture;
    }
}
