using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab5 : MonoBehaviour
{
    VisualElement plantilla;

    TextField input_nombre;
    TextField input_apellido;
    
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        plantilla = root.Q("plantilla");
        input_nombre = root.Q<TextField>("InputNombre");
        input_apellido = root.Q<TextField>("InputApellido");

        VisualElement izq = root.Q<VisualElement>("Izda"); //para debug es más fácil no hacer todos los Q<>() en la misma línea
        List <VisualElement> imgs = izq.Q<VisualElement>("header").Children().ToList();

        foreach (VisualElement img in imgs) { img.RegisterCallback<ClickEvent>(CambioImg); }
        plantilla.RegisterCallback<ClickEvent>(SeleccionIndividuo);
        input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
        input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);
    }

    void CambioImg(ClickEvent evt)
    {
        VisualElement imgACambiar = plantilla.Q<VisualElement>("top");
        imgACambiar.style.backgroundImage = new StyleBackground((evt.currentTarget as VisualElement).resolvedStyle.backgroundImage);
    }
    void SeleccionIndividuo(ClickEvent evt)
    {
        string nombre = plantilla.Q<Label>("Nombre").text;
        string apellido = plantilla.Q<Label>("Apellido").text;

        Debug.Log(nombre);

        input_nombre.SetValueWithoutNotify(nombre);
        input_apellido.SetValueWithoutNotify(apellido);
    }

    void CambioNombre(ChangeEvent<string> evt)
    {
        Label nombreLabel = plantilla.Q<Label>("Nombre");
        nombreLabel.text = evt.newValue;
    }

    void CambioApellido(ChangeEvent<string> evt)
    {
        Label apellidoLabel = plantilla.Q<Label>("Apellido");
        apellidoLabel.text = evt.newValue;
    }
}

