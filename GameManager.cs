using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
    // This makes our GameManager accessible from anywhere in the game.
    public static GameManager Instance { get; private set; }

    // A list of all our mini-game scenes, in order.
    [Export]
    private PackedScene[] _gameLevels;

    private int _currentLevelIndex = -1;

    public override void _Ready()
    {
        // The Singleton pattern.
        Instance = this;
        
        // Start the experience
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex < _gameLevels.Length)
        {
            // Unload the previous scene if one exists
            foreach (var child in GetTree().Root.GetChildren())
            {
                if (child is Node && child.Owner != null)
                {
                    // A simple way to find and free the main scene content.
                    // Be careful if you have other complex nodes at the root.
                    if (child.Name == "Level")
                        child.QueueFree();
                }
            }

            // Load and instance the new scene
            var nextLevelScene = _gameLevels[_currentLevelIndex].Instantiate();
            nextLevelScene.Name = "Level"; // Give it a consistent name
            GetTree().Root.AddChild(nextLevelScene);

            GD.Print($"Loaded level: {_currentLevelIndex}");
        }
        else
        {
            GD.Print("All levels completed! Back to main menu or quit.");
            GetTree().Quit(); // Or load a "Thanks for Playing" screen.
        }
    }
}