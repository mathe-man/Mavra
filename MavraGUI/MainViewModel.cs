using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using MavraLib;

namespace MavraGUI;

public class MainViewModel : INotifyPropertyChanged
{
	// INotifyPropertyChanged, property changed handling
	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


	private Universe _onScreen;
	public Universe OnScreen
	{
		get => _onScreen;
		set {
			if (Equals(value, _onScreen)) return;
			_onScreen = value;
			OnPropertyChanged();
			DrawUniverse(value);
		} 
	}  
	

	
	
	
	private string _name;
	public string Name
	{
		get => _name;
		set {
			if (Equals(value, _name)) return;
			_name = value;
			OnPropertyChanged();
		} 
	}  
	
	private float _x;
	public float X
	{
		get => _x;
		set
		{
			if (Equals(value, _x)) return;

			_x = value;
			OnPropertyChanged();
		}
	}  
	
	private float _y;
	public float Y
	{
		get => _y;
		set {
			if (Equals(value, _y)) return;
			_y = value;
			OnPropertyChanged();
		} 
	}  
	
	private float _XVelocity;
	public float XVelocity
	{
		get => _XVelocity;
		set {
			if (Equals(value, _XVelocity)) return;
			_XVelocity = value;
			OnPropertyChanged();
		} 
	}  
	
	private float _YVelocity;
	public float YVelocity
	{
		get => _YVelocity;
		set {
			if (Equals(value, _YVelocity)) return;
			_YVelocity = value;
			OnPropertyChanged();
		} 
	}  
	
	private float _mass;
	public float Mass
	{
		get => _mass;
		set {
			if (Equals(value, _mass)) return;
			_mass = value;
			OnPropertyChanged();
		} 
	}  
	
	private float _radius;
	public float Radius
	{
		get => _radius;
		set {
			if (Equals(value, _radius)) return;
			
			_radius = value;
			OnPropertyChanged();
		} 
	}

	private Grid UniverseGrid;
	public MainViewModel(Grid universGrid)
	{
		_evolutiveUniverse = new (_seed);
		OnScreen = _seed;
		UniverseGrid = universGrid;
	}

	private DispatcherTimer? timer;
	
	private Universe _seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse _evolutiveUniverse;
	private int index = 0;
	public void AddBody()
	{
		Body b = new Body(Mass, new Vector2(X, Y), Radius, new Vector2(XVelocity, YVelocity));
		b.Name = string.IsNullOrEmpty(Name) ? "Object" : Name;
		
		_seed.Bodies.Add(b);
		DrawUniverse(_seed);
		
		Console.WriteLine(_seed);
	}

	public bool MoveIndex(int amount)
	{
		var requested_index = index + amount;

		if (requested_index >= _evolutiveUniverse.Evolution.Count)
			return false;
		
		
		if (requested_index < 0)
			return false;
		
		index += amount;
		Console.WriteLine(index);
		DrawUniverse(_evolutiveUniverse.Evolution[index]);
		return true;
	}

	public void StartSimulation()
	{
		_evolutiveUniverse.ComputeEvolution(100000);
	}

	
	private int _animationSpeed;
	public int AnimationSpeed
	{
		get => _animationSpeed;
		set {
			if (Equals(value, _animationSpeed)) return;
			_animationSpeed = value;
			OnPropertyChanged();
		} 
	}  
	public void AnimateSimulation(int frame_milliseconds = 16) // 16 milliseconds for 60fps
	{
		if (timer == null || timer.IsEnabled)
			timer?.Stop();
		
		timer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(frame_milliseconds)
		};
		timer.Tick += (_, _) =>
		{
			if (!MoveIndex(AnimationSpeed))
				timer.Stop();
			
			Console.WriteLine($"Frame: {DateTime.Now - lastAnimationFrameTime}");
			lastAnimationFrameTime = DateTime.Now;
		};
		
		timer.Start();
	}

	private static DateTime lastAnimationFrameTime;
	
	private void DrawUniverse(Universe universe)
	{
		if (UniverseGrid == null)
			return;
		
		UniverseGrid.Children.Clear();
		foreach (var body in universe.Bodies)
		{
			var circle = new Ellipse();
			circle.Height = body.Radius;
			circle.Width  = body.Radius;

			circle.Margin = new Thickness(body.Position.X, 0, 0, body.Position.Y);
			circle.Fill = new SolidColorBrush(Colors.Red);
			
			UniverseGrid.Children.Add(circle);
		}
	}
}