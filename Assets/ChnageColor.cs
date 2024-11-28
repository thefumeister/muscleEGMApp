using UnityEngine;

public class ChangeColorWithInput : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float red = 1f, green = 1f, blue = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void Update()
    {
        // Change color based on keyboard inputs (for demonstration purposes)
        // Press keys to increment or decrement color values within 0 to 1 range

        if (Input.GetKeyDown(KeyCode.R))
        {
            red = Mathf.Clamp01(red + 0.1f); // Increase red
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            red = Mathf.Clamp01(red - 0.1f); // Decrease red
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            green = Mathf.Clamp01(green + 0.1f); // Increase green
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            green = Mathf.Clamp01(green - 0.1f); // Decrease green
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            blue = Mathf.Clamp01(blue + 0.1f); // Increase blue
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            blue = Mathf.Clamp01(blue - 0.1f); // Decrease blue
        }

        // Update the sprite color
        UpdateColor();
    }

    void UpdateColor()
    {
        spriteRenderer.color = new Color(red, green, blue);
    }
}
