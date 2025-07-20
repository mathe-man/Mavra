using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MavraGUI.Windows;

public class PropertyChanger
{
	// INotifyPropertyChanged, property changed handling
	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}