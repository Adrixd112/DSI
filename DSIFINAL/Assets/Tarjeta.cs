using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



    public class Tarjeta
    {
        ItemInfo miItem;


        Label nombreLabel;

        Label apellidoLabel;

        VisualElement imagen;

        public Tarjeta(VisualElement tarjetaRoot, ItemInfo item)
        {
            this.miItem = item;

            nombreLabel = tarjetaRoot.Q<Label>("Name");
            imagen = tarjetaRoot.Q<VisualElement>("Image");
            tarjetaRoot.userData = miItem;

            tarjetaRoot.Query()
                .Descendents<VisualElement>()
                .ForEach(elem => elem.pickingMode = PickingMode.Ignore);

            UpdateUI();

        }
        void UpdateUI()
        {
            nombreLabel.text = miItem.nombre;
            imagen.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(miItem.imgUrl));

        }
    }

