using UnityEngine;
using System;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;


namespace Lab6_namespace
{
    [Serializable]
    public class Individuo
    {
        public event Action Cambio; 

        [SerializeField]private string nombre;

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

        [SerializeField] private string apellido;
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

        [SerializeField]private string imgVeName;
      
        public string ImageVeName { get { return imgVeName; } }

        private StyleBackground imagenFondo;

        public StyleBackground ImagenFondo
        {
            get { return imagenFondo; }
        }
        public void CambioImagenFondo(VisualElement veConImgFondo)
        {

            if (veConImgFondo.name != imgVeName || imagenFondo == null)
            {
                this.imagenFondo = new StyleBackground(veConImgFondo.resolvedStyle.backgroundImage);
                imgVeName = veConImgFondo.name;
                Cambio?.Invoke();
            }
        }

        public Individuo(string nombre, string apellido, StyleBackground imagenFondo,string imgVeName)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.imagenFondo = imagenFondo;
            this.imgVeName = imgVeName;
        }
    }
}
