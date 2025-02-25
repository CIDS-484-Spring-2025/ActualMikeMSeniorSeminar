using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menubuttons : MonoBehaviour
{
    RectTransform rectangle;
    Image image;

    public UnityEvent eventToTrigger;

    void Start()
    {
        rectangle = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the mouse is within our rectangle
        if (RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition))
        {
            image.color = Color.black;

            // Clicking should trigger an event
            if (Input.GetButtonDown("Fire1"))
            {
                eventToTrigger.Invoke();
            }
        }
        else
        {
            image.color = Color.gray;
        }
    }
}
