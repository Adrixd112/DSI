using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;


public class UIScript : MonoBehaviour
{
    [SerializeField]
    Texture2D ricardo;
    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();
        VisualElement root = document.rootVisualElement;
        UQueryBuilder<VisualElement> builder = new(root);
        List<VisualElement> list = builder.ToList();
        list.ForEach(elem => Debug.Log("Elementos"+elem.name));


        List<Button> lista = root.Query(className: "shopListElem").Descendents<Button>().ToList();

        Debug.Log(root.Q(className: "shopListElem").name);

        lista.ForEach(button => { Debug.Log("Buttext:"+button.text); if (button.text.Length >= 5 && button.text.Substring(0, 2) == "LO" && button.text.Substring(button.text.Length-1, 1) == "G") { button.style.backgroundColor = Color.plum; } else { button.style.backgroundColor = Color.green; } });

        root.Query("ShopList", "simpleBlueBackground").ToList().ForEach(el => el.AddToClassList("ricardoRueda"));
    }
}
