using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeImages;
    public Text scoreText;
    public void UpdateLife(int lives)
    {
        for(int i = 0; i < lifeImages.Length; i++)
        {
            if (lives > i)
            {
                lifeImages[i].color = Color.white;
            }
            else
            {
                lifeImages[i].color = Color.black;
            }
        }
    }

    public void UpdateCoins(int coin)
    {
        scoreText.text = coin.ToString();
    }

}
