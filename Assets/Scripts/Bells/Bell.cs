using UnityEngine;
using UnityEngine.Events;
public class Bell : MonoBehaviour {

    public bool xMagicButton;
    public float maxRingRange;
    public float ringDecayTime;
    public UnityEvent onBellRing;
    public float enemyBounceFactor;
    public SpriteRenderer wavesCircle;

    private float ringRange;

	// Use this for initialization
	void Start () {
		if (enemyBounceFactor < 1f)
        {
            Debug.LogError("enemyBounceFactor < 1");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (ringRange > 0)
        {
            ScareShadows(1.0f);
            ringRange -= (maxRingRange / ringDecayTime) * Time.deltaTime;
        }
        else
        {
            wavesCircle.enabled = false;
        }
        
    }

    void OnGUI()
    {
        if (xMagicButton)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.LogWarning("CHEAT ring");
                Ring(1.0f);
            }
        }
    }

    public void Ring(float multiplier)
    {
        ringRange = maxRingRange;
        ScareShadows(multiplier);
        onBellRing.Invoke();
    }

    private void ScareShadows(float multiplier)
    {
        wavesCircle.enabled = true;
        Vector3 shape = wavesCircle.transform.localScale;
        float aspectRatio = shape.y / shape.x;
        wavesCircle.transform.localScale = new Vector3(ringRange * 2, ringRange * 2 * aspectRatio, 0f);
        GameObject[] shadows = GameObject.FindGameObjectsWithTag("Shadow");
        foreach (GameObject shadow in shadows)
        {
            Vector3 offset = shadow.transform.position - transform.position;
            offset.y = 0f;
            float penetrationInBellRange = ringRange - offset.magnitude;
            //Debug.Log("penetrationInBellRange=" + penetrationInBellRange+ " offset =" + offset + " shadow.transform.position=" + shadow.transform.position);
            if (penetrationInBellRange > 0)
            {
                shadow.GetComponent<CharacterDetector>().ScareAway(this.transform, (penetrationInBellRange * multiplier * enemyBounceFactor));
            }
        }

    }

}
