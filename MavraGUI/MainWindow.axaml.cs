using System;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using MavraLib;

namespace MavraGUI;

public partial class MainWindow : Window
{
	private Universe seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse _evolutiveUniverse;
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainViewModel();
	}

	private void BtnAddBody_OnClick(object? sender, RoutedEventArgs e)
	{
		(DataContext as MainViewModel)?.AddBody();
	}
}