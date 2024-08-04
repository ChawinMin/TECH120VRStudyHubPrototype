using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WhiteboardDraw : MonoBehaviour
{
    public Texture2D drawingTexture;
    public Color drawingColor = Color.black;
    private bool isDrawing = false;

    // Reference to the XR Ray Interactor
    public XRRayInteractor rayInteractor;

    void Start()
    {
        // Create a new texture and set it as the main texture
        drawingTexture = new Texture2D(512, 512);
        GetComponent<Renderer>().material.mainTexture = drawingTexture;

        // Fill the texture with white color
        Color[] pixels = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.white;
        drawingTexture.SetPixels(pixels);
        drawingTexture.Apply();
    }

    void Update()
    {
        // Check if the XR Ray Interactor is active
        if (rayInteractor != null && rayInteractor.selectEntered)
        {
            isDrawing = true;
        }
        else
        {
            isDrawing = false;
        }

        if (isDrawing)
            DrawOnTexture();
    }

    void DrawOnTexture()
    {
        Ray ray = new Ray(rayInteractor.transform.position, rayInteractor.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector2 uv;
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                uv = hit.textureCoord;
                int x = (int)(uv.x * drawingTexture.width);
                int y = (int)(uv.y * drawingTexture.height);
                drawingTexture.SetPixel(x, y, drawingColor);
                drawingTexture.Apply();
            }
        }
    }
}
