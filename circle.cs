using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anweshdahal
{



 /// <summary>
 /// This is the circle class which inherits the interface class
 /// The purpose of this class is to draw circle according to the user input
 /// </summary>
    class Circle : Shape
    {
        /// <summary>
        /// Getting value for ellipse
        /// </summary>
        int x, y, size, size1;
        Color c1;
        int texturestyle;
        /// <summary>
        /// for drawing command
        /// </summary>
        /// <param name="g">for calling graphics for form</param>
        public void Draw(Graphics g)
        {
            SolidBrush bb = new SolidBrush(c1);
            //throw new NotImplementedException();
            Pen p = new Pen(c1, 5);
            if (texturestyle == 0)
            {
                g.DrawEllipse(p, x, y, size, size1);
            }
            else
            {
                g.FillEllipse(bb, x, y, size, size1);
            }
        }

        /// <summary>
        /// for setting the data accoridng to the user
        /// </summary>
        /// <param name="texturestyle">for the purpose of filling of shape</param>
        /// <param name="c1">for getting color according to the user </param>
        /// <param name="list">for getting radius and xaxis and yaxis</param>
        public void set(int texturestyle, Color c1, params int[] list)
        {
            //throw new NotImplementedException();
            this.texturestyle = texturestyle;
            //this.bb = bb;
            this.c1 = c1;
            this.x = list[0];
            this.y = list[1];
            this.size = list[2];
            this.size1 = list[3];
        }
    }

}
