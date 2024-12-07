using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupSobre;
    public GameObject popupConfig;

    public void OpenPopup(GameObject popup)
    {
        popup.SetActive(true);
        Debug.Log("button is pressed");
    }

    public void ClosePopup(GameObject popup)
    {
        popup.SetActive(false);
    }
}
