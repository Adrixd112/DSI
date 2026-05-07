using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class Lab3 : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement izda = root.Q("Izda");
        VisualElement dcha = root.Q("Dcha");

        List<VisualElement> lveizda = izda.Children().ToList();
        List<VisualElement> lvedcha = dcha.Children().ToList();

        izda.RegisterCallback<MouseMoveEvent>(ev => 
        lveizda.ForEach(el =>{
            //borro el inlined style que he añadido por si acaso a alguien le da hacer que los
            //bordes de las casillas sean de otro color que no sea negro.
            el.style.borderBottomColor = StyleKeyword.Null;
            el.style.borderLeftColor = StyleKeyword.Null;
            el.style.borderRightColor = StyleKeyword.Null;
            el.style.borderTopColor = StyleKeyword.Null;
        }),TrickleDown.TrickleDown);

        dcha.RegisterCallback<MouseMoveEvent>(ev =>
        lvedcha.ForEach(el => {
            //borro el inlined style que he añadido por si acaso a alguien le da hacer que los
            //bordes de las casillas sean de otro color que no sea negro.
            el.style.borderBottomColor = StyleKeyword.Null;
            el.style.borderLeftColor = StyleKeyword.Null;
            el.style.borderRightColor = StyleKeyword.Null;
            el.style.borderTopColor = StyleKeyword.Null;
        }), TrickleDown.TrickleDown);

        izda.AddManipulator(new ExampleResizer());

        lvedcha.ForEach(el => el.AddManipulator(new Lab3Manipulator()));

        lveizda.ForEach(el => el.AddManipulator(new Lab3Manipulator()));



        //izda.RegisterCallback<MouseDownEvent>(ev =>
        //{
        //    Debug.Log("Contenedor Izquierda. Fase: " + ev.propagationPhase);
        //    Debug.Log("Contenedor Izquierda. Target: " + (ev.target as VisualElement).name);
        //    (ev.target as VisualElement).style.backgroundColor = Color.green;
        //},TrickleDown.TrickleDown);

        //dcha.RegisterCallback<MouseDownEvent>(ev =>
        //{
        //    Debug.Log("Contenedor Derecha. Fase: " + ev.propagationPhase);
        //    Debug.Log("Contenedor Derecha. Target: " + (ev.target as VisualElement).name);
        //    (ev.target as VisualElement).style.backgroundColor = Color.red;
        //}, TrickleDown.TrickleDown);
    }
}
