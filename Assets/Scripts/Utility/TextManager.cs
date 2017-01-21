using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public TextAsset TextFile;
    public Text shownText;
    public Image Container;
    public float textDelay;
    public int timeDelayCut;

    string[] textArray;
    float startTime;
    int textNumber;
    public bool trigger;
    int arrayLegth;
    int textLength;

    void Start ()
    {
        SplitTextFile();
        arrayLegth = textArray.Length;
        WriteText();
    }

    void FixedUpdate()
    {
        
        if ((startTime + textDelay * (textArray[textNumber].Length/ timeDelayCut)).Equals(Time.fixedTime) && (textNumber < arrayLegth) && trigger)
        {
            WriteText();
        }
        else if((startTime + textDelay * (textArray[textNumber].Length / timeDelayCut)).Equals(Time.fixedTime) && textNumber == arrayLegth && trigger)
        {
            ShowUI(false);
        }
        
    }

    void ShowUI(bool _value)
    {
        trigger = _value;
        Container.enabled = _value;
        shownText.enabled = _value;
    }

    void WriteText()
    {
        startTime += textDelay;
        shownText.text = textArray[textNumber];
        textNumber++;
    }

    void SplitTextFile()
    {
        textArray = TextFile.text.Split('\n');
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startTime = Time.fixedTime;
            ShowUI(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowUI(false);
        }
    }
}
