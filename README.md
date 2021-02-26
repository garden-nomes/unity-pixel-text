# Unity Pixel Text

I've struggled one too many times to get bitmap fonts to work perfectly in Unity--working with TTFs is messy, ensuring that all the various sizes and scales line up perfectly can be a headache and it's the absolute WORST when the pixels don't line up right. This package streamlines things by reading a font from a spritesheet and rendering it using Unity's built-in UI system.

## Usage

### Installation

Copy the git url for this project and install from the "Package Manager" window in Unity.

### Creating a font

1. Arrange all the characters for the given font in a grid and import it as a texture (empty cells are OK). Use white over a transparent background to make sure it renders correctly. (Other or multiple foreground colors do work, but will interact with the "color" option in the Pixel Text component).
2. Create a bitmap font: Assets -> Create -> Pixel Text -> Bitmap Font
3. Assign the texture from step 1 to the "texture" field
4. The inspector will likely prompt you about updating some import settings, go ahead and click "Update Import Settings" to have those automatically applied
5. Set grid width/height to match the grid from step 1
6. In the "ordering" field enter the characters represented in the bitmap in order, left to right then top to bottom

With all those filled out, the bitmap font will automatically scan through the texture cell by cell and determine he width of each individual character, skipping over empty cells. The inspector preview should populate with a rendering of the result.

Note that the "Update Import Settings" button turns of the "non-power of two" import setting, which unfortunately wreaks havoc with the bitmap font system. You should manually size the texture dimensions to a power of two to ensure compatibility on all platforms.

If you like, you can adjust some additional settings that control how the font is rendered:

- **Space Width**: how many pixels to render for each space
- **Letter Spacing**: how many pixels to render between each character
- **Line Spacing**: how many pixels to render between lines

### Creating text

Add a pixel text component: Component -> UI -> Pixel Text

It functions as a replacement for Unity's built-in text component, although lacking many features (such as rich text). Position and size it via rect transform. Text wraps within the rect transform's width. Tweak the "scale" factor (which represents units per pixel) until the text lines up with your pixel grid. Make sure that the rect transform's bottom-left corner sits on the pixel grid so that everything lines up neatly.

## Included fonts

- **m3x6**: A 3 by 6 (on average) pixel font by Daniel Linssen (source: [https://managore.itch.io/m3x6](https://managore.itch.io/m3x6))
