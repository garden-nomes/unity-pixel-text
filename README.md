# Unity Pixel Text

I've struggled one too many times to get bitmap fonts to work perfectly in Unity--working with TTFs
is messy, and ensuring that all the various sizes and scales line up perfectly can be a headache. This
library is designed to simplify things by reading a font from a spritesheet and rendering it using
Unity's built-in UI system.

## Usage

### Installation

Copy the git url for this project and install from the "Package Manager" window in Unity.

### Creating a font

1. Arrange all the characters for the given font in a grid and import it as a texture (empty spaces
   are OK)
2. Create a bitmap font: Assets -> Create -> Pixel Text -> Bitmap Font
3. Assign the texture from step 1 to the "texture" field
4. The inspector will likely prompt you about updating some import settings, go ahead and click
   "Update Import Settings" to have those automatically applied
5. Set a grid width and height to match the grid from step 1
6. In the "ordering" field enter all the the characters represented in the bitmap, left to right and
   top to bottom

With all those filled out, the bitmap font will automatically scan through the texture cell by cell
and determine he width of each individual character, skipping over empty cells. The inspector preview
should populate with a rendering of the result.

If you like, you can adjust some additional settings that control how the font is rendered:

- **Space Width**: how many pixels to render for each space
- **Letter Spacing**: how many pixels to place between each character
- **Line Spacing**: how many pixels to place between lines

### Creating text

1. Add a "Pixel Text" object to canvas: Component -> UI -> Pixel Text
2. It should function as a drop-in replacement for Unity's built-in text component, although lacking
   many features (notably rich text)

## Included fonts

- **m3x6**: A 3 by 6 (on average) pixel font by Daniel Linssen
  (source: [https://managore.itch.io/m3x6](https://managore.itch.io/m3x6))
