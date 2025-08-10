using Godot;
using System;

public partial class Level01_Walking : Node3D
{
    private void OnArea3DBodyEntered(Node3D body)
    {
        if (body is SimpleWalker)
        {
                GD.Print("Player reached the end of Level 1!");
            GameManager.Instance.LoadNextLevel();
        }
    }
}