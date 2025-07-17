using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using MavraLib;

namespace MavraGUI;

public class MainViewModel : INotifyPropertyChanged
{
	// INotifyPropertyChanged, property changed handling
	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


	private Universe seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse evolutiveUniverse;

	
	
	
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



	private Universe _seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse _evolutiveUniverse;
	public void AddBody()
	{
		Body b = new Body(Mass, new Vector2(X, Y), Radius);
		b.Name = string.IsNullOrEmpty(Name) ? "Object" : Name;
		
		_seed.Bodies.Add(b);
		
		Console.WriteLine(_seed);
	}
	
}