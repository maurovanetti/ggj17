using UnityEngine;
using UnityEngine.Events;
public class Bell : MonoBehaviour {

    public bool neverEnding;
    public bool xMagicButton;
    public float ringDecayTime;
    public float maxCircleRangeDelta;
    public UnityEvent onBellRing;
    public float enemyBounceFactor;
    public SpriteRenderer wavesCircle;

    private float ringRange;
    private float circleRange;
    private float minCircleRange = 1f;
    private float maxRingRange;

    // Use this for initialization
    void Start () {
        circleRange = 0f;
        if (enemyBounceFactor < 1f)
        {
            Debug.LogError("enemyBounceFactor < 1");
        }
        if (maxCircleRangeDelta <= 0f)
        {
            Debug.LogError("maxCircleRangeDelta <= 0");
        }
    }
	
	// Update is called once per frame
	void Update () {
        circleRange = Mathf.MoveTowards(circleRange, ringRange * 2, maxCircleRangeDelta);
        if (ringRange > 0)
        {            
            ScareShadows();
            if (!neverEnding)
            {
                ringRange -= (maxRingRange / ringDecayTime) * Time.deltaTime;
            }            
        }
        if (circleRange < minCircleRange)
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

    public void Ring(float force)
    {
        maxRingRange = force;
        ringRange = force;
        ScareShadows();
        onBellRing.Invoke();
    }

    private void ScareShadows()
    {
        if (circleRange >= minCircleRange)
        {
            wavesCircle.enabled = true;
            Vector3 shape = wavesCircle.transform.localScale;
            float aspectRatio = shape.y / shape.x;
            wavesCircle.transform.localScale = new Vector3(circleRange, circleRange * aspectRatio, 0f);
        }
        GameObject[] shadows = GameObject.FindGameObjectsWithTag("Shadow");
        foreach (GameObject shadow in shadows)
        {
            Vector3 offset = shadow.transform.position - transform.position;
            offset.y = 0f;
            float penetrationInBellRange = ringRange - offset.magnitude;
            //Debug.Log("penetrationInBellRange=" + penetrationInBellRange+ " offset =" + offset + " shadow.transform.position=" + shadow.transform.position);
            if (penetrationInBellRange > 0)
            {
                shadow.GetComponent<CharacterDetector>().ScareAway(this.transform, (penetrationInBellRange * enemyBounceFactor));
            }
        }

    }

}
