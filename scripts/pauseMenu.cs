using Godot;
using System;

public partial class pauseMenu : Control
{
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("pause"))
		{
			GD.Print("Pausado");
			Visible = !GetTree().Paused; 
			GetTree().Paused = !GetTree().Paused;
		}
	}
}
