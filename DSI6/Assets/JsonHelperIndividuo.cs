using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab6_namespace
{
    public static class JsonHelperIndividuo
    {
        public static List<T> FromJson<T>(string json)
        {
            ListaItem<T> listaIndividuo = JsonUtility.FromJson<ListaItem<T>>(json);
            return listaIndividuo.Individuos;
        }

        public static string ToJson<T>(List<T> lista)
        {
            ListaItem<T> listaIndividuo = new ListaItem<T>();
            listaIndividuo.Individuos = lista;
            return JsonUtility.ToJson(listaIndividuo);
        }

        public static string ToJson<T>(List<T> lista, bool prettyPrint)
        {
            ListaItem<T> listaIndividuo = new ListaItem<T>();
            listaIndividuo.Individuos = lista;
            return JsonUtility.ToJson(listaIndividuo, prettyPrint);
        }

        [Serializable]

        private class ListaItem<T>
        {
            public List<T> Individuos;
        }
    }
   
}