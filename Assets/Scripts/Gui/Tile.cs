using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    public Vector2Int coordinate;
    [HideInInspector] public RawImage img;

    public void Awake()
    {
        //Init each tile into the guiManager!
        GameManager.Instance.guiManager.grid[coordinate.x, coordinate.y] = this;
    }


    public void Start()
    {
        img = GetComponent<RawImage>();
    }


    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    //img.color = Color.white;

    //    if (GameManager.Instance.phase == GameManager.Phases.Action)
    //        GameManager.Instance.guiManager.ClearTiles();
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Finds Gui Manager
        //Updates cell colors!
        //img.color = Color.red;
        GameManager.Instance.guiManager.ColorTiles(coordinate);

        //print(coordinate);
    }
}
