using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public TextAsset TextFile;
    private Canvas canvas;
    private Text shownText;
    private Image Container;
    public float textDelay;

    string[] textArray;
    float startTime;
    int textNumber;
    int arrayLegth;
    bool trigger;

    public void ShowText(bool _value)
    {
        ShowUI(_value);
    }

    void Start ()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        shownText = GetComponentInChildren<Text>();
        Container = GetComponentInChildren<Image>();
        textArray = SplitTextFile();
        arrayLegth = textArray.Length;
        textNumber = -1;
    }

    void Update()
    {
        if ((Time.time >= startTime) && trigger)
        {
            if (textNumber < arrayLegth - 1)
            {
                WriteText();
            }
            else
            {
                ShowUI(false);
            }     
        }
    }

    void ShowUI(bool _value)
    {
        Debug.Log("ShowUI " + _value);
        if (_value)
        {
            startTime = Time.time;            
        }
        else
        {
            startTime = 0;
            DestroyObject(gameObject);
        }
        trigger = _value;
        canvas.enabled = _value;
        if (_value)
        {
            Container.enabled = true;
            shownText.enabled = true;
        }
    }

    void WriteText()
    {
        textNumber++;
        shownText.text = textArray[textNumber];
        startTime = CalcDelayTime();
    }

    float CalcDelayTime()
    {
        return startTime + Mathf.Clamp(textDelay * (textArray[textNumber].Length), 5f, 10f);
    }

    string[] SplitTextFile()
    {
        return TextFile.text.Trim().Split('\n');
    }

    void OnTriggerEnter(Collider other)
    {
        if (textNumber == -1 && other.gameObject.CompareTag("Player"))
        {
            ShowUI(true);
        }
    }
}
