using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFrameManager : MonoBehaviour
{
    public Transform chooseScrean;
    public GameObject part; // The object you want to scale
    public RectTransform menuTran;
    private Vector2 boundarySize;
    // Start is called before the first frame update

    void Start()
    {
        boundarySize = new Vector2(menuTran.rect.width, menuTran.rect.height);
        // Calculate the bounding box of the object
        Bounds bounds = new Bounds(part.transform.position, Vector3.zero);
        foreach (Renderer renderer in part.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        // Calculate the scale factor
        Vector2 scale = boundarySize;
        scale.x /= bounds.size.x;
        scale.y /= bounds.size.y;
        float scaleFactor = Mathf.Min(scale.x, scale.y);

        // Scale the object to fit inside the boundary while preserving aspect ratio
        Vector3 currentScale = GetGlobalScale(part.transform);
        GameObject UIPart = Instantiate(part, menuTran.rect.position, part.transform.rotation, chooseScrean);
        UIPart.transform.localScale = new Vector3(currentScale.x * scaleFactor, currentScale.y * scaleFactor, currentScale.z * scaleFactor);
        UIPart.transform.position = menuTran.position;
        UIPart.AddComponent<Rotating>();
    }

    private Vector3 GetGlobalScale(Transform partTran)
    {
        Vector3 globalScale = partTran.localScale;
        Transform parent = partTran.parent;

        while (parent != null)
        {
            globalScale = Vector3.Scale(globalScale, parent.localScale);
            parent = parent.parent;
        }

        return globalScale;
    }
}
