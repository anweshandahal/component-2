using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anweshdahal
{

    /// <summary>
    /// this is for creating rectangle
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// variables for xaxis yaxis width height and fill on/off
        /// </summary>
        public int x, y, size, size1, texturestyle;

        /// <summary>
        /// for getting color for the pen
        /// </summary>
        public Color c1;
        /// <summary>
        /// this for drawing command
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            //throw new NotImplementedException();
            Pen p = new Pen(c1, 5);
            SolidBrush bb = new SolidBrush(c1);
            if (texturestyle == 0)
            {
                g.DrawRectangle(p, x, y, size, size1);
            }
            else
            {
                g.FillRectangle(bb, x, y, size, size1);
            }
        }

        /// <summary>
        /// this is for setting rectangle properties
        /// </summary>
        /// <param name="texturestyle">for the purpose of filling the shape</param>
        /// <param name="c1">for getting the color from the user</param>
        /// <param name="list">for getting the properties for the shape</param>
        public void set(int texturestyle, Color c1, params int[] list)
        {
            //throw new NotImplementedException();
            this.texturestyle = texturestyle;
            // this.bb = bb;
            this.c1 = c1;
            this.x = list[0];
            this.y = list[1];
            this.size = list[2];
            this.size1 = list[3];
        }
    }



}
