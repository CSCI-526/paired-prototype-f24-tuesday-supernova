using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Define a set of colors that can be switched
    public Color[] colors;
    private int currentColorIndex = 0;

    // Expose the GameObjects as public fields for configuration in the Unity editor
    public GameObject backgroundObject;
    public GameObject stemObject;

    private SpriteRenderer backgroundSpriteRenderer;
    private bool isPast = true;

    // Start is called before the first frame update
    void Start()
    {
        // If no colors are specified, default to two colors
        if (colors.Length == 0)
        {
            Color lightblue = new Color(0.68f, 0.85f, 0.90f);
            Color Darkblue = new Color(59f / 255f, 76f / 255f, 107f / 255f);
            colors = new Color[] { lightblue, Darkblue };
        }

        // Get the SpriteRenderer component from the assigned GameObject
        if (backgroundObject != null)
        {
            backgroundSpriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();
        }

        // Initialize the sprite renderer's color to the first color
        if (backgroundSpriteRenderer != null)
        {
            backgroundSpriteRenderer.color = colors[currentColorIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Switch to the next color
            currentColorIndex = (currentColorIndex + 1) % colors.Length;

            // Set the SpriteRenderer color
            if (backgroundSpriteRenderer != null)
            {
                backgroundSpriteRenderer.color = colors[currentColorIndex];
            }

            // Toggle the isPast boolean
            isPast = !isPast;

            // Call the appropriate method on the stemObject
            if (stemObject != null)
            {
                if (isPast)
                {
                    stemObject.SendMessage("ResetStem", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    stemObject.SendMessage("ExtendStem", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
