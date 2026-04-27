using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using System;

[UxmlElement]
public partial class Lab4d : VisualElement
{

    VisualElement vidasRoot = new VisualElement();
    const int HPMAX = 5;
    int hp = 3;
    string vidaLLenaImg = "corazon-lleno";
    string vidaVaciaImg = "corazon-vacio";

    [UxmlAttribute("hp")]
    public int Hp
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, HPMAX);
            updatearVidas();
        }
    }
    [UxmlAttribute("sprite-full-hp-img")]
    public string VidaLLenaImg
    {
        get => vidaLLenaImg;
        set
        {
            vidaLLenaImg = value;
            Debug.Log(value);
            updatearVidas();
        }
    }
    [UxmlAttribute("sprite-empty-hp-img")]
    public string VidaVaciaImg
    {
        get => vidaVaciaImg;
        set
        {
            vidaVaciaImg = value;
            Debug.Log(value);
            updatearVidas();
        }
    }
    void updatearVidas()
    {
        Sprite vidaLLena = Resources.Load<Sprite>( vidaLLenaImg );
        Sprite vidaVacia = Resources.Load<Sprite>(vidaVaciaImg );
        int i = 0;
        List<VisualElement> elemList = vidasRoot.Children().ToList();
        for(; i<hp;i++){
            elemList[i].style.backgroundImage = new StyleBackground(vidaLLena);
        }
        for (; i < HPMAX; i++)
        {
            elemList[i].style.backgroundImage = new StyleBackground(vidaVacia);
        }
    }

   
    public Lab4d()
    {

        for(int i = 0; i < HPMAX; i++)
        {
            VisualElement ve = new VisualElement();
            ve.style.height = 50;
            ve.style.width = 50;
            vidasRoot.Add(ve);
        }

        vidasRoot.style.height = 50;
        vidasRoot.style.flexDirection = FlexDirection.Row;
        hierarchy.Add(vidasRoot);


    }

   
}