using System.Collections;
using System.Collections.Generic;
using Lab5b_namespace;
using UnityEngine;
using UnityEngine.UIElements;


namespace Lab5c_namespace
{
    public class Basedatos
    {
        public static List<Individuo> getData()
        {

            List<Individuo> datos = new List<Individuo>();
            
            StyleBackground imgDefault = new StyleBackground(Resources.Load<Sprite>("rickRueda"));

            Individuo perico = new Individuo(
                "Perico",
                "Palotes",
                imgDefault
            );
            Individuo tornasol = new Individuo(
                "Tornasol",
                "Tornasolado",
                imgDefault

            );

            Individuo luca = new Individuo(
                "Luca",
                "Lucatelli",
                imgDefault

            );

            Individuo ivan = new Individuo(
                "Ivan",
                "Ivanovich",
                imgDefault
            );            

            datos.Add(perico);
            datos.Add(tornasol);
            datos.Add(luca);
            datos.Add(ivan);

            return datos; 
        }
    }
}
