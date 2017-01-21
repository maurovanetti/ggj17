using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerPerspectiveSimulator : MonoBehaviour
{
    Transform objTrasform;

    void Start()
    {
        objTrasform = GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    void TranslateOnY()
    {
        objTrasform.position = new Vector3(objTrasform.position.x, objTrasform.position.y - (objTrasform.position.z / 10000), objTrasform.position.z);
    }
}
