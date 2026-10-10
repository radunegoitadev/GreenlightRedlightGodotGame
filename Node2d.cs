using Godot;
using System;
using System.Collections.Generic;

public partial class Node2d : Node2D
{
	private Timer _timer;
	private Timer pauseTimer;
	private Timer warningTimer;
	private Timer stopTimer;
	private Timer blinkTimer;
	private StaticBody2D portal;
	
	private AudioStreamPlayer2D blinkSound;
	
	private Label timeLabel;
	public Label objectsLabel;
	
	private ColorRect fadeOut;
	private TextureRect warning;
	private TextureRect stop;
	private TextureRect battery; 
	
	public bool InSafeArea = false;
	
	public int found = 0;
	
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		pauseTimer = GetNode<Timer>("PauseTimer");
		warningTimer = GetNode<Timer>("WarningTimer");
		stopTimer = GetNode<Timer>("StopTimer");
		blinkTimer = GetNode<Timer>("BlinkTimer");
		portal = GetNode<StaticBody2D>("Portal");
		
		timeLabel = GetNode<Label>("TextTimer/LabelTime");
		objectsLabel = GetNode<Label>("TextObjects/LabelObjects");
		
		fadeOut = GetNode<ColorRect>("TextTimer/ColorRect");
		warning = GetNode<TextureRect>("PhaseSign/Warning");
		stop = GetNode<TextureRect>("PhaseSign/Stop");
		battery = GetNode<TextureRect>("TextObjects/Battery");
		
		_timer.Timeout += OnTimeExpired;
		pauseTimer.Timeout += WarningPhase;
		warningTimer.Timeout += StopPhase;
		stopTimer.Timeout += PausePhase;
		blinkTimer.Timeout += WarningBlink;
		
		blinkSound = GetNode<AudioStreamPlayer2D>("BlinkTimer/BlinkSound");
		
		PausePhase();
		HideObjects(5);
		portal.Visible = false;
	}
	
	public override void _Process(double delta)
	{
		double remainingSeconds = Math.Round(_timer.TimeLeft);
		timeLabel.Text = "Time Remaining: " + remainingSeconds.ToString();
		
		if ( !InSafeArea && !stopTimer.IsStopped())
		{
			die();
		}
		
		if(found == 5)
		{
			objectsLabel.Text = "Recharged";
			portal.Visible = true;
		}
	}
	
	private void PausePhase()
	{
		warning.Visible = false;
		stop.Visible = false;
		
		pauseTimer.WaitTime = (float)GD.RandRange(10.3,11.1);
		pauseTimer.Start();
		
	}
	
	private void WarningPhase()
	{
		blinkTimer.Start();
		
		warningTimer.WaitTime = (float)GD.RandRange(4.3,7.2);
		
		warningTimer.Start();
	}
	
	private void WarningBlink()
	{
		warning.Visible = !warning.Visible;
		blinkSound.Play();
	}
	
	private void StopPhase()
	{
		blinkTimer.Stop();
		
		warning.Visible = false;
		stop.Visible = true;
		
		stopTimer.WaitTime = (float)GD.RandRange(2.1,3.3);
		
		stopTimer.Start();
	}
	
	private void OnTimeExpired()
	{
		GD.Print("Game Over");
		timeLabel.Text = "Game Over";
		
		if (found != 5)
		{
			die();
		}
		else
		{
			GetTree().ReloadCurrentScene();
			found = 0;
		}
	}
	
	private void HideObjects(int number)
	{
		var HidingNodes = GetTree().GetNodesInGroup("HidingSpots");
		List<Node> AvailableList = new List<Node>(HidingNodes);
		
		if (AvailableList.Count < number)
		{
			GD.PrintErr("No way to hide the objects!");
			number = AvailableList.Count;
		}
		
		Random random = new Random();
		
		for (int i = 0; i < number; i++)
		{
			int ChosenIndex = random.Next(AvailableList.Count);
			
			var HiddenSpot = AvailableList[ChosenIndex] as SearchZone;
			
			if (HiddenSpot != null)
			{
				HiddenSpot.HasHiddenObject = true;
			}
			
			AvailableList.RemoveAt(ChosenIndex);
		}
	}
	
	public void die()
	{
		timeLabel.Visible = false;
		objectsLabel.Visible = false;
		warning.Visible = false;
		stop.Visible = false;
		battery.Visible = false;
		
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(fadeOut, "color", new Color(0,0,0,1), 1.0f);
		tween.TweenCallback(Callable.From(() => GetTree().ChangeSceneToFile("res://game_over.tscn")));
	}
}
