using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lab5b_namespace
{
    public class Lab5b : MonoBehaviour
    {
        VisualElement plantilla;

        TextField input_nombre;
        TextField input_apellido;

        Individuo individuoPrueba;
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            plantilla = root.Q("plantilla");
            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");

            individuoPrueba = new Individuo("Jose","Jostrella",new StyleBackground(Resources.Load<Sprite>("rickRueda")));
            Tarjeta tarjetaPrueba = new Tarjeta(plantilla, individuoPrueba);

            VisualElement izq = root.Q<VisualElement>("Izda"); //para debug es más fácil no hacer todos los Q<>() en la misma línea
            List<VisualElement> imgs = izq.Q<VisualElement>("header").Children().ToList();

            foreach (VisualElement img in imgs) { img.RegisterCallback<ClickEvent>(CambioImg); }
            //  plantilla.RegisterCallback<ClickEvent>(SeleccionIndividuo);
            input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);

            input_nombre.SetValueWithoutNotify(individuoPrueba.Nombre);
            input_apellido.SetValueWithoutNotify(individuoPrueba.Apellido);
        }
        void CambioImg(ClickEvent evt)
        {
            individuoPrueba.ImagenFondo = new StyleBackground((evt.currentTarget as VisualElement).resolvedStyle.backgroundImage);
        }

        void CambioNombre(ChangeEvent<string> evt)
        {
            individuoPrueba.Nombre = evt.newValue;
        }

        void CambioApellido(ChangeEvent<string> evt)
        {
            individuoPrueba.Apellido = evt.newValue;
        }
    }
}
