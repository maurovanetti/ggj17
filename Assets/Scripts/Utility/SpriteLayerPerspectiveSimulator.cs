using UnityEngine;

public class SpriteLayerPerspectiveSimulator : MonoBehaviour
{
    public float m_yStartOffSet = 5.5f;
    public bool fixParentAngle = false;
    Transform objTrasform;

    void Start()
    {
        objTrasform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 temp = objTrasform.position;
        temp.y = m_yStartOffSet - temp.z*0.001f; 
        objTrasform.position = temp;
        if(fixParentAngle)
        {
            objTrasform.eulerAngles = new Vector3(90,0,0);
        }
    }

    void TranslateOnY()
    {
        objTrasform.position = new Vector3(objTrasform.position.x, objTrasform.position.y - (objTrasform.position.z / 10000), objTrasform.position.z);
    }
}
