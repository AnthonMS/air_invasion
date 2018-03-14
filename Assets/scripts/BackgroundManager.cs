using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    //private GameObject player;
    private float bgHorizontalLength;
    public float bgSpeed;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(transform.position);
        bgHorizontalLength = GetComponent<Renderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update ()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //Debug.Log("Length: " + bgHorizontalLength + ", LeftBorder: " + leftBorder);

        if (leftBorder > transform.position.x + (bgHorizontalLength/2) + 1)
        {
            Debug.Log("BG OUT OF SIGHT!");
            RepositionBackground();
        }

        transform.Translate(Vector3.right * bgSpeed * Time.deltaTime);
    }

    private void RepositionBackground()
    {
        // I have to retract 0.1 of the offset x axis, because every second time it had a very small gap. IDK WHY?!?!
        Vector2 bgOffset = new Vector2((bgHorizontalLength * 2f) - 0.1f, 0);
        Debug.Log("bgOffset: " + bgOffset + ", position: " + transform.position + ", length: " + bgHorizontalLength);
        transform.position = (Vector2)transform.position + bgOffset;
        Debug.Log("bgOffset: " + bgOffset + ", position: " + transform.position + ", length: " + bgHorizontalLength);
    }
}
