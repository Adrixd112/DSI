using System.Collections;
using System.Collections.Generic;
using Lab5b_namespace;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;

namespace Lab5c_namespace
{
    public class Lab5c : MonoBehaviour
    {

        VisualElement root;
        VisualElement der;
        [SerializeField]
        VisualTreeAsset tarjetaTemplate;

        List<Individuo> individuos;

        VisualElement botonCrear;
        Toggle toggleModificar;
        Individuo indivSeleccionado;
        VisualElement backgroundSeleccionado;

        TextField input_nombre;
        TextField input_apellido;

        private void OnEnable()
        {
            if(tarjetaTemplate == null)     tarjetaTemplate = Resources.Load<VisualTreeAsset>("Tarjeta");

            root = GetComponent<UIDocument>().rootVisualElement;

            der = root.Q<VisualElement>("Dcha");
            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");
            botonCrear = root.Q<Button>("BotonCrear");
            toggleModificar = root.Q<Toggle>("ToggleModificar");
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

            for (int i = 1; i < individuos.Count(); i++)
            {
                VisualElement tarjetaVe = tarjetaTemplate.Instantiate();
                tarjetaVe.AddToClassList("tarjeta");
                tarjetaVe.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                new Tarjeta(tarjetaVe, individuos[i]); //se inicializa la tarjeta enlazándose el ve con los cambios en el individuo
                der.Add(tarjetaVe);

            }


            VisualElement izq = root.Q<VisualElement>("Izda"); //para debug es más fácil no hacer todos los Q<>() en la misma línea
            List<VisualElement> imgs = izq.Q<VisualElement>("header").Children().ToList();

            Assert.That(imgs.Count, Is.GreaterThan(0));
            backgroundSeleccionado = imgs[0];
            foreach (VisualElement img in imgs) { img.RegisterCallback<ClickEvent>(CambioImg); }

            botonCrear.RegisterCallback<ClickEvent>(NuevaTarjeta);
            //  plantilla.RegisterCallback<ClickEvent>(SeleccionIndividuo);
            input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);


        }
        void NuevaTarjeta(ClickEvent evt)
        {
            if (!toggleModificar.value)
            {
                VisualElement tarjetaVe = tarjetaTemplate.Instantiate();
                Individuo indiv = new Individuo(input_nombre.value, input_apellido.value, new StyleBackground(backgroundSeleccionado.resolvedStyle.backgroundImage));
                tarjetaVe.AddToClassList("tarjeta");
                tarjetas_borde_negro();
                tarjeta_borde_blanco(tarjetaVe);
                tarjetaVe.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                new Tarjeta(tarjetaVe, indiv); //se inicializa la tarjeta enlazándose el ve con los cambios en el individuo
                der.Add(tarjetaVe);
            }
        }
        void SeleccionTarjeta(ClickEvent evt)
        {
            VisualElement tarjeta = evt.target as VisualElement;
            indivSeleccionado = tarjeta.userData as Individuo;

            if (indivSeleccionado == null) return;

            input_nombre.SetValueWithoutNotify(indivSeleccionado.Nombre);
            input_apellido.SetValueWithoutNotify(indivSeleccionado.Apellido);
            toggleModificar.value = true;

            tarjetas_borde_negro();
            tarjeta_borde_blanco(tarjeta);
        }
        void CambioImg(ClickEvent evt)
        {
            backgroundSeleccionado = evt.currentTarget as VisualElement;
            if (toggleModificar.value)
            {    
                if (indivSeleccionado == null) return;
                indivSeleccionado.ImagenFondo = new StyleBackground(backgroundSeleccionado.resolvedStyle.backgroundImage);
            }
        }

        void CambioNombre(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                if (indivSeleccionado == null) return;
                indivSeleccionado.Nombre = evt.newValue;
            }
        }

        void CambioApellido(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                if (indivSeleccionado == null) return;
                indivSeleccionado.Apellido = evt.newValue;
            }
        }
        void tarjetas_borde_negro()
        {
            List<VisualElement> lista_tarjetas = der.Children().ToList();
            lista_tarjetas.ForEach(elem =>
            {
                VisualElement tarjeta = elem.Q("Tarjeta");

                tarjeta.style.borderBottomColor = Color.black;
                tarjeta.style.borderRightColor = Color.black;
                tarjeta.style.borderTopColor = Color.black;
                tarjeta.style.borderLeftColor = Color.black;
            });
        }

        void tarjeta_borde_blanco(VisualElement tar)
        {
            VisualElement tarjeta = tar.Q("Tarjeta");

            tarjeta.style.borderBottomColor = Color.white;
            tarjeta.style.borderRightColor = Color.white;
            tarjeta.style.borderTopColor = Color.white;
            tarjeta.style.borderLeftColor = Color.white;
        }
    }
}

