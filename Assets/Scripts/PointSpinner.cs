using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSpinner : MonoBehaviour {

    public int panelAmount;
    public GameObject panel;

    private List<GameObject> panels = new List<GameObject>();

    private float rotateAmount;

	// Use this for initialization
	void Start () {
        CreateSpinner();
        rotateAmount = 0;
	}

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(rotateAmount, 0, 0));
        rotateAmount += Time.deltaTime * 50;

        foreach(GameObject getPanel in panels)
        {
            if (getPanel.transform.position.z > 50)
            {
                getPanel.SetActive(true);
            } else
            {
                getPanel.SetActive(false);
            }
        }


        transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private void CreateSpinner()
    {
        for (int i = 0; i < panelAmount; i++)
        {
            float angle = ((360f / (float)panelAmount) * i) * Mathf.Deg2Rad;

            GameObject newPanel = Instantiate(panel, Vector3.zero, Quaternion.identity, transform);

            newPanel.transform.localPosition = new Vector3(0, Mathf.Sin(angle), -Mathf.Cos(angle)) * (11f * (panelAmount / 4f));
            newPanel.transform.rotation = Quaternion.Euler(new Vector3(angle * Mathf.Rad2Deg, 0, 0));

            panels.Add(newPanel);
        }
    }
}
