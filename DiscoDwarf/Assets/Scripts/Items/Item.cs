﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    private ITEMTYPE type;

    public ITEMTYPE GetItemType() { return type; }

}
