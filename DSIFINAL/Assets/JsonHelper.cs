using System.Collections.Generic;
using System;
using UnityEngine;


public static class JsonHelper
{
    public static T FromJson<T>(string json)
    {
        ListaItem<T> lacosa = JsonUtility.FromJson<ListaItem<T>>(json);
        return lacosa.lodedentro;
    }

    //public static string ToJson<T>(List<T> lista)
    //{
    //    ListaItem<T> listaIndividuo = new ListaItem<T>();
    //    listaIndividuo.Individuos = lista;
    //    return JsonUtility.ToJson(listaIndividuo);
    //}

    //public static string ToJson<T>(List<T> lista, bool prettyPrint)
    //{
    //    ListaItem<T> listaIndividuo = new ListaItem<T>();
    //    listaIndividuo.lodedentro = lista;
    //    return JsonUtility.ToJson(listaIndividuo, prettyPrint);
    //}
    public static string ToJson<T>(T cosa, bool prettyPrint)
    {
        ListaItem<T> tempObj = new ListaItem<T>();
        tempObj.lodedentro = cosa;
        return JsonUtility.ToJson(tempObj, prettyPrint);
    }

    [Serializable]

    private class ListaItem<T>
    {
        public T lodedentro;
    }
}



