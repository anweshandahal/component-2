using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anweshdahal
{
    /// <summary>
    /// this is the shape class which contains method which makes easier to add more shapes
    /// </summary>
    public interface Shape
    {

        /// <summary>
        /// this is for the graphics
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);


        /// passing the value from button click of form to the shape

        /// <param name="fill">define texture</param>
        /// <param name="bb">define properties of brush</param>
        /// <param name="c">define color</param>
        /// <param name="list">list of parameter</param>
        void set(int fill, Color c, params int[] list);

    }

}
