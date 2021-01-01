using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anweshdahal
{
    class drawto : Shape
    {
        public void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is to draw shape of line
        /// </summary>
        /// <param name="res">it stores the user input</param>
        /// <param name="colour">it stores the color</param>
        /// <param name="g">for graphics</param>
        /// <param name="k">for xaxis</param>
        /// <param name="l">for yaxis</param>
        public void drawShape(string[] res, Color colour, Graphics g, int k, int l)
        {
            int a = Convert.ToInt32(res[1]);
            int b = Convert.ToInt32(res[2]);
            Pen p = new Pen(colour, 2);
            g.DrawLine(p, k, l, a, b);
        }

        public void fillShape(string[] res, Color colour, Graphics g, int a, int b)
        {
            SolidBrush brush = new SolidBrush(colour);
        }

        public void set(int fill, Color c, params int[] list)
        {
            throw new NotImplementedException();
        }
    }
}

