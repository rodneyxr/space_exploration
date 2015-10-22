using UnityEngine;
using System.Collections;

/// <summary>
/// DemoScript
/// 
/// This is an example of how to document your code in order to
/// be compatible with the Doxygen documentation engine. Doxygen
/// produces detailed HTML documentation pages based on /// comments
/// in each script. Typing /// on the line above a class or method
/// declaration will automatically generate most of the documentation
/// for you.
/// </summary>
public class DemoScript : MonoBehaviour
{
	/// <summary>
	/// A method provided by Unity. There is no real
	/// need to document these methods.
	/// </summary>
	void Start ()
	{
		DemoMethod (1, 2, 3);
	}

	/// <summary>
	/// An arbitrary method to demonstrate method documentation.
	/// </summary>
	/// <returns>The sum of the given integer parameters.</returns>
	/// <param name="a">Integer 'a'.</param>
	/// <param name="b">Integer 'b'.</param>
	/// <param name="c">Integer 'c'.</param>
	public int DemoMethod (int a, int b, int c)
	{
		return a + b + c;
	}
}
