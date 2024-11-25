## Audio Player

A cyberpunk-style audio player that incorporates iconic elements of the genre. 
With a subtle neon glow and a terminal-style interface, it evokes the gritty image of a crime-driven city of the future, complete with occasional graffiti. 
In addition to standard audio player functionality, it includes ability to read and edit track metadata and Image Gallery.


The Audio Player was built using WPF and .NET 8. 
As WPF strongly encourages the use of the MVVM architectural pattern, we implemented this pattern to separate the application's business and presentation logic from its user interface.
During development, we used several third-party libraries. One of them is the fantastic audio library NAudio, which was of invaluable help during playback and processing of audio files. 


### Reading metadata

Apart from the common functionality you'd expect from the audio player such as playlists, track selection, and playback controls.
One of the more interesting features is the ability to read and edit metadata tags of audio files. 

This means users can edit track information, like the title or artist, directly in the player even while the track is playing. 
To achieve this, we used TagLibSharp, a reliable library for editing audio metadata.



### Image Gallery

I have always appreciated well-crafted images and GIFs, so I included an Image Gallery. 
This lets users add any kind of visual media to their library and choose images or GIFs that match the mood of their music. 

However, WPF struggles to display GIFs efficiently. To solve this, I used the XAMLAnimatedGif library, which made handling GIFs much easier and is now my go-to tool for this task.
           
Finally, it's important to mention that the player does not come with any preloaded images or GIFs. The pretty artworks seen in the displayed images are solely for demonstrating the functionality of the image gallery.  



### What to expect in the future

In my spare time, I am developing a 3D visualizer using OpenGL, which I intend to include as a page in the player, similar to an image gallery. 

Additionally, I plan to create a dedicated Playlist Library page that will provide more detailed control over playlists and tracks.





#### We have used the following resources during application development:

##### Fonts

“PIXY” by 2DFUNS Studio.

“Terminal Grotesque” by [Raphaël Bastide](https://velvetyne.fr/authors/raphael-bastide/) and [Jérémy Landes](https://velvetyne.fr/authors/jjjlllnnn/).

“Don Graffiti” by [HelloDonMarciano](https://hellodonmarciano.com/).

##### Images

“Texture fresh element dirty abstract” by [kues1](https://www.freepik.com/free-photo/texture-fresh-element-dirty-abstract_1096288.htm).
