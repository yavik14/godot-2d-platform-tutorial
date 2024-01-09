using Godot;
using System;

public partial class player : CharacterBody2D
{
	private const int MoveSpeed = 25;
	private const int MaxSpeed = 50;
	
	private const int JumpHeight = -250;
	private const int Gravity = 10;

	private Vector2 Up = new Vector2(0, -1);
	private Vector2 Motion = new Vector2();
	
	int lives = 3;
	
	private Sprite2D sprite;
	private AnimationPlayer animationPlayer;

	[Signal]
	public delegate void LifeEventHandler(int Lives);
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{	
		Motion.Y += Gravity;
		var friction = false;
		
		if(Input.IsActionPressed("ui_right"))
		{
			sprite.FlipH = true;
			animationPlayer.Play("walk");
			Motion.X = Mathf.Min(Motion.X + MoveSpeed, MaxSpeed);
		}
		else if(Input.IsActionPressed("ui_left"))
		{ 
			sprite.FlipH = false;
			animationPlayer.Play("walk");
			Motion.X = Mathf.Max(Motion.X - MoveSpeed, -MaxSpeed);
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
				Motion.Y = JumpHeight;
			}
			if(friction == true){
				Motion.X = (float)Mathf.Lerp(Motion.X, 0, 0.5);
			}
		}
		else {
			if(friction == true)
			{
				Motion.X = (float)Mathf.Lerp(Motion.X, 0, 0.01);
			}
		}
		
		Velocity = Motion;
		MoveAndSlide();
	}

	private void OnSawCollision()
	{
		GD.Print("Nos hemos pinchado");
		LooseLife();
	}	

	private void OnEnemyCollision(int PositionX)
	{
		GD.Print("Me mato el rojo");
		LooseLife((Int32)PositionX);
	}
	
	private void LooseLife(Int32? EnemyPosX = null)
	{
		if(EnemyPosX != null)
		{
			if(Position.X < EnemyPosX)
			{
				Motion.X = -200;
				Motion.Y = -100;
			}
			
			if(Position.X > EnemyPosX)
			{
				Motion.X = 200;
				Motion.Y = -100;
			}
		}
		lives--;
		EmitSignal(SignalName.Life, lives);
		GD.Print("Perdemos vida. Vida actual: " + lives.ToString());

		if(lives <= 0)
		{
			GetTree().ReloadCurrentScene();
		}
	}
}

