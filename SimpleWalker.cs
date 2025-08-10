using Godot;
using System;

public partial class SimpleWalker : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float MouseSensitivity = 0.002f;

    private Camera3D _camera;

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("Camera3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);
            _camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
            _camera.RotationDegrees = new Vector3(Mathf.Clamp(_camera.RotationDegrees.X, -90f, 90f), 0, 0);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        Velocity = direction * Speed;
        MoveAndSlide();
    }
}