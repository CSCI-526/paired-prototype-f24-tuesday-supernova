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
<<<<<<< HEAD
    private LineRenderer lineRenderer;
    public GameObject playerObject;

    private SpriteRenderer backgroundSpriteRenderer;
    private bool isPast = true;
    private bool isStemPickedUp = false;

=======

    private SpriteRenderer backgroundSpriteRenderer;
    private bool isPast = true;
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD

        // Get the LineRenderer component
        lineRenderer = stemObject.GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing!");
        }

=======
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
        // If no colors are specified, default to two colors
        if (colors.Length == 0)
        {
            Color lightblue = new Color(0.68f, 0.85f, 0.90f);
<<<<<<< HEAD
            Color darkblue = new Color(59f / 255f, 76f / 255f, 107f / 255f);
            colors = new Color[] { lightblue, darkblue };
=======
            Color Darkblue = new Color(59f / 255f, 76f / 255f, 107f / 255f);
            colors = new Color[] { lightblue, Darkblue };
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
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
<<<<<<< HEAD
            if (isStemPickedUp)
            {
                return;
            }
                // Switch to the next color
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            
=======
            // Switch to the next color
            currentColorIndex = (currentColorIndex + 1) % colors.Length;

>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
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
<<<<<<< HEAD
                     stemObject.SendMessage("ExtendStem", SendMessageOptions.DontRequireReceiver);
                    
                }
            }
        }

        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F) && playerObject != null && stemObject != null)
        {
            // Call a method on playerObject to check if the player is on the ground
            bool isOnGround = playerObject.GetComponent<PlayerMovement>().IsOnGround();
            if(isPast)
            {
                // Get distance to stem
                float distanceToStem = Vector2.Distance(playerObject.transform.position, stemObject.transform.position);


                if (isStemPickedUp && isOnGround)
                {
                    // Place the stem at the player's position
                    Vector3 playerBottomPosition = playerObject.GetComponent<PlayerMovement>().GetBottomPosition();
                    stemObject.transform.position = playerBottomPosition;
                    stemObject.GetComponent<StemGrowth>().PositionDidChange();
                    //lineRenderer.SetPosition(0, playerBottomPosition); // Start point at player
                    //lineRenderer.SetPosition(1, playerBottomPosition + Vector3.up * 0.3f); // End point 2 units above player, for example
                    //lineRenderer.SetPosition(2, playerBottomPosition + Vector3.up * 0.6f); // End point 2 units above player, for example
                    isStemPickedUp = false;
                }
                else if (!isStemPickedUp && distanceToStem < 1.0f)
                {
                    // Pick up the stem
                    isStemPickedUp = true;
                }
            }
            
        }

        if (isStemPickedUp)
        {
            if (lineRenderer != null && playerObject != null)
            {
                // Update the start and end positions of the LineRenderer to follow the player
                Vector3 playerPosition = playerObject.transform.position;

                // Set the positions, for example, between the player and another fixed point
                lineRenderer.SetPosition(0, playerPosition); // Start point at player
                lineRenderer.SetPosition(1, playerPosition + Vector3.up * 0.25f); // End point 2 units above player, for example
                lineRenderer.SetPosition(2, playerPosition + Vector3.up * 0.5f); // End point 2 units above player, for example
            }
            //stemObject.transform.position = playerObject.transform.position;
        }

    }
}

=======
                    stemObject.SendMessage("ExtendStem", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
