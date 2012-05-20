img2
=============================================================
img2 is an ASP.NET adapter and handler that extends the capabilities of the <img> tag.
It provides SVG swapping for browsers that don't support SVGs in IMG tags.
It provides adaptive image resizing to accomodate HD images for high-resolution displays
without unnecessarily burderning devices that don't support it.



Installation
-------------------------------------------------------------
- Install the ImgAdapater.cs and ImgHandler.cs files into your App_Code folder

- Install img.browser file into your App_browsers folder

- Register the handler in your web.config to process gif, jpg and png files. For IIS 7:

	  <system.webServer>
		<handlers>
		  <add name="ImgHandler gif" verb="*" path="*.gif" type="ImgHandler" resourceType="File"/>
		  <add name="ImgHandler jpg" verb="*" path="*.jpg" type="ImgHandler" resourceType="File"/>
		  <add name="ImgHandler png" verb="*" path="*.png" type="ImgHandler" resourceType="File"/>
		</handlers>
		<modules runAllManagedModulesForAllRequests="true"/>
	  </system.webServer>



SVG swapping
-------------------------------------------------------------
When you use the img tag to render an SVG it will give the ability to swap the SVG with a typical image file
for browers that don't support it. Just include the alternative image file in the tag as altsrc. Example:

	<img runat="server" src='mypicture.svg' altsrc='mypicture.png' width=20 height=10 />

A browser that supports SVG will get:

	<img src='mypicture.svg' width=20 height=10 />

while one that doesn't will get:

	<img src='mypicture.png' width=20 height=10 />


Adaptive image resizing
-------------------------------------------------------------
It also provides server-side resizing of images. This addresses a few needs. First, in order to provide HD
images to devices such as the iPad with high-resolution displays, you want to send an image that is of greater
width and height than what you typically want to to display at.  This gives the iPad more to work with as it resizes
the image and zooms in or out. But, for machines with normal displays, you are sending them an image that is bigger
than they need - wasting bandwidth and also trusting the browser to do a good job at resizing the image, which isn't
always the case. 

So for example, if you want an image to occupy a 400x300 space on a page, build it as a 600x450 or 800x600 image.  Then embed
it like this:

	<img runat="server" src='myimg.png' width=400 />

or

	<img runat="server" src='myimg.png' height=300 />

or

	<img runat="server" src='myimg.png' width=400 height=300 />

In each case, the code will determine whether the use has aan iPad. If they do, it will send the image as is.  If they don't,
it will resample the image (proportionally, if only one dimension is provided) on the server and server that image.




The MIT License
-------------------------------------------------------------
Copyright (c) 2012 Ron Michael Zettlemoyer
				
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


