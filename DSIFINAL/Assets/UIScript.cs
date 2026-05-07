using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static UnityEditor.Progress;


public class UIScript : MonoBehaviour
{
    Dictionary<string, ItemInfo> diccionarioItems;

    TiendaInfo tiendaInfo;
    UserInfo userInfo;

    SlotInfo catalogoSelected; //de donde viene el comprado
    SlotInfo userInvItemSlot; //a donde va el vendido



    VisualElement ventanaCatalogo;
    VisualElement ventanaCatalogoItemInfo;

    VisualElement ventanaCarrito;

    private void OnEnable()
    {
        GenerateItems();
        GenerateTienda();
        GenerateUser();

        VisualTreeAsset invElemTemplate = Resources.Load<VisualTreeAsset>("InventoryElement");

        UIDocument document = GetComponent<UIDocument>();
        VisualElement root = document.rootVisualElement;
        ventanaCarrito = root.Q<VisualElement>("CartWindow");

        ventanaCatalogo = root.Q<VisualElement>("CatalogueWindow");



        VisualElement header = root.Q<VisualElement>("header");
        header.Q<Button>("CatalogueButton").clicked += () => 
        { 
            ventanaCatalogo.style.display = DisplayStyle.Flex; 
            ventanaCarrito.style.display = DisplayStyle.None; 
        };
        header.Q<Button>("CartButton").clicked += () =>
        {
            ventanaCatalogo.style.display = DisplayStyle.None;
            ventanaCarrito.style.display = DisplayStyle.Flex;
        };



        ventanaCatalogoItemInfo = ventanaCatalogo.Q<VisualElement>("ItemInfoWindow");

        UnsignedIntegerField nUnitsTextField = ventanaCatalogoItemInfo.Q<UnsignedIntegerField>("NUnitsTextField");
        nUnitsTextField.RegisterValueChangedCallback(evt =>
        {
            uint clamped = (uint)Mathf.Clamp(evt.newValue, 0, catalogoSelected.cantidad_U);

            if (clamped != evt.newValue)
            {
                nUnitsTextField.SetValueWithoutNotify(clamped);
            }
            UpdateItemDescriptionCatalogo();
        });


        Button butt = ventanaCatalogoItemInfo.Q<Button>("BuyNButton");
        butt.clicked += () => BuyButtonCallback(catalogoSelected, (int)nUnitsTextField.value);

        SliderInt bulkBuySlider = ventanaCatalogoItemInfo.Q<SliderInt>("NBoxesSlider");
        bulkBuySlider.RegisterValueChangedCallback(evt =>
        {
            int clamped = Mathf.Clamp(evt.newValue, 0, bulkBuySlider.highValue);

            if (clamped != evt.newValue)
            {
                bulkBuySlider.SetValueWithoutNotify(clamped);
            }
            UpdateItemDescriptionCatalogo();
        });
        
        butt = ventanaCatalogoItemInfo.Q<Button>("BulkBuyButton");
        butt.clicked += () => BuyBoxButtonCallback(catalogoSelected, (int)bulkBuySlider.value);


        ListView lv = ventanaCatalogo.Q<VisualElement>("Catalogue").Q<ListView>();

        lv.itemsSource = tiendaInfo.catalogoItems;

        lv.makeItem = () =>
        {

            VisualElement item = invElemTemplate.Instantiate();
            item.style.paddingBottom = 4;

            item.RegisterCallback<ClickEvent>(SeleccionItemCatalogo);
            item.Query()
               .Descendents<VisualElement>()
               .ForEach(elem => elem.pickingMode = PickingMode.Ignore);
            item.pickingMode = PickingMode.Position;
            return item;
        };

        lv.bindItem = (element, index) =>
        {
            element.userData = tiendaInfo.catalogoItems[index];

            element.Q<Label>("Name").text =diccionarioItems[(element.userData as SlotInfo).key].nombre;

            element.Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(
                Resources.Load<Sprite>(diccionarioItems[(element.userData as SlotInfo).key].imgUrl));

        };

        lv = ventanaCarrito.Q<VisualElement>("Inventory").Q<ListView>();

        lv.itemsSource = userInfo.inventario;

        lv.makeItem = () =>
        {

            VisualElement item = invElemTemplate.Instantiate();
            item.style.paddingBottom = 4;

            item.RegisterCallback<ClickEvent>(SeleccionItemCatalogo);
            item.Query()
               .Descendents<VisualElement>()
               .ForEach(elem => elem.pickingMode = PickingMode.Ignore);
            item.pickingMode = PickingMode.Position;
            return item;
        };

        lv.bindItem = (element, index) =>
        {
            element.userData = tiendaInfo.catalogoItems[index];

            element.Q<Label>("Name").text = diccionarioItems[(element.userData as SlotInfo).key].nombre;

            element.Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(
                Resources.Load<Sprite>(diccionarioItems[(element.userData as SlotInfo).key].imgUrl));

        };

        UpdateItemDescriptionCatalogo();
    }

    void updateListVes()
    {
        ventanaCatalogo.Q<VisualElement>("Catalogue").Q<ListView>().RefreshItems();
        ventanaCarrito.Q<VisualElement>("Inventory").Q<ListView>().RefreshItems();
    }
    void GenerateItems()
    {
        diccionarioItems = new Dictionary<string, ItemInfo>();
        diccionarioItems.Add("log",new ItemInfo("log","Log","log",10,500,80));
        diccionarioItems.Add("full_heart", new ItemInfo("full_heart", "Heart", "corazon-lleno", 10250, 950000, 25));
        diccionarioItems.Add("empty_heart", new ItemInfo("empty_heart", "Stuffed heart", "corazon-vacio", 1200, 8000, 10));
    }

    void GenerateTienda()
    {
        tiendaInfo = new TiendaInfo();
        tiendaInfo.catalogoItems.Add(new SlotInfo("log",15,50));
        tiendaInfo.catalogoItems.Add(new SlotInfo("empty_heart", 3, 2));
        tiendaInfo.catalogoItems.Add(new SlotInfo("full_heart", 2, 0));
    } 
    void GenerateUser()
    {
        userInfo = new UserInfo(21400);
    }
    void SeleccionItemCatalogo(ClickEvent evt)
    {
        VisualElement itemVe = evt.target as VisualElement;
        SlotInfo slotInfo = itemVe.userData as SlotInfo;
        userInvItemSlot = userInfo.inventario.Find(slot => slot.key == slotInfo.key);
        catalogoSelected = slotInfo;

        UpdateItemDescriptionCatalogo();
    }

    void BuyButtonCallback(SlotInfo slot, int ammount)
    {
        if (ammount >= slot.cantidad_U)
        {
            slot.cantidad_U -= ammount;
            if(userInvItemSlot == null)
            {
                userInvItemSlot = new SlotInfo(slot.key);
                userInfo.inventario.Add(userInvItemSlot); 
            }
            userInvItemSlot.cantidad_U += ammount;
            UpdateItemDescriptionCatalogo();
        }
    }
    void BuyBoxButtonCallback(SlotInfo slot, int ammount)
    {
        if (ammount >= slot.cantidad_Cajas)
        {
            slot.cantidad_Cajas -= ammount;
            if (userInvItemSlot == null)
            {
                userInvItemSlot = new SlotInfo(slot.key);
                userInfo.inventario.Add(userInvItemSlot);
            }
            userInvItemSlot.cantidad_Cajas += ammount;
            UpdateItemDescriptionCatalogo();
        }
    }
    void UpdateItemDescriptionCatalogo()
    {
        
        if (catalogoSelected == null) { ventanaCatalogoItemInfo.Q<VisualElement>("InvisiBoy").style.display = DisplayStyle.None; return; }
        else { ventanaCatalogoItemInfo.Q<VisualElement>("InvisiBoy").style.display = DisplayStyle.Flex; }


        ItemInfo itemSelected = diccionarioItems[catalogoSelected.key];

        Label name = ventanaCatalogoItemInfo.Q<Label>("Name");
        Label unitPriceText = ventanaCatalogoItemInfo.Q<Label>("UnitPriceText");
        Label boxPriceText = ventanaCatalogoItemInfo.Q<Label>("BoxPriceText");
        Label bulkDiscountText = ventanaCatalogoItemInfo.Q<Label>("BulkDiscountText");
        Label existancesText = ventanaCatalogoItemInfo.Q<Label>("ExistancesText");
        Label unitsOwnedText = ventanaCatalogoItemInfo.Q<Label>("UnitsOwnedText");
        VisualElement image = ventanaCatalogoItemInfo.Q<VisualElement>("Image");
        VisualElement retailBuyMenu = ventanaCatalogoItemInfo.Q<VisualElement>("RetailBuyMenu");
        UnsignedIntegerField nUnitsTextField = ventanaCatalogoItemInfo.Q<UnsignedIntegerField>("NUnitsTextField");
        SliderInt bulkBuySlider = ventanaCatalogoItemInfo.Q<SliderInt>("NBoxesSlider");

        name.text = itemSelected.nombre;
        unitPriceText.text = "Unit price: "+itemSelected.precioXunidad.ToString();
        boxPriceText.text = $"Box price ({itemSelected.cantidadXCaja} item{(itemSelected.cantidadXCaja > 1 ? "s" : "")} per box): {itemSelected.precioXCaja}";
        bulkDiscountText.text = $"Bulk buy discount: {((1 - (itemSelected.precioXCaja / (itemSelected.precioXunidad * itemSelected.cantidadXCaja))) * 100f).ToString()} %";
        image.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(itemSelected.imgUrl));
        existancesText.text = $"Existances: {catalogoSelected.cantidad_Cajas} box{(catalogoSelected.cantidad_Cajas > 1 ? "es" : "")}, {catalogoSelected.cantidad_U} unit{(catalogoSelected.cantidad_U > 1 ? "s" : "")}";
       
        unitsOwnedText.text = $"Boxes owned: {(userInvItemSlot!=null ? userInvItemSlot.cantidad_Cajas : 0)}, " +
            $"units owned: {(userInvItemSlot != null ? userInvItemSlot.cantidad_U: 0)} u. Total: {(userInvItemSlot != null ? userInvItemSlot.cantidad_Cajas*itemSelected.cantidadXCaja + userInvItemSlot.cantidad_U : 0)} u";

        Button butt = ventanaCatalogoItemInfo.Q<Button>("BuyNButton");
        if (catalogoSelected.cantidad_U >= 1) { butt.text = $"Add {nUnitsTextField.value} unit{(nUnitsTextField.value>1? "s":"")} to cart for {nUnitsTextField.value * itemSelected.precioXunidad}"; }
        else { butt.text = "Out of existances"; }


        butt = ventanaCatalogoItemInfo.Q<Button>("BulkBuyButton");
        if (catalogoSelected.cantidad_Cajas >= 1)
        {
            butt.text = $"Add {bulkBuySlider.value} box{(bulkBuySlider.value > 1 ? "es" : "")} of {itemSelected.cantidadXCaja} item{(itemSelected.cantidadXCaja > 1 ? "s" : "")} to cart";
        }
        else
        {
            butt.text = "Out of existances";
        }

        bulkBuySlider.highValue = catalogoSelected.cantidad_Cajas;
    }
}
