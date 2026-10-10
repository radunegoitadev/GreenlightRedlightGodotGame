using Godot;
using System;

public partial class SearchZone : Area2D
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
		var audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		var node = GetTree().CurrentScene as Level1;
		
		if (InArea && !ObjectFound && Input.IsActionJustPressed("Interact"))
		{
			if (HasHiddenObject)
			{
				ObjectFound = true;
				GD.Print("Bravo! Ai gasit piatra mistica te poti intoarce acasa!");
				audioPlayer.Play();
				node.found++;
				node.objectsLabel.Text = node.found + "/5";
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
