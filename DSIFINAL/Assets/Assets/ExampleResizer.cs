using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ExampleResizer : PointerManipulator
{
    bool rightClickPressed;
    public ExampleResizer()
    {
        rightClickPressed = false;
        activators.Add(new ManipulatorActivationFilter { button = UnityEngine.UIElements.MouseButton.RightMouse });

    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<WheelEvent>(OnWheelRotate);
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<WheelEvent>(OnWheelRotate);
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnWheelRotate(WheelEvent e)
    {
        if (rightClickPressed)
        {
            Vector2 startSize = target.layout.size;

            target.style.height = startSize.y + 2 * e.delta.y;
            target.style.width = startSize.x + 2 * e.delta.y;

            //esto para q esté centrado ya que el origen del ve es la esquina sup. izq.
            target.style.top = target.layout.y - e.delta.y;
            target.style.left = target.layout.x - e.delta.y;

            e.StopPropagation();
        }
    }
    protected void OnMouseDown(MouseDownEvent e)
    {
        if (CanStartManipulation(e)) rightClickPressed = true;
        e.StopPropagation();
    }

    protected void OnMouseUp(MouseUpEvent e)
    {
        if (CanStartManipulation(e)) rightClickPressed = false;
    }
}