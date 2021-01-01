using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anweshdahal
{
    /// <summary>
    /// this is the from class
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// this is the constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            gap = Panel_Draw.CreateGraphics();
        }


        Graphics gap;
        int x, y = 1;//pen postion
        int Xaxis = 0, Yaxis = 0;//xaxis and yaxis in the panel

        OpenFileDialog openFile = new OpenFileDialog();

        Validation validate;
        int loopCounter = 0;
        /// <summary>
        /// boolean value for checking if the value is true or false
        /// </summary>
        Boolean hasDrawOrMoveValue = false;
        /// <summary>
        /// setting variable value.
        /// </summary>
        public int radius = 0, width = 0, height = 0, dSize = 0, count = 0, side = 0;
        /// <summary>
        /// for rotation value
        /// </summary>
        public float rotation = 0;

        //string shape;
        ShapeFactory shapeFactory = new ShapeFactory();//calling factory class for verification
        //Shape shapes;
        private void btn_exec_Click(object sender, EventArgs e)
        {
            hasDrawOrMoveValue = false;
            if (txtCommand.Text != null && txtCommand.Text != "")
            {
                validate = new Validation(txtCommand);
                if (!validate.isSomethingInvalid)
                {
                    MessageBox.Show("Everything is working fine");
                    lCommand();
                }

            }
        }
        /// <summary>
        /// this is for the purpose of loading command
        /// </summary>
        private void lCommand()
        {
            int numOfLines = txtCommand.Lines.Length;

            for (int i = 0; i < numOfLines; i++)
            {
                String oneLineCmd = txtCommand.Lines[i];
                oneLineCmd = oneLineCmd.Trim();
                if (!oneLineCmd.Equals(""))
                {
                    Boolean hasDrawto = Regex.IsMatch(oneLineCmd.ToLower(), @"\bdrawto\b");
                    Boolean hasMoveto = Regex.IsMatch(oneLineCmd.ToLower(), @"\bmoveto\b");
                    if (hasMoveto)
                    {
                        String args = oneLineCmd.Substring(6, (oneLineCmd.Length - 6));
                        String[] parms = args.Split(',');
                        for (int j = 0; j < parms.Length; j++)
                        {
                            parms[j] = parms[j].Trim();
                        }
                        Xaxis = int.Parse(parms[0]);
                        Yaxis = int.Parse(parms[1]);
                        hasDrawOrMoveValue = true;
                    }
                    else if (hasDrawto)
                    {
                        String args = oneLineCmd.Substring(6, (oneLineCmd.Length - 6));
                        String[] parms = args.Split(',');
                        for (int j = 0; j < parms.Length; j++)
                        {
                            parms[j] = parms[j].Trim();
                        }
                        x = int.Parse(parms[0]);
                        y = int.Parse(parms[1]);
                        hasDrawOrMoveValue = true;
                        Pen p = new Pen(Color.Black);
                        gap.DrawLine(p, x, y, Xaxis, Yaxis);
                    }
                    else
                    {
                        hasDrawOrMoveValue = false;
                    }
                    if (hasMoveto)
                    {
                        Panel_Draw.Refresh();
                    }
                }
            }
            //for checking if there is loop for if and loop statement
            for (loopCounter = 0; loopCounter < numOfLines; loopCounter++)
            {
                String oneLineCommand = txtCommand.Lines[loopCounter];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    RunCmd(oneLineCommand);
                }

            }
        }
        /// <summary>
        /// for excuting command
        /// </summary>
        /// <param name="oneLineCmd">to get user input</param>
        private void RunCmd(String oneLineCmd)
        {

            Boolean hasPlus = oneLineCmd.Contains('+');
            Boolean hasEquals = oneLineCmd.Contains("=");
            try
            {

                oneLineCmd = Regex.Replace(oneLineCmd, @"\s+", " ");
                string[] words = oneLineCmd.Split(' ');
                //removing white spaces in between words
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }

                String firstWord = words[0].ToLower();

                if (firstWord.Equals("if") && words.Contains("then"))
                {

                    Boolean loop = false;
                    if (words[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(words[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("count"))
                    {
                        if (count == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("side"))
                    {
                        if (side == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }


                    int ifStartLine = (GetIfStartLineNumber());
                    //int ifEndLine = (GetEndifEndLineNumber() - 1);
                    //loopCounter = ifEndLine;
                    if (words[4].Equals("then"))
                    {
                        for (int j = ifStartLine; j <= ifStartLine; j++)
                        {
                            try
                            {
                                if (words[5].ToLower().Equals("circle"))
                                {
                                    if (words[6].ToLower().Equals("radius"))
                                    {
                                        radius = Int32.Parse(words[6]);
                                        DrawCircle(radius);

                                    }
                                    else
                                    {
                                        DrawCircle(Int32.Parse(words[6]));
                                        //break;
                                        MessageBox.Show("infinite loop");
                                    }

                                }

                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("If Statement is false");
                    }
                }
                else
                {
                    string[] words2 = oneLineCmd.Split('=');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("count"))
                    {
                        count = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("side"))
                    {
                        side = int.Parse(words2[1]);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
            }
            if (hasEquals)
            {
                try
                {
                    oneLineCmd = Regex.Replace(oneLineCmd, @"\s+", " ");
                    string[] words = oneLineCmd.Split(' ');
                    //removing white spaces in between words
                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();
                    }
                    String firstWord = words[0].ToLower();

                    if (firstWord.Equals("if"))
                    {
                        Boolean loop = false;
                        if (words[1].ToLower().Equals("radius"))
                        {
                            if (radius == int.Parse(words[3]))
                            {
                                loop = true;
                            }
                        }
                        else if (words[1].ToLower().Equals("width"))
                        {
                            if (width == int.Parse(words[3]))
                            {
                                loop = true;
                            }
                        }
                        else if (words[1].ToLower().Equals("height"))
                        {
                            if (height == int.Parse(words[3]))
                            {
                                loop = true;
                            }

                        }
                        else if (words[1].ToLower().Equals("count"))
                        {
                            if (count == int.Parse(words[3]))
                            {
                                loop = true;
                            }
                        }
                        else if (words[1].ToLower().Equals("side"))
                        {
                            if (side == int.Parse(words[3]))
                            {
                                loop = true;
                            }
                        }
                        int ifStartLine = (GetIfStartLineNumber());
                        int ifEndLine = (GetEndifEndLineNumber() - 1);
                        loopCounter = ifEndLine;
                        if (loop)
                        {
                            for (int j = ifStartLine; j <= ifEndLine; j++)
                            {
                                string oneLineCommand1 = txtCommand.Lines[j];
                                oneLineCommand1 = oneLineCommand1.Trim();
                                if (!oneLineCommand1.Equals(""))
                                {
                                    RunCmd(oneLineCommand1);
                                }
                            }
                        }

                        else
                        {
                            MessageBox.Show("If Statement is false");
                        }
                    }
                    else
                    {
                        string[] words2 = oneLineCmd.Split('=');
                        for (int j = 0; j < words2.Length; j++)
                        {
                            words2[j] = words2[j].Trim();
                        }
                        if (words2[0].ToLower().Equals("radius"))
                        {
                            radius = int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("width"))
                        {
                            width = int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("height"))
                        {
                            height = int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("count"))
                        {
                            count = int.Parse(words2[1]);
                        }
                        else if (words2[0].ToLower().Equals("side"))
                        {
                            side = int.Parse(words2[1]);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            else if (hasPlus)
            {
                oneLineCmd = System.Text.RegularExpressions.Regex.Replace(oneLineCmd, @"\s+", " ");
                string[] words = oneLineCmd.Split(' ');
                if (words[0].ToLower().Equals("repeat"))
                {
                    count = int.Parse(words[1]);
                    if (words[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = GetSize(oneLineCmd);
                        radius = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("square"))
                    {
                        int increaseValue = GetSize(oneLineCmd);
                        dSize = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawRectangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = GetSize(oneLineCmd);
                        dSize = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawRectangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = GetSize(oneLineCmd);
                        dSize = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawTriangle(dSize, dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                }
                else
                {
                    string[] words2 = oneLineCmd.Split('+');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("side"))
                    {
                        side += int.Parse(words2[1]);
                    }

                }
            }
            else
            {
                try
                {
                    DrawingCommand(oneLineCmd);
                    // Application.Exit();
                }
                catch (Exception e) { }
            }


        }


        /// <summary>
        /// Returns the size of structure
        /// </summary>
        /// <param name="lineCommand"></param>
        /// <returns></returns>
        private int GetSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }
        /// <summary>
        /// this is area where draw commands/shapes commands are executed
        /// </summary>
        /// <param name="lineOfCommand"></param>
        private void DrawingCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle", "square" };
            String[] variable = { "radius", "width", "height", "count", "size", "side" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] words = lineOfCommand.Split(' ');
            //removing white spaces in between words
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }
            if (words[0].ToUpper() == "FILL" && words[1].ToUpper() == "ON")
            {
                fill = 1;
            }
            else if (words[0].ToUpper() == "FILL" && words[1].ToUpper() == "OFF")
            {
                fill = 0;
            }
            else if (words[0].ToUpper() == "PEN" && words[1].ToUpper() == "GREEN")
            {
                pcolor = Color.Green;
            }
            else if (words[0].ToUpper() == "PEN" && words[1].ToUpper() == "YELLOW")
            {
                pcolor = Color.Yellow;
            }
            else if (words[0].ToUpper() == "PEN" && words[1].ToUpper() == "PINK")
            {
                pcolor = Color.Pink;
            }
            else if (words[0].ToUpper() == "PEN" && words[1].ToUpper() == "RED")
            {
                pcolor = Color.Red;


            }
            else if (words[0].ToUpper() == "ROTATE")
            {
                rotation = float.Parse(words[1]);
            }

            String firstWord = words[0].ToLower();
            Boolean firstWordShape = shapes.Contains(firstWord);
            if (firstWordShape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(words[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (words[1].ToLower().Equals("radius"))
                        {
                            DrawCircle(radius);
                        }
                    }
                    else
                    {
                        DrawCircle(Int32.Parse(words[1]));
                    }

                }
                else if (firstWord.Equals("square"))
                {
                    Boolean secondWordIsVariable = variable.Contains(words[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (words[1].ToLower().Equals("side"))
                        {
                            DrawSquare(side, side);
                        }
                    }
                    else
                    {
                        DrawSquare(Int32.Parse(words[1]), Int32.Parse(words[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(width, height);
                        }
                        else
                        {
                            DrawRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            DrawRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }

                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    DrawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }
              

            }
            else
            {
                if (firstWord.Equals("loop"))
                {
                    string[] words2 = lineOfCommand.Split('=');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("count"))
                    {
                        count = int.Parse(words2[1]);
                    }
                    //counter = int.Parse(words[1]);
                    int loopStartLine = (GetLoopStartLineNumber());
                    int loopEndLine = (GetLoopEndLineNumber() - 1);
                    loopCounter = loopEndLine;
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String oneLineCommand = txtCommand.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCmd(oneLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (words[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(words[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("count"))
                    {
                        if (count == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("side"))
                    {
                        if (side == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCounter = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            String oneLineCommand = txtCommand.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCmd(oneLineCommand);
                            }
                        }
                    }
                }
                //method
            }
        }
        /// <summary>
        /// initiates if there is an if clause
        /// </summary>
        /// <returns></returns>
        private int GetIfStartLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');
                //removing white spaces in between words
                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        /// <summary>
        /// initiates loop
        /// </summary>
        /// <returns></returns>
        private int GetEndifEndLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        /// <summary>
        /// Initiates loops
        /// </summary>
        /// <returns></returns>
        private int GetLoopStartLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCmd = txtCommand.Lines[i];
                oneLineCmd = Regex.Replace(oneLineCmd,@"\s+", " ");
                string[] words = oneLineCmd.Split(' ');
                //removing white spaces in between words
                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCmd = oneLineCmd.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;

        }
        /// <summary>
        /// checks for loop end
        /// </summary>
        /// <returns></returns>
        private int GetLoopEndLineNumber()
        {
            try
            {
                int nOfLines = txtCommand.Lines.Length;
                int lineNum = 0;

                for (int i = 0; i < nOfLines; i++)
                {
                    String oneLineCommand = txtCommand.Lines[i];
                    oneLineCommand = oneLineCommand.Trim();
                    if (oneLineCommand.ToLower().Equals("endloop"))
                    {
                        lineNum = i + 1;

                    }
                }
                return lineNum;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// for calling triangle class
        /// </summary>
        /// <param name="rBase">variable for base of the triangle</param>
        /// <param name="adj">variable for adjacent of the triangle</param>
        /// <param name="hyp"></param>
        private void DrawTriangle(int rBase, int adj, int hyp)
        {

            gap = Panel_Draw.CreateGraphics();
            gap.RotateTransform(rotation);//for rotating the shape
            _s1 = Xaxis;
            _s2 = Yaxis;

            _s3 = rBase;
            _s4 = adj;
            _s5 = hyp;


            xi1 = _s1;
            yi1 = _s2;
            xi2 = Math.Abs(_s3);
            yi2 = _s2;

            xii1 = _s1;
            yii1 = _s2;
            xii2 = _s1;
            yii2 = Math.Abs(_s4);

            xiii1 = Math.Abs(_s3);
            yiii1 = _s2;
            xiii2 = _s1;
            yiii2 = Math.Abs(_s4);

            ShapeFactory shapeFactory = new ShapeFactory();
            Shape c = shapeFactory.GetShape("triangle");
            c.set(fill, pcolor, xi1, yi1, xi2, yi2, xii1, yii1, xii2, yii2, xiii1, yiii1, xiii2, yiii2);
            c.Draw(gap);
        }
        /// <summary>
        /// Method for calling rectangle class
        /// </summary>
        /// <param name="width">getting width of the </param>
        /// <param name="height"></param>
        private void DrawRectangle(int width, int height)
        {

            gap = Panel_Draw.CreateGraphics();
            gap.RotateTransform(rotation);//for rotating the shape
            _s1 = Xaxis;
            _s2 = Yaxis;
            _s3 = width;
            _s4 = height;



            ShapeFactory shapeFactory = new ShapeFactory();
            Shape c = shapeFactory.GetShape("rectangle");

            c.set(fill, pcolor, _s1, _s2, _s3, _s4);
            c.Draw(gap);
        }
        /// <summary>
        /// Method for calling rectangle class
        /// </summary>
        /// <param name="width">getting width of the </param>
        /// <param name="height"></param>


        /// <summary>
        /// Method for calling rectangle class
        /// </summary>
        /// <param name="width">getting width of the </param>
        /// <param name="height"></param>
        private void DrawSquare(int side, int side1)
        {

            gap = Panel_Draw.CreateGraphics();
            gap.RotateTransform(rotation);//for rotating the shape
            _s1 = Xaxis;
            _s2 = Yaxis;
            _s3 = side;
            _s4 = side1;
            //_size4 = side;
            ShapeFactory shapeFactory = new ShapeFactory();
            Shape c = shapeFactory.GetShape("square");
            c.set(fill, pcolor, _s1, _s2, _s3, _s4);
            c.Draw(gap);

        }
        /// <summary>
        /// method for calling circle class
        /// </summary>
        /// <param name="radius">value from the user</param>
        private void DrawCircle(int radius)
        {

            //-----------------------------
            gap = Panel_Draw.CreateGraphics();
            _s1 = Xaxis;
            _s2 = Yaxis;
            _s3 = radius;
            ShapeFactory shapeFactory = new ShapeFactory();
            Shape c = shapeFactory.GetShape("circle");
            c.set(fill, pcolor, _s1, _s2, _s3 * 2, _s3 * 2);
            //c.draw(set);
            c.Draw(gap);
        }
        /// <summary>
        /// This function will load the text file from desired location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // txt_cmd.Text = File.ReadAllText(OpenFileDialog.);
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text Document(*.txt) | *.txt";
            if (of.ShowDialog() == DialogResult.OK)
            {
                txtCommand.Text = File.ReadAllText(of.FileName);
            }
        }


        /// <summary>
        /// to save running program to textfile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sv.FileName, txtCommand.Text);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// for the shapes data
        /// </summary>
        public int _s1, _s2, _s3, _s4, _s5, _s6;

        private void Panel_Draw_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// displays how to use the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instructionToolStripMenuItem_Click_1(object sender, EventArgs e)



        {

        }
        /// <summary>
        /// shows about owner of the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// for the purpose of user so when enter is pressed commands executes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.Text.ToUpper() == "RUN")
            {
                btn_exec_Click(this, new EventArgs());
            }
            if (e.KeyCode == Keys.Enter)
            {
                btn_run_Click(this, new EventArgs());
            }
        }


        /// <summary>
        /// For exiting the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txt_cmd_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pnl_Draw_Paint(object sender, PaintEventArgs e)
        {

        }



        /// <summary>
        /// for Triangle sides
        /// </summary>
        public int xi1, yi1, xi2, yi2, xii1, yii1, xii2, yii2, xiii1, yiii1, xiii2, yiii2;
        Color pcolor = Color.Blue;
        int fill = 0;//this is for the fill on and off option


        /// <summary>
        /// all logic to run command in commnad line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            Regex regexRun = new Regex(@"run");
            Regex regexClear = new Regex(@"clear");
            Regex regexReset = new Regex(@"reset");
            Regex regexMT = new Regex(@"moveto (.*[\d])([,])(.*[\d])");
            Regex regexDT = new Regex(@"drawto (.*[\d])([,])(.*[\d])");

            Regex regexR = new Regex(@"rectangle (.*[\d])([,])(.*[\d])");
            Regex regexC = new Regex(@"circle (.*[\d])");
            Regex regexT = new Regex(@"triangle (.*[\d])([,])(.*[\d])([,])(.*[\d])");

            Match matchRun = regexRun.Match(textBox2.Text.ToLower());
            Match matchClear = regexClear.Match(textBox2.Text.ToLower());
            Match matchReset = regexReset.Match(textBox2.Text.ToLower());
            Match matchMT = regexMT.Match(textBox2.Text.ToLower());
            Match matchDT = regexDT.Match(textBox2.Text.ToLower());

            Match matchR = regexR.Match(textBox2.Text.ToLower());
            Match matchC = regexC.Match(textBox2.Text.ToLower());
            Match matchT = regexT.Match(textBox2.Text.ToLower());



            if (matchRun.Success || matchClear.Success || matchReset.Success || matchMT.Success || matchDT.Success || matchR.Success || matchC.Success || matchT.Success)
            {
                //----------------RECTANGLE-----------------------//


                if (matchR.Success)
                {
                    try
                    {
                        gap = Panel_Draw.CreateGraphics();
                        // g.RotateTransform(rotation);//for rotating the shape
                        _s1 = Xaxis;
                        _s2 = Yaxis;
                        _s3 = int.Parse(matchR.Groups[1].Value);
                        _s4 = int.Parse(matchR.Groups[3].Value);

                        ShapeFactory shapeFactory = new ShapeFactory();
                        Shape c = shapeFactory.GetShape("rectangle");
                        c.set(fill, pcolor, _s1, _s2, _s3, _s4);

                        c.Draw(gap);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("wrong parameter: should be like this \"rectangle width, height\"");
                    }
                }

                //----------------CIRCLE-----------------------//
                else if (matchC.Success)
                {
                    try
                    {
                        gap = Panel_Draw.CreateGraphics();
                        // g.RotateTransform(rotation);//for rotating the shape
                        _s1 = Xaxis;
                        _s2 = Yaxis;
                        _s3 = int.Parse(matchC.Groups[1].Value);


                        ShapeFactory shapeFactory = new ShapeFactory();
                        Shape c = shapeFactory.GetShape("circle");
                        c.set(fill, pcolor, _s1, _s2, _s3 * 2, _s3 * 2);
                        //c.draw(set);
                        c.Draw(gap);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("wrong parameter: should be like this \"circle radius\"");
                    }
                }
                // ----------------TRIANGLE---------------------- -//

                else if (matchT.Success)
                {
                    try
                    {
                        gap = Panel_Draw.CreateGraphics();
                        // g.RotateTransform(rotation);//for rotating the shape
                        _s1 = Xaxis;
                        _s2 = Yaxis;

                        _s3 = int.Parse(matchT.Groups[1].Value);
                        _s4 = int.Parse(matchT.Groups[3].Value);
                        _s5 = int.Parse(matchT.Groups[5].Value);


                        xi1 = _s1;
                        yi1 = _s2;
                        xi2 = Math.Abs(_s3);
                        yi2 = _s2;

                        xii1 = _s1;
                        yii1 = _s2;
                        xii2 = _s1;
                        yii2 = Math.Abs(_s4);

                        xiii1 = Math.Abs(_s3);
                        yiii1 = _s2;
                        xiii2 = _s1;
                        yiii2 = Math.Abs(_s4);

                        ShapeFactory shapeFactory = new ShapeFactory();
                        Shape c = shapeFactory.GetShape("triangle"); //new rectangles();
                        c.set(fill, pcolor, xi1, yi1, xi2, yi2, xii1, yii1, xii2, yii2, xiii1, yiii1, xiii2, yiii2);
                        c.Draw(gap);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("wrong parameter: should be like this \"triangle side, side, side\"");
                    }
                }

                // ----------------CLEAR------------------------//

                else if (matchClear.Success)
                {
                    Panel_Draw.Refresh();
                    this.Panel_Draw.BackgroundImage = null;
                }

                // ----------------RUN-----------------------//

                else if (matchRun.Success)
                {
                    btn_exec_Click(this, new EventArgs());
                }
                // ----------------RESET------------------------//
                else if (matchReset.Success)
                {
                    _s1 = 0;
                    _s2 = 0;
                    Xaxis = 0;
                    Yaxis = 0;
                    rotation = 0;
                }

                // ----------------MOVETO------------------------//

                else if (matchMT.Success)
                {
                    try
                    {
                        _s1 = int.Parse(matchMT.Groups[1].Value);
                        _s2 = int.Parse(matchMT.Groups[3].Value);

                        Xaxis = _s1;
                        Yaxis = _s2;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }
                // ----------------DRAWTO------------------------//

                else if (matchDT.Success)
                {
                    try
                    {
                        Pen p = new Pen(pcolor);
                        _s1 = int.Parse(matchMT.Groups[1].Value);
                        _s2 = int.Parse(matchMT.Groups[3].Value);


                        gap.DrawLine(p, _s1, _s2, Xaxis, Yaxis);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }

            }
            else
            {
                MessageBox.Show("Command doesnot exist");
            }
        }
    }




}
