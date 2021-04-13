using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScripMenu : MonoBehaviour
{
    public List<GameObject> btn = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < btn.Count; i++)
        {
            GameObject g = btn[i];
            btn[i].GetComponent<Button>().onClick.AddListener(delegate { changeBuilding(g); });
        }
    }
    void changeBuilding(GameObject gm)
    {
        Camera.main.gameObject.GetComponent<BuildScript>().building = gm.GetComponent<BuildingId>().listpos;
        int oldnum = Camera.main.gameObject.GetComponent<BuildScript>().num_building_shadow;
        Camera.main.gameObject.GetComponent<BuildScript>().ch_ShadowBuilding[oldnum].SetActive(false);
        Camera.main.gameObject.GetComponent<BuildScript>().num_building_shadow = gm.GetComponent<BuildingId>().listpos;
        int newnum = Camera.main.gameObject.GetComponent<BuildScript>().num_building_shadow;
        Camera.main.gameObject.GetComponent<BuildScript>().ch_ShadowBuilding[newnum].SetActive(true);
    }
}
