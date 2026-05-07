using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab3Manipulator : MouseManipulator
{
    public Lab3Manipulator()
    {
        activators.Add(new ManipulatorActivationFilter { button = UnityEngine.UIElements.MouseButton.RightMouse });
    }
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
    }

    private void OnMouseMove(MouseMoveEvent mev)
    {
        target.style.borderBottomColor = Color.white;
        target.style.borderLeftColor = Color.white;
        target.style.borderRightColor = Color.white;
        target.style.borderTopColor = Color.white;
    }
    //private void OnMouseDown(MouseDownEvent mev)
    //{
    //    Debug.Log(target.name + ": Click en Elenemto");
    //    if (CanStartManipulation(mev))
    //    {
    //        target.style.borderBottomColor = Color.white;
    //        target.style.borderLeftColor = Color.white;
    //        target.style.borderRightColor = Color.white;
    //        target.style.borderTopColor = Color.white;
    //        mev.StopPropagation();

    //    }
    //}
}
