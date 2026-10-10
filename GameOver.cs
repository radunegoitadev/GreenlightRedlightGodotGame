using Godot;
using System;

public partial class GameOver : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var audioplayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		audioplayer.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Reload"))
		{
			GetTree().ChangeSceneToFile("res://node_2d.tscn");
		}
	}
}
