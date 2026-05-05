using System.Collections;
using System.Collections.Generic;
using Lab6_namespace;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;

namespace Lab6_namespace
{
    public class Lab6 : MonoBehaviour
    {

        VisualElement root;
        VisualElement der;
        [SerializeField]
        VisualTreeAsset tarjetaTemplate;

        List<Individuo> individuos;
        List<VisualElement> imgs;

        VisualElement botonCrear;
        Toggle toggleModificar;
        Individuo indivSeleccionado;
        VisualElement backgroundSeleccionado;

        TextField input_nombre;
        TextField input_apellido;

        private void OnEnable()
        {

            if (tarjetaTemplate == null) tarjetaTemplate = Resources.Load<VisualTreeAsset>("Tarjeta");

            root = GetComponent<UIDocument>().rootVisualElement;

            der = root.Q<VisualElement>("Dcha");
            VisualElement izq = root.Q<VisualElement>("Izda");
            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");
            botonCrear = root.Q<Button>("BotonCrear");
            toggleModificar = root.Q<Toggle>("ToggleModificar");

            individuos = Basedatos.getData();

            VisualElement header = root.Q<VisualElement>("header");

            imgs = header.Children().ToList();
            Assert.That(imgs.Count, Is.GreaterThan(0));

            foreach (Individuo individuo in individuos)
            {
                VisualElement fondo = header.Q<VisualElement>(individuo.ImageVeName);
                if (fondo != null) individuo.CambioImagenFondo(fondo);
            }

            //si hay tarjetas colocadas manualmente, les pone un indiv. default

            int individuosBase = individuos.Count();

            foreach (var item in der.Children())
            {
                if (item.ClassListContains("tarjeta"))
                {
                    Individuo individuoDefault = new Individuo("John", "Doe", new StyleBackground(Resources.Load<Sprite>("rickRueda")), "RicardoGira");
                    new Tarjeta(item, individuoDefault);
                    item.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                    individuos.Add(individuoDefault);
                }
            }


            //creo tantas tarjetas como individuos en LA LISTA

            for (int i = 0; i < individuosBase; i++)
            {
                VisualElement tarjetaVe = tarjetaTemplate.Instantiate();
                tarjetaVe.AddToClassList("tarjeta");
                tarjetaVe.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                new Tarjeta(tarjetaVe, individuos[i]); //se inicializa la tarjeta enlazándose el ve con los cambios en el individuo
                der.Add(tarjetaVe);

            }




            backgroundSeleccionado = imgs[0];
            foreach (VisualElement img in imgs) { img.RegisterCallback<ClickEvent>(CambioImg); }

            izq.Q<Button>("ButtonGuardar").RegisterCallback<ClickEvent>(GuardarInfoEnJson);
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
                Individuo indiv = new Individuo(input_nombre.value, input_apellido.value, new StyleBackground(backgroundSeleccionado.resolvedStyle.backgroundImage), backgroundSeleccionado.name);
                tarjetaVe.AddToClassList("tarjeta");
                tarjetas_borde_negro();
                tarjeta_borde_blanco(tarjetaVe);
                tarjetaVe.RegisterCallback<ClickEvent>(SeleccionTarjeta);
                new Tarjeta(tarjetaVe, indiv); //se inicializa la tarjeta enlazándose el ve con los cambios en el individuo
                der.Add(tarjetaVe);
                individuos.Add(indiv);
                //individuos.ForEach(indiv =>
                //{
                //    string jsonIndividuo = JsonUtility.ToJson(indiv);
                //    Debug.Log(jsonIndividuo);
                //});
                //string listaToJson = JsonHelperIndividuo.ToJson(individuos, true);
                //Debug.Log(listaToJson);
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
            imgs_borde_negro();

            int i = 0;
            while (i < imgs.Count && indivSeleccionado.ImagenFondo != imgs[i].resolvedStyle.backgroundImage) i++;
            if (i < imgs.Count) img_borde_blanco(imgs[i]);
        }
        void CambioImg(ClickEvent evt)
        {
            imgs_borde_negro();
            backgroundSeleccionado = evt.currentTarget as VisualElement;
            img_borde_blanco(backgroundSeleccionado);
            if (toggleModificar.value)
            {
                if (indivSeleccionado == null) return;
                indivSeleccionado.CambioImagenFondo(backgroundSeleccionado);
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
                VisualElement tarjeta = elem.Q("plantilla").Q<VisualElement>("cuadroInterior");

                tarjeta.style.borderBottomColor = Color.black;
                tarjeta.style.borderRightColor = Color.black;
                tarjeta.style.borderTopColor = Color.black;
                tarjeta.style.borderLeftColor = Color.black;
            });
        }

        void tarjeta_borde_blanco(VisualElement tar)
        {
            VisualElement tarjeta = tar.Q("plantilla").Q<VisualElement>("cuadroInterior");

            tarjeta.style.borderBottomColor = Color.white;
            tarjeta.style.borderRightColor = Color.white;
            tarjeta.style.borderTopColor = Color.white;
            tarjeta.style.borderLeftColor = Color.white;
        }

        void imgs_borde_negro()
        {

            imgs.ForEach(img =>
            {
                img.style.borderBottomColor = Color.black;
                img.style.borderRightColor = Color.black;
                img.style.borderTopColor = Color.black;
                img.style.borderLeftColor = Color.black;
            });
        }

        void img_borde_blanco(VisualElement img)
        {
            img.style.borderBottomColor = Color.white;
            img.style.borderRightColor = Color.white;
            img.style.borderTopColor = Color.white;
            img.style.borderLeftColor = Color.white;
        }

        void GuardarInfoEnJson(ClickEvent evt)
        {
            string json = JsonHelperIndividuo.ToJson(individuos, true);
            string ruta = Application.persistentDataPath + "/lista_individuos.json";
            System.IO.File.WriteAllText(ruta, json);

        }

    }
}

