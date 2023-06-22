using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public Vector3 Position { get { return transform.position; } }
    protected SpriteRenderer spriteRenderer { get { return spriteRenderers[0]; } }

    SpriteRenderer[] spriteRenderers;
    Material matDefault;
    Material matWhite;

    protected virtual void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        matDefault = spriteRenderers[0].material;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    protected void Show()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    protected void Hide()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1000f);
    }

    public void Flash()
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
            renderer.material = matWhite;

        Invoke("ResetMaterial", .1f);
    }

    private void ResetMaterial()
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.material = matDefault;
        }
    }
}
