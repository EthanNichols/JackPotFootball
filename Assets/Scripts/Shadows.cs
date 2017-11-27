using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour {

    public GameObject shadow;
    public GameObject center;
    public GameObject pivot;
	// Use this for initialization
	void Start () {
        SpriteRenderer shadow_sprite = shadow.GetComponent<SpriteRenderer>();
        StartCoroutine(ShadowTrack(shadow_sprite));
	}

    private IEnumerator ShadowTrack(SpriteRenderer sprite)
    {
        float currentAlpha = sprite.color.a;
        if (gameObject.transform.position.y > 10f)
        {
            currentAlpha += 1f;
        }
        else
        {
            //needs fixing in terms of how long does the object stay on the ground, so that the sprite doesn't resize himself back up
            currentAlpha -= 1f;
            //scale the shadow according to the distance from the edge of the circle to the projectile
            //center the pivot point to the projectile
            pivot.transform.position = new Vector3(gameObject.transform.position.x, 7.1f, gameObject.transform.position.z);
            shadow.transform.SetParent(pivot.transform);
            Vector3 newScale = new Vector3(pivot.transform.position.x - .1f, pivot.transform.position.y, pivot.transform.position.z - .1f);
            pivot.transform.localScale = newScale;
        }
        currentAlpha = Mathf.Clamp(currentAlpha, 0f, 100f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentAlpha);

        yield return new WaitForSeconds(.0001f);
    }
}
