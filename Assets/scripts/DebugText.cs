using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    public string text = null;

	// Use this for initialization
	void Start () {
        text = "Default";
        this.GetComponent<Text>().text = text;
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = text;
	}
}
