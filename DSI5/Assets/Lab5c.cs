using System.Collections;
using System.Collections.Generic;
using Lab5b_namespace;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lab5c_namespace
{
    public class Lab5c : MonoBehaviour
    {
        [SerializeField]
        VisualTreeAsset tarjetaTemplate;

        List<Individuo> individuos;

        Individuo indivSeleccionado;

        TextField input_nombre;
        TextField input_apellido;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            VisualElement der = root.Q<VisualElement>("Dcha");
            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");

            //si hay tarjetas colocadas manualmente, les pone un indiv. default
            
            foreach (var item in der.Children())
            {
               if (item.ClassListContains("tarjeta")) 
                {
                    Individuo individuoDefault = new Individuo("John", "Doe", new StyleBackground(Resources.Load<Sprite>("rickRueda")));
                    new Tarjeta(item, individuoDefault);
                    item.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                }
            }
            

            //creo tantas tarjetas como individuos en la base de datos

            individuos = Basedatos.getData();

            for (int i = 1;i<individuos.Count();i++)
            {
                VisualElement tarjetaVe = tarjetaTemplate.Instantiate();
                tarjetaVe.AddToClassList("tarjeta");
                tarjetaVe.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                new Tarjeta(tarjetaVe, individuos[i]); //se inicializa la tarjeta enlazándose el ve con los cambios en el individuo
                der.Add(tarjetaVe);

            }


            VisualElement izq = root.Q<VisualElement>("Izda"); //para debug es más fácil no hacer todos los Q<>() en la misma línea
            List<VisualElement> imgs = izq.Q<VisualElement>("header").Children().ToList();
            foreach (VisualElement img in imgs) { img.RegisterCallback<ClickEvent>(CambioImg); }


            //  plantilla.RegisterCallback<ClickEvent>(SeleccionIndividuo);
            input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);


        }
        void SeleccionTarjeta(ClickEvent evt)
        {
            VisualElement tarjeta = evt.target as VisualElement;
            indivSeleccionado = tarjeta.userData as Individuo;

            if (indivSeleccionado == null) return;

            input_nombre.SetValueWithoutNotify(indivSeleccionado.Nombre);
            input_apellido.SetValueWithoutNotify(indivSeleccionado.Apellido);
        }
        void CambioImg(ClickEvent evt)
        {
            if (indivSeleccionado == null) return;
            indivSeleccionado.ImagenFondo = new StyleBackground((evt.currentTarget as VisualElement).resolvedStyle.backgroundImage);
        }

        void CambioNombre(ChangeEvent<string> evt)
        {
            if (indivSeleccionado == null) return;
            indivSeleccionado.Nombre = evt.newValue;
        }

        void CambioApellido(ChangeEvent<string> evt)
        {
            if (indivSeleccionado == null) return;
            indivSeleccionado.Apellido = evt.newValue;
        }

    }
}

