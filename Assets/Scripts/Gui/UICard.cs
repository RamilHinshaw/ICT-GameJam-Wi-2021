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
        GameManager.Instance.UIcard = this;
        GameManager.Instance.SwitchCameraMode(1);
    }

    public void UpdateCard(int passedCardID)
    {
        this.cardID = passedCardID;
        Card referencedCard = GameManager.Instance.cardDatabase.cards[cardID];

        cardName.text = referencedCard.name;
        description.text = referencedCard.description;

        if (referencedCard.img != null)
            img.texture = referencedCard.img;        

        //Update Name, img, description
        //Enable card
    }

}
