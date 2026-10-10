using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private AudioStreamPlayer2D audioPlayer;
	
	public const float Speed = 60.0f;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		audioPlayer = GetNode<AudioStreamPlayer2D>("AnimatedSprite2D/AudioStreamPlayer2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction != Vector2.Zero)
		{
			if (direction.X != 0) 
			{
				_animatedSprite.Play("side_walk");
				_animatedSprite.FlipH = direction.X < 0; 
			}
			else if (direction.Y != 0) 
			{
				if (direction.Y < 0)
				{
					_animatedSprite.Play("back_walk");
				}
				else
				{
					_animatedSprite.Play("walk");
					_animatedSprite.FlipH = false;
				} 
			}
		
			Velocity = direction * Speed;
		}
		else
		{
			Velocity = Vector2.Zero;
			_animatedSprite.Stop();
			_animatedSprite.Frame = 0;
		}

		MoveAndSlide();
	}
	
	private void _on_animated_sprite_2d_frame_changed()
	{
		if (_animatedSprite.Animation == "walk" || _animatedSprite.Animation == "back_walk")
		{
			if (_animatedSprite.Frame == 0 || _animatedSprite.Frame == 2)
			{
				audioPlayer.PitchScale = (float)GD.RandRange(1, 1.2);
			}
		}
		
		if (_animatedSprite.Animation == "side_walk")
		{
			if ( _animatedSprite.Frame == 0 || _animatedSprite.Frame == 3)
			{
				audioPlayer.PitchScale = (float)GD.RandRange(1, 1.2);
			}
		}
		
		if (GetTree().CurrentScene.Name == "level1")
		{
			audioPlayer.Play();
		}
	}
}
