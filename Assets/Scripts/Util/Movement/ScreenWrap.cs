using UnityEngine;

public class ScreenWrap : MonoBehaviour
{	
	Renderer[] renderers;

    public bool canWrapX = true;
    public bool canWrapY = true;

    bool isWrappingX = false;
	bool isWrappingY = false;
	
	float screenWidth;
	float screenHeight;
	
	void Start()
	{
		// Fetch all the renderers that display object graphics.
		// We use the renderer(s) so we can check if the ship is
		// visible or not.
		renderers = GetComponentsInChildren<Renderer>();
		
		var cam = Camera.main;
		
		// We need the screen width in world units, relative to the ship.
		// To do this, we transform viewport coordinates of the screen edges to 
		// world coordinates that lie on on the same Z-axis as the player.
		//
		// Viewport coordinates are screen coordinates that go from 0 to 1, ie
		// x = 0 is the coordinate of the left edge of the screen, while,
		// x = 1 is the coordinate of the right edge of the screen.
		// Similarly,
		// y = 0 is the bottom screen edge coordinate, while
		// y = 1 is the top screen edge coordinate.
		//
		// Which gives us this:
		// (0, 0) is the bottom left corner, to
		// (1, 1) is the top right corner.
		var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
		var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
		
		// The width is then equal to difference between the rightmost and leftmost x-coordinates
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		// The height, similar to above is the difference between the topmost and the bottom yycoordinates
		screenHeight = screenTopRight.y - screenBottomLeft.y;
	}
	
	// Update is called once per frame
	void Update()
	{	
		CheckForScreenWrap();
	}
	
	void CheckForScreenWrap()
	{	
		// If all parts of the object are invisible we wrap it around
		foreach(var renderer in renderers)
		{
			if(renderer.isVisible)
			{
				isWrappingX = false;
				isWrappingY = false;
				return;
			}
		}

		// If we're already wrapping on both axes there is nothing to do
		if(isWrappingX && isWrappingY) {
			return;
		}
		
		var cam = Camera.main;
		var newPosition = transform.position;
		
		// We need to check whether the object went off screen along the horizontal axis (X),
		// or along the vertical axis (Y).
		//
		// The easiest way to do that is to convert the ships world position to
		// viewport position and then check.
		//
		// Remember that viewport coordinates go from 0 to 1?
		// To be exact they are in 0-1 range for everything on screen.
		// If something is off screen, it is going to have
		// either a negative coordinate (less than 0), or
		// a coordinate greater than 1
		//
		// So, we get the ships viewport position
		var viewportPosition = cam.WorldToViewportPoint(transform.position);
		
		
		// Wrap it is off screen along the x-axis and is not being wrapped already
		if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0) && canWrapX)
		{
			// The scene is laid out like a mirror:
			// Center of the screen is position the camera's position - (0, 0),
			// Everything to the right is positive,
			// Everything to the left is negative;
			// Everything in the top half is positive
			// Everything in the bottom half is negative
			// So we simply swap the current position with it's negative one
			// -- if it was (15, 0), it becomes (-15, 0);
			// -- if it was (-20, 0), it becomes (20, 0).
			newPosition.x = -newPosition.x;
			
			// If you had a camera that isn't at X = 0 and Y = 0,
			// you would have to use this instead
			// newPosition.x = Camera.main.transform.position - newPosition.x;
			
			isWrappingX = true;
		}
		
		// Wrap it is off screen along the y-axis and is not being wrapped already
		if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0) && canWrapY)
		{
			newPosition.y = -newPosition.y;
			
			isWrappingY = true;
		}
		
		//Apply new position
		transform.position = newPosition;
	}
}