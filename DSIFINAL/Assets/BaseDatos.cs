using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UIElements;

public class BaseDatos
{
   
        public static TiendaInfo getTiendaInfo()
        {

            string ruta = Application.persistentDataPath + "/tienda.json";

            Debug.Log(ruta);
            if (!File.Exists(ruta))
            {
                Debug.Log("No se ha encontrado tienda.json");
                return null;
            }
            string json = File.ReadAllText(ruta);

            TiendaInfo datos = JsonHelper.FromJson<TiendaInfo>(json);

            return datos;
        }
    
}
