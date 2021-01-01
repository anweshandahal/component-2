using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anweshdahal
{


 class ShapeFactory
    {

        /// <summary>
        /// checking the type of shape
        /// </summary>
        /// <param name="shapeType">getting the shape name</param>
        /// <returns>returns the shape name</returns>
        public Shape GetShape(string shapeType)
        {
            if (shapeType == "circle")
            {
                return new Circle();
            }
            else if (shapeType == "rectangle")
            {
                return new Rectangle();
            }

            else if (shapeType == "triangle")
            {
                return new Triangle();
            }
            else if (shapeType == "square")
            {
                return new Square();
            }
            return null;
        }
    }

}
