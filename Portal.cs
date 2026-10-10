using Godot;
using System;

public partial class Portal : StaticBody2D
{
	private CollisionShape2D colision1;
	private CollisionShape2D colision2;
	private CollisionShape2D colision3;
	private CollisionShape2D colision4;
	private CollisionShape2D colision5;
	private CollisionShape2D colision6;
	private CollisionShape2D colision7;
	
	public override void _Ready()
	{
		colision1 = GetNode<CollisionShape2D>("CollisionShape2D");
		colision2 = GetNode<CollisionShape2D>("CollisionShape2D2");
		colision3 = GetNode<CollisionShape2D>("CollisionShape2D3");
		colision4 = GetNode<CollisionShape2D>("CollisionShape2D4");
		colision5 = GetNode<CollisionShape2D>("CollisionShape2D5");
		colision6 = GetNode<CollisionShape2D>("CollisionShape2D6");
		colision7 = GetNode<CollisionShape2D>("CollisionShape2D7");
	}

	public override void _Process(double delta)
	{
		var node = GetTree().CurrentScene as Level1;
		
		if (node.found != 5)
		{
			colision1.Disabled = true;
			colision2.Disabled = true;
			colision3.Disabled = true;
			colision4.Disabled = true;
			colision5.Disabled = true;
			colision6.Disabled = true;
			colision7.Disabled = true;
		}
		else
		{
			colision1.Disabled = false;
			colision2.Disabled = false;
			colision3.Disabled = false;
			colision4.Disabled = false;
			colision5.Disabled = false;
			colision6.Disabled = false;
			colision7.Disabled = false;
		}
	}
}
