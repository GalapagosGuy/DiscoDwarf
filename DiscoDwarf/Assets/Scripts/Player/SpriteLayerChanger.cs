using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerChanger : MonoBehaviour
{
    public static SpriteLayerChanger Instance = null;

    private void Awake()
    {
        if (SpriteLayerChanger.Instance == null)
            SpriteLayerChanger.Instance = this;
        else
            Destroy(this);
    }

    public SpritesContainer playerSprites;

    private List<SpritesContainer> rootOfObjectWithSpriteRenderers = new List<SpritesContainer>();

    private void OnTriggerEnter(Collider other)
    {
        SpritesContainer sc = other.transform.root.GetComponentInChildren<SpritesContainer>();

        if (sc && !rootOfObjectWithSpriteRenderers.Contains(sc) && sc != playerSprites)
            rootOfObjectWithSpriteRenderers.Add(sc);
    }

    private void OnTriggerExit(Collider other)
    {
        SpritesContainer sc = other.transform.root.GetComponentInChildren<SpritesContainer>();

        if (sc && rootOfObjectWithSpriteRenderers.Contains(sc) && sc != playerSprites)
        {
            sc.RestoreLayersToDefault();
            rootOfObjectWithSpriteRenderers.Remove(sc);
        }
    }

    public void RemoveReference(SpritesContainer sc)
    {
        if (rootOfObjectWithSpriteRenderers.Contains(sc))
            rootOfObjectWithSpriteRenderers.Remove(sc);
    }

    private void Update()
    {
        for (int i = 0; i < rootOfObjectWithSpriteRenderers.Count; i++)
        {
            if (rootOfObjectWithSpriteRenderers[i] == playerSprites)
                continue;

            if (rootOfObjectWithSpriteRenderers[i].distancePoint.transform.position.z > playerSprites.distancePoint.transform.position.z)
                rootOfObjectWithSpriteRenderers[i].MoveLayersDown();
            else if (rootOfObjectWithSpriteRenderers[i].distancePoint.transform.position.z < playerSprites.distancePoint.transform.position.z)
                rootOfObjectWithSpriteRenderers[i].MoveLayersUp();
        }
    }
}
