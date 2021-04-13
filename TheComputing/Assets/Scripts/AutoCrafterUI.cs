using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCrafterUI : MonoBehaviour
{
    AutoCrafter autoCrafterScript;
    public RawImage craftedItemImage;
    private void Awake()
    {
        autoCrafterScript = GetComponent<AutoCrafter>();
    }
    private void Start()
    {

    }
}
