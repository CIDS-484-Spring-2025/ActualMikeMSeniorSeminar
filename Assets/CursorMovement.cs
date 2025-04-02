using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorMovement : MonoBehaviour
{
    public RectTransform cursor;
    public List<Button> buttons;
    private int index = 0;

    void Start()
    {
        UpdateCursor();
    }

    void Update()
    {
        // Handle up and down arrow key inputs for navigating through buttons
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursor(1);
        }

        // Handle Enter key to simulate button click
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (buttons.Count > 0) // Ensure there are buttons available
            {
                buttons[index].onClick.Invoke();
            }
        }
    }

    void MoveCursor(int direction)
    {
        index = (index + direction + buttons.Count) % buttons.Count;
        UpdateCursor();
    }

    // Update the cursor position based on the current index
    void UpdateCursor()
    {
        // Check if the button at the current index still exists
        if (buttons.Count > 0 && index >= 0)
        {
            // Skip destroyed buttons
            while (index < buttons.Count && buttons[index] == null)
            {
                index = (index + 1) % buttons.Count; // Move to the next valid button
            }

            // Make sure the index is valid after skipping destroyed buttons
            if (index < buttons.Count && buttons[index] != null)
            {
                RectTransform cursorRect = cursor.GetComponent<RectTransform>();
                RectTransform buttonRect = buttons[index].GetComponent<RectTransform>();

                if (cursorRect != null && buttonRect != null)
                {
                    // Add an offset to move the cursor slightly to the left
                    float offsetX = -20f; // Adjust this value to control spacing
                    cursorRect.position = buttonRect.position + new Vector3(offsetX, 0, 0);
                }
            }
        }
    }

    // Call this function whenever a button is destroyed to remove it from the list
    public void RemoveButton(Button button)
    {
        buttons.Remove(button);

        // Adjust index if needed, in case we remove the button currently being highlighted
        if (index >= buttons.Count)
        {
            index = buttons.Count - 1; // Ensure index doesn't go out of bounds
        }

        UpdateCursor();
    }
}

