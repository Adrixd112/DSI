using UnityEngine;
using System;
using UnityEngine.UIElements;


namespace Lab5b_namespace
{
    
    public class Individuo
    {
        public event Action Cambio; 

        private string nombre;

        public string Nombre
        {
            get{return nombre;}
            set
            {
                if(value != nombre)
                {
                    nombre = value;
                    Cambio?.Invoke();
                }
            }
        }

        private string apellido;
        public string Apellido
        {
            get{return apellido;}
            set
            {
                if (value != apellido)
                {
                    apellido = value;
                    Cambio?.Invoke();
                }
            }
        }

        
         private Sprite foto;

        public Sprite Foto
        {
            get { return foto; }
            set
            {
                if (value != foto)
                {
                    foto = value;
                    Cambio?.Invoke();
                }
            }
        }

        public Individuo(string nombre, string apellido, Sprite foto)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.foto = foto;
        }
    }
}
