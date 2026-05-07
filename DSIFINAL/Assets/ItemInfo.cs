using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public string key;
    public string nombre;
    public string imgUrl;
    public float precioXunidad;
    public float precioXCaja;
    public float cantidadXCaja;
    public ItemInfo(string key, string nombre,string imgUrl, float precioXU, float precioXCaja, float cantidadXCaja)
    {
        this.key = key;
        this.nombre = nombre;
        this.imgUrl = imgUrl;
        this.precioXunidad = precioXU;
        this.precioXCaja = precioXCaja;
        this.cantidadXCaja = cantidadXCaja;
    }
}

public enum ItemType
{
   Unidad,
   Caja
}

[Serializable]
public class SlotInfo
{
    public string key;
    public int cantidad_U;
    public int cantidad_Cajas;

    public SlotInfo(string key, int cantidad_U = 0, int cantidad_Cajas = 0)
    {
        this.key = key;
        this.cantidad_U = cantidad_U;
        this.cantidad_Cajas = cantidad_Cajas;
    }
}

[Serializable]
public class TiendaInfo
{
    public List<SlotInfo> catalogoItems;
    public TiendaInfo()
    {
        catalogoItems = new List<SlotInfo>();
    }
}

[Serializable]
public class UserInfo
{
    public List<SlotInfo> inventario;
    public List<SlotInfo> carrito;
    float dinero;
    public UserInfo(float dinero)
    {
        inventario = new List<SlotInfo>();
        carrito = new List<SlotInfo>();
        this.dinero = dinero;
    }
}