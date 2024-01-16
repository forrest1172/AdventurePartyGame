using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerData;
    public GameObject bg;
    public TextMeshProUGUI worldData;

    public void DisplayPlayerData(Vector3 pd)
    {
        bg.SetActive(true);
        
        PlayerData.text = ("Speed: " + pd.x + "\n" + "Health: " + pd.y + "\n" + "Damage: " + pd.z);
        
    }

    public void DisablePlayerData()
    {
        bg.SetActive(false);
    }

    public void DisplayWorldData(string tileInfo)
    {
        worldData.DOFade(255,0);
        worldData.text = tileInfo;
        worldData.DOFade(0, 3f);

    }
}
