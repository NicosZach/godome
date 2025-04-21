using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 400.0f;
	private const float ShootingSpeed = 1.0f;
	private double _shootingCd = 1.0;
	[Export]
	public PackedScene BulletScene { get; set; }

	[Export] public Marker2D Gun;

	public override void _Process(double delta)
	{	
		var mousePosition = GetGlobalMousePosition();
		_shootingCd -= delta;
		if (Input.IsActionPressed("shoot") && _shootingCd <= 0.0f)
		{
			Shoot();
			_shootingCd = ShootingSpeed;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);

		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void Shoot()
	{
		if (BulletScene != null)
		{
			GD.Print("creating bullet scene");
			Bullet bullet = BulletScene.Instantiate<Bullet>();
			GetTree().CurrentScene.AddChild(bullet);

			GD.Print("setting bullet position");
			Vector2 gun = Gun.GlobalPosition;
			Vector2 mousePosition = GetGlobalMousePosition();
			
			Vector2 direction = (mousePosition - gun).Normalized();
			GD.Print(direction.ToString());
			bullet.Rotation = direction.Angle();
			bullet.GlobalPosition = gun;
		}
	}
}
