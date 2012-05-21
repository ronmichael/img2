using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// ImgAdapter overrides the default behavior of IMG tag.
/// 
/// It provides backwards compatibility for browsers that don't support SVG in IMG tags.
/// Just put a link to the non-SVG version of the file in the img as altsrc tag (eg. <img src='me.svg' altsrv='me.png' />
/// 
/// It also provides for server-side resizing of images for devices that don't support HD images.
/// So, put a larger version of file on server, and use tags to define width and/or heigh (eg <img src'me.svg' width=100 />
/// and if the device is known to not support HD the adapter will scale the image before sending it to them.
///  
/// </summary>
public class ImgAdapter : System.Web.UI.Adapters.ControlAdapter
{

    protected override void Render(HtmlTextWriter writer)
    {
        
        // if they don't support SVG, switch the src to alt version (stored in another property?)
        // if they don't need HD version, resize it

        HtmlImage i = (HtmlImage)base.Control;
        string fname = i.Src.ToLower();
        int width = i.Width;
        int height = i.Height;

       
        if (fname.EndsWith(".svg"))
        {
           
            if (!SupportsSvg())
            {
                if(i.Attributes["altsrc"]!=null)
                    fname = i.Attributes["altsrc"].ToString();

            }
        }

     
        if (!fname.EndsWith(".svg"))
        {
            if( (width>=0 || height>=0) && !SupportsHD())
            {

                // move width & height to parameters in image filename, let ImgHandler resize
                // make sure ImgHandler registered for png, gif and jpg in web.config

                string xx = "";

                if (width >= 0)
                    xx += "?w=" + width.ToString();
                if (height >= 0)
                    xx += (xx.Length > 1 ? "&" : "?") + "h=" + height.ToString();

                // we'll still include the width & height in the img properties so the browser knows the dimensions beforehand

                fname += xx;
                 
            }
        
            // we should at some point use some of the handler code to example the file, determine proportions,
            // and then include the proper width and height here.


        }

        // write out new tag

        writer.Write("<img src='" + fname + "'");
        if(width>=0) writer.Write(" width=" + width.ToString());
        if(height>=0) writer.Write(" height=" + height.ToString());

        
        foreach (string akey in i.Attributes.Keys)
        {
            if (akey != " altsrc" && akey !="src" && akey != "width" && akey !="height")
                writer.Write(akey + "=\"" + i.Attributes[akey].ToString() + "\"");
        }
        writer.Write(" />");

        // also have to write out any other properties...

        //base.Render(writer);
    }

    protected override void OnInit(EventArgs e)
    {
        //base.OnInit(e);
    }

    protected override void BeginRender(HtmlTextWriter writer)
    {
        //base.BeginRender(writer);
    }

    protected override void EndRender(HtmlTextWriter writer)
    {
        //base.EndRender(writer);
    }


 
    private bool SupportsHD()
    {

        HttpBrowserCapabilities b = HttpContext.Current.Request.Browser;
        string agent = HttpContext.Current.Request.UserAgent;

        // yes for retina displays; need to fine tune this for only iPhone 4+ and iPad 3+
        if (agent.Contains("like Mac OS X"))
            return true;


        // assume no for everything else

        return false;

    }



    private bool SupportsSvg()
    {
        

        // http://caniuse.com/#feat=svg-img

        HttpBrowserCapabilities b = HttpContext.Current.Request.Browser;
        string agent = HttpContext.Current.Request.UserAgent;

        if (b.Browser == "IE" && b.MajorVersion < 9) // IE didn't support SVG in IMG tags before version 9
            return false;

        if (b.Browser == "Firefox" && b.MajorVersion < 9) // Firefox didn't support SVG in IMG tags before version 9
            return false;

        // identification issues:
        // .NET identifies Android (Kindle Fire) browser as Safari ...  can test for agent containing "Silk"
        // .NET identifies Ipad browser as Safari; can test for agent containing "like Mac OS X"
        // .NET doesn't recognize ipad/kindle as mobile devices
        
        // other compatibility issues:
        // iOS Safari didn't properly support SVG in IMG tags before version 4.0; it worked but was buggy in older versions; just let it pass as true
        // Android browser didn't support SVG in IMG tags before version 3.0

        // assume everything else works

        return true;

    
    }



    
}
