using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArena : MonoBehaviour
{
    public int mapSize;
    public float deathLayerPosition;

    // Use this for initialization
    void Start()
    {
        SetupArena();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Set the size of the arena and death layer
    /// </summary>
    private void SetupArena()
    {
        /*
        //Test if a mapSize hasn't been set properly
        if ((mapSize.x <= 0 || mapSize.y <= 0))
        {
            //Test if the arena doesn't have a current size
            if (transform.localScale.x == 0 ||
                transform.localScale.z == 0)
            {
                //Default the arena size to 1
                mapSize = Vector2.one;
            } else
            {
                //Keep the arena the same size as it currently is
                mapSize = new Vector2(transform.localScale.x, transform.localScale.z);
            }
        }
        */
        //Set the size of the arena
        transform.localScale = new Vector3(Mathf.Abs(mapSize), 1, Mathf.Abs(mapSize));

        //Set the position of the death layer under the map, default is -1;
        if (deathLayerPosition == 0)
        {
            deathLayerPosition = 1;
        }
        deathLayerPosition = Mathf.Abs(deathLayerPosition) * -1f;
    }
}
