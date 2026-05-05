using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;


namespace Lab6_namespace
{
    public class Basedatos
    {
        public static List<Individuo> getData()
        {

            string ruta = Application.persistentDataPath + "/lista_individuos.json";

            Debug.Log(ruta);
            if (!File.Exists(ruta))
            {
                Debug.Log("No se ha encontrado lista_individuos.json");
                return new List<Individuo>();
            }

            string json = File.ReadAllText(ruta);

            List<Individuo> datos = JsonHelperIndividuo.FromJson<Individuo>(json);

            return datos;
        }
    }
}
