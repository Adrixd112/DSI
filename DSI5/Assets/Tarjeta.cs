using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


namespace Lab5b_namespace
{
    public class Tarjeta
    {
        Individuo miIndividuo;

        VisualElement tarjetaRoot;

        Label nombreLabel;

        Label apellidoLabel;

        VisualElement imagen;

        public Tarjeta(VisualElement tarjetaRoot, Individuo individuo)
        {
            this.tarjetaRoot = tarjetaRoot;
            this.miIndividuo = individuo;

            nombreLabel = tarjetaRoot.Q<Label>("Nombre");
            apellidoLabel = tarjetaRoot.Q<Label>("Apellido");
            imagen = tarjetaRoot.Q<VisualElement>("top");
            tarjetaRoot.userData = miIndividuo;

            tarjetaRoot
                .Query(className: "tarjeta")
                .Descendents<VisualElement>()
                .ForEach(elem=>elem.pickingMode = PickingMode.Ignore);

            UpdateUI();

            miIndividuo.Cambio += UpdateUI;

        }
         void UpdateUI()
        {
        nombreLabel.text = miIndividuo.Nombre;
        apellidoLabel.text = miIndividuo.Apellido;  
        imagen.style.backgroundImage = new StyleBackground(miIndividuo.Foto);

        }
    }
}
