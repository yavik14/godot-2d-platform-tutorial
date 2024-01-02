using Godot;
using System;

public partial class player : CharacterBody2D
{
	private const int MoveSpeed = 25;
	private const int MaxSpeed = 50;
	
	private const int JumpHeight = -200;
	private const int Gravity = 10;

	private Vector2 up = new Vector2(0, -1);
	private Vector2 motion = new Vector2();
	
	private Sprite2D sprite;
	private AnimationPlayer animationPlayer;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{	
		motion.Y += Gravity;
		var friction = false;
		
		if(Input.IsActionPressed("ui_right"))
		{
			sprite.FlipH = true;
			animationPlayer.Play("walk");
			motion.X = Mathf.Min(motion.X + MoveSpeed, MaxSpeed);
		}
		else if(Input.IsActionPressed("ui_left"))
		{ 
			sprite.FlipH = false;
			animationPlayer.Play("walk");
			motion.X = Mathf.Max(motion.X - MoveSpeed, -MaxSpeed);
		}
		else 
		{
			animationPlayer.Play("idle");
			friction = true;
		}
		
		if (IsOnFloor())
		{
			if(Input.IsActionPressed("ui_accept"))
			{
				motion.Y = JumpHeight;
			}
			if(friction == true){
				motion.X = (float)Mathf.Lerp(motion.X, 0, 0.5);
			}
		}
		else {
			if(friction == true)
			{
				motion.X = (float)Mathf.Lerp(motion.X, 0, 0.01);
			}
		}
		
		Velocity = motion;
		MoveAndSlide();
	}
	
}

