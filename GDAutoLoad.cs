using Godot;

/// <summary>
/// Godot C# singleton base class.
/// You need to load the child class in menu Project Settings > Globals > Autoload,
/// but in some cases everything will work fine even without this step.
/// </summary>
public partial class GDAutoLoad<T> : Node where T : Node, new() {

    static T _instance;

	public static bool FromAutoload = true;

	public static T Instance {
		get {

			FromAutoload = false;

			if (_instance == null) {
				
				// Check if we were loaded via Autoload
				_instance = ((SceneTree)Engine.GetMainLoop()).Root.GetNodeOrNull<T>(typeof(T).Name);
				
				if (_instance == null)
				{
					// Instantiate to root at runtime
					_instance = new T() {  
						Name = typeof(T).Name
					};			
															
					((SceneTree)Engine.GetMainLoop()).Root.CallDeferred(MethodName.AddChild, _instance);
				}
				else {
					FromAutoload = true;
				}						
			}
			return _instance;
		}
	}

	protected GDAutoLoad() {
	}	

}