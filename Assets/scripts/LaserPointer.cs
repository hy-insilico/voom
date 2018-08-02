using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaserPointer : MonoBehaviour {

    [SerializeField]
    private GameObject _Player;

    [SerializeField]
    private Transform _Transform_RightHandAnchor;

    [SerializeField]
    private Transform _Transform_LeftHandAnchor;

    [SerializeField]
    private Transform _CenterEyeAnchor;

    [SerializeField]
    private GameObject _tex;

    [SerializeField]
    private float _MaxDistance = 100.0f;


    public DebugText debugText;

    private Vector3 right_vec = new Vector3(0.2f, 0.0f, 0.5f);
    private Vector3 left_vec = new Vector3(-0.2f, 0.0f, 0.5f);

    //コントローラを取得
    private Transform Pointer
    {
        get
        {
            var controller = OVRInput.GetActiveController();
            //利き手を右に設定している場合
            if (controller == OVRInput.Controller.RTrackedRemote)
            {
                return _Transform_RightHandAnchor;
            }
            else if (controller == OVRInput.Controller.LTrackedRemote)
            {
                return _Transform_LeftHandAnchor;
            }
            else
            {
                return _CenterEyeAnchor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var pointer = Pointer;

        //移動
        if (OVRInput.Get(OVRInput.Button.Up) || Input.GetKey(KeyCode.UpArrow))
        {
            _Player.transform.Translate(Vector3.forward * 0.2f);
            debugText.GetComponent<DebugText>().text = "Button.Up";
            
            
        }
        else if (OVRInput.Get(OVRInput.Button.Down) || Input.GetKey(KeyCode.DownArrow))
        {
            _Player.transform.Translate(Vector3.back * 0.2f);
            debugText.GetComponent<DebugText>().text = "Button.Down";
        }

        if (OVRInput.Get(OVRInput.Button.Right) || Input.GetKey(KeyCode.RightArrow))
        {
            _Player.transform.Translate(Vector3.right * 0.2f);
            debugText.GetComponent<DebugText>().text = "Button.Right";
        }
        else if (OVRInput.Get(OVRInput.Button.Left) || Input.GetKey(KeyCode.LeftArrow))
        {
            _Player.transform.Translate(Vector3.left * 0.2f);
            debugText.GetComponent<DebugText>().text = "Button.Left";
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Z)) {
            debugText.GetComponent<DebugText>().text = "HandTrigger";

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) || Input.GetKey(KeyCode.X))
        {
            debugText.GetComponent<DebugText>().text = "Touchpad";
            Texture2D tex = _tex.GetComponent<PngFileLoad>().getTexture();
            CaptureScreenAndroid.setTexture(tex);
            CaptureScreenAndroid.CaptureScreen(this);
        }



    }

}
