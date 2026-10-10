using Godot;
using System;

public partial class ObiectIntercativ : Area2D
{
	private bool InArea = false;
	public bool HasHiddenObject = false;
	private bool ObjectFound = false;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public override void _Process(double delta)
	{
		if (InArea && !ObjectFound && Input.IsActionJustPressed("Interact"))
		{
			if (HasHiddenObject)
			{
				ObjectFound = true;
				GD.Print("Bravo! Ai gasit piatra mistica te poti intoarce acasa!");
			}
			else
			{
				GD.Print("Obiectul nu se afla aici!");
			}
		}
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			InArea = true;
		}
	}	
	private void OnBodyExited(Node2D body)
	{
		if (body.Name == "Player")
		{
			InArea = false;
		}
	}
}
