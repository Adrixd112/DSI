using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public string key;
    public string nombre;
    public string imgUrl;
    public float precio_unitario;
    public float precio_caja;
    public float cantidad_caja;
}

public enum ItemType
{
   Unidad,
   Caja
}

[Serializable]
public class slotInfo
{
    ItemType type;
    public string key;
    public int cantidad;
}

[Serializable]
public class TiendaInfo
{
    public List<slotInfo> itemsEnVenta;
}

[Serializable]
public class UserInfo
{
    public List<slotInfo> inventario;
    public List<slotInfo> carrito;
    float dinero;
}