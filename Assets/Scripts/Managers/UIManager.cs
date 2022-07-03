using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : GameBehaviour<UIManager>
{
    public TMP_Text flashLightIntensity;
    public TMP_Text smallRocksCollected;
    public TMP_Text canBuildText;

    private void Start()
    {
        UpdateCanBuildText(false);
    }
    private void Update()
    {
        flashLightIntensity.text = "Light Power: " + 
            FL.myLight.intensity.ToString("F2") + " /10";
    }

    public void UpdateSmallRocksCollectedText()
    {
        smallRocksCollected.text = "Small Rocks Collected: " +  
            GM.smallRocksCollected.ToString();
    }


    public void UpdateCanBuildText(bool canBuild)
    {
        if (!canBuild)
        {
            canBuildText.text = "";
        }
        if (canBuild)
        {
            canBuildText.text = "Build";
        }
    }

    public void UpdateCanBuildText(string text)
    {
        canBuildText.text = text;
    }
}
