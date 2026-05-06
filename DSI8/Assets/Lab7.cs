using UnityEngine;
using UnityEngine.UIElements;
public class Lab7 : MonoBehaviour
{
    VisualElement botonVerde;
    VisualElement contenidoVerde;
    VisualElement botonAzul;
    VisualElement contenidoAzul;
    VisualElement botonMorado;
    VisualElement contenidoMorado;

    void NoContenido()
    {
        contenidoAzul.style.display = DisplayStyle.None;
        contenidoVerde.style.display = DisplayStyle.None;
        contenidoMorado.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        UIDocument uidoc = GetComponent<UIDocument>();
        VisualElement rootve = uidoc.rootVisualElement;
        VisualElement contenido = rootve.Q<VisualElement>("Contenido");
        VisualElement botones = rootve.Q<VisualElement>("Botones");
        botonVerde = botones.Q<VisualElement>("Verde");
        botonAzul = botones.Q<VisualElement>("Azul");
        botonMorado = botones.Q<VisualElement>("Fucsia");
        contenidoVerde = contenido.Q<VisualElement>("Verde");
        contenidoAzul = contenido.Q<VisualElement>("Azul");
        contenidoMorado = contenido.Q<VisualElement>("Fucsia");

        botonVerde.RegisterCallback<MouseDownEvent>(evt =>
        {
            Debug.Log("Pestaña verde");
            NoContenido();
            contenidoVerde.style.display = DisplayStyle.Flex;
        });
        botonAzul.RegisterCallback<MouseDownEvent>(evt =>
        {
            Debug.Log("Pestaña azul");
            NoContenido();
            contenidoAzul.style.display = DisplayStyle.Flex;
        });
        botonMorado.RegisterCallback<MouseDownEvent>(evt =>
        {
            Debug.Log("Pestaña morada");
            NoContenido();
            contenidoMorado.style.display = DisplayStyle.Flex;
        });

        Label textoVerde = contenidoVerde.Q<Label>("texto");
        textoVerde.text = @"<line-indent=15%>En un lugar de <smallcaps>La Mancha</smallcaps> </line-indent><br>
de cuyo nombre <rotate=""45"">no quiero acordarme</rotate>,
<b><color=""black""><gradient=""rojoverde"">no hacia mucho que vivia un hidalgo</gradient></b>
de los de lanza en astillero,
<b><color=""black""><gradient=""aaaa"">adarga antigua</gradient></b>,
<i>rocin flaco y galgo corredor.";


    }


}
