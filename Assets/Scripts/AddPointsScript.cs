using UnityEngine;

public class AddPointsScript : MonoBehaviour
{

    public float x;
    public float y;
    public string pointstext;

    public GUIStyle style = null;

    // Use this for initialization
    void Start()
    {

    }

    void OnGUI()
    {
        GUI.Label(new Rect(x, y, 100, 100), pointstext, style);
    }

    // Update is called once per frame
    void Update()
    {
        y -= 1;
        if (y < Screen.height * 0.25)
            Destroy(this);
    }
}
