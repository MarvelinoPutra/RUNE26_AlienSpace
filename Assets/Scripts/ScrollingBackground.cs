using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float backgroundWidth = 19.2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= -backgroundWidth)
        {
            transform.position = new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}
