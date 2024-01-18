using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerData;
    public GameObject bg;
    public TextMeshProUGUI worldData;
    public Image worldData_bg;
    private void Start()
    {
        worldData_bg.DOFade(0, 0f);
    }
    public void DisplayPlayerData(Vector3 pd)
    {
        bg.SetActive(true);
        
        PlayerData.text = ("Speed: " + pd.x + "\n" + "Health: " + pd.y + "\n" + "Moves: " + pd.z);
        
    }

    public void DisablePlayerData()
    {
        bg.SetActive(false);
    }

    public void DisplayWorldData(string tileInfo)
    {
        worldData_bg.DOFade(50, 0f);
        worldData.DOFade(255,0f);
        worldData.text = tileInfo;
        worldData.DOFade(0, 3f);
        worldData_bg.DOFade(0, 3f);

    }
}
