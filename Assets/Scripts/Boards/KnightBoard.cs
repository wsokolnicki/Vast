using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class KnightBoard : PlayerBoard
{
    //[Header("BOARD INFO CACHE", order = 2)]
    [Header("_Bomb", order = 3)]
    [SerializeField] GameObject bombCollider;
    [SerializeField] GameObject bombPanel;
    [Header("_Bow", order = 4)]
    [SerializeField] GameObject bowCollider;
    [SerializeField] GameObject bowPanel;
    [Header("_Map", order = 5)]
    [SerializeField] GameObject mapCollider;
    [SerializeField] GameObject mapPanel;
    [Header("_Shield", order = 6)]
    [SerializeField] GameObject shieldCollider;
    [SerializeField] GameObject shieldPanel;
    [Header("_TheKnight", order = 7)]
    [SerializeField] GameObject knightCollider;
    [SerializeField] GameObject knightPanel;
    [Header("_Grit", order = 8)]
    [SerializeField] GameObject gritCollider;
    [SerializeField] GameObject gritPanel;

    protected override GameObject ReturnPanel(string name)
    { 
        switch (name)
        {
            case "bomb":
                return bombPanel;
            case "bow":
                return bowPanel;
            case "map":
                return mapPanel;
            case "shield":
                return shieldPanel;
            case "knight":
                return knightPanel;
            case "grit":
                return gritPanel;
            default:
                return null;
        }
    }

    protected override void BoardInfo()
    {
            if (Input.GetMouseButtonDown(1) && !infoClicked)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == bombCollider)
                        PanelInfo("bomb");
                    else if (hit.collider.gameObject == bowCollider)
                        PanelInfo("bow");
                    else if (hit.collider.gameObject == mapCollider)
                        PanelInfo("map");
                    else if (hit.collider.gameObject == shieldCollider)
                        PanelInfo("shield");
                    else if (hit.collider.gameObject == knightCollider)
                        PanelInfo("knight");
                    else if (hit.collider.gameObject == gritCollider)
                        PanelInfo("grit");
                }
            }
            else if (Input.GetMouseButton(0) && infoClicked)
            {
                infoClicked = false;
                TurnPanelOff(currentlyOpenPanel);
                currentlyOpenPanel = null;
            }
    }
}

#pragma warning restore 0649