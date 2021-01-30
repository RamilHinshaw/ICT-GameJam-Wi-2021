using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public int cardID;

    public Text cardName;
    public Text description;
    public RawImage img;

    public void OnSelect()
    {
        GameManager.Instance.SelectCard(cardID);
    }

    public void UpdateCard()
    {
        //Update Name, img, description
        //Enable card
    }
}
