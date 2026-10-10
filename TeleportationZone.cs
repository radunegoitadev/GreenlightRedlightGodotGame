using Godot;
using System;

public partial class TeleportationZone : Area2D
{
	private CollisionShape2D colision;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		colision = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	public override void _Process(double delta)
	{
		var node = GetTree().CurrentScene as Level1;
		
		if (node.found != 5)
		{
			colision.Disabled = true;
		}
		else
		{
			colision.Disabled = false;
		}
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			GetTree().ChangeSceneToFile("res://level_2.tscn");
		}
	}
}
