using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public TextAsset TextFile;
    public Text shownText;
    public Image Container;
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
        textArray = SplitTextFile();
        arrayLegth = textArray.Length;
        textNumber = -1;
    }

    void Update()
    {
        if((Time.time >= startTime) && trigger)
        {
            if (textNumber < arrayLegth-1)
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
        if (_value)
        {
            startTime = Time.time;
        }
        else
        {
            startTime = 0;
        }
        trigger = _value;
        Container.enabled = _value;
        shownText.enabled = _value;
    }

    void WriteText()
    {
        textNumber++;
        shownText.text = textArray[textNumber];
        startTime = CalcDelayTime();
    }

    public float CalcDelayTime()
    {
        return startTime + Mathf.Clamp(textDelay * (textArray[textNumber].Length), 5f, 10f);
    }

    public string[] SplitTextFile()
    {
        return TextFile.text.Trim().Split('\n');
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowUI(true);
        }
    }
}
