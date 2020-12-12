using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public GameObject tray = null;
    [HideInInspector]
    public RoboHand roboHand = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private GameObject itemSlot;

    public GameObject Item { get => item; set => item = value; }

    private void Awake()
    {
        roboHand = GetComponent<RoboHand>();
    }

    public void AddItemToSlot(GameObject item)
    {
        if (!this.item)
        {
            this.item = item;
            item.transform.position = itemSlot.transform.position;
            item.transform.rotation = itemSlot.transform.rotation;
            item.transform.parent = itemSlot.transform;

            roboHand?.ChangeHand(item.GetComponent<Item>().GetItemType());
        }
        else
            Debug.Log($"Player's slot is not empty - occupied by {item.name}");
    }

    public void RemoveItemFromSlot()
    {
        Debug.Log($"Player's {item.name} from itemslot got removed");
        this.item = null;
    }

    public void ShowTray()
    {
        item = tray;

        tray.SetActive(true);
    }

    public void HideTray()
    {
        item = null;

        tray.SetActive(false);
    }
}
