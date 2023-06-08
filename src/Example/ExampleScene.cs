using System;
using System.Diagnostics;
using System.Windows;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace Example {

    [Serializable]
    [DataContract(Name = "Curve")]   
    public class Curve
    {
        [DataMember()]
        private Vector r_a;

        [DataMember()]
        private Vector r_b;

        [DataMember()]
        private Vector r_c;
        
        public Vector GetRa()
        {
            return r_a;
        }
        
        public void SetRa(Vector val)
        {
            r_a = val;
        }

        public Vector GetRb()
        {
            return r_b;
        }

        public void SetRb(Vector val)
        {
            r_b = val;
        }

        public Vector GetRc()
        {
            return r_c;
        }

        public void SetRc(Vector val)
        {
            r_c = val;
        }

        static double fieldWidth = 230;
        static double fieldHeight = 250;

        public static int? selectedFrame = null;

        public static Curve[] UpLeftCoordinateCurvesInit = new Curve[] {new Curve(new Vector(180, 15), new Vector(190, 28),new Vector(170, 54)),
                                             new Curve(new Vector(170, 54), new Vector (163.5, 60), new Vector(160.5, 62.5)),
                                             new Curve(new Vector(160.5, 62.5), new Vector(157.5, 71),new Vector(157, 79.5)),

                                             new Curve(new Vector(157, 79.5), new Vector(169, 87),new Vector(167.5, 94.5)),
                                             new Curve(new Vector(167.5, 94.5), new Vector(169, 100),new Vector(172.5, 104)),
                                             new Curve(new Vector(172.5, 104), new Vector(176.5, 110),new Vector(176.5, 114.5)),

                                             new Curve(new Vector(176.5, 114.5), new Vector(177, 123),new Vector(167.5, 125)),
                                             new Curve(new Vector(167.5, 125), new Vector(170, 130),new Vector(172, 135)),
                                             new Curve(new Vector(172, 135), new Vector(175, 151.5),new Vector(169, 164)),      //

                                             new Curve(new Vector(169, 164), new Vector(167.5, 164.5),new Vector(166, 163.5)),
                                             new Curve(new Vector(166, 163.5), new Vector(162, 173),new Vector(157, 176)),
                                             new Curve(new Vector(157, 176), new Vector(157, 193),new Vector(175, 212.5)),

                                             new Curve(new Vector(175, 212.5), new Vector(188, 215),new Vector(190, 223)),
                                             new Curve(new Vector(190, 223), new Vector(183.5, 224),new Vector(176, 224.5)),
                                             new Curve(new Vector(176, 224.5), new Vector(174, 225),new Vector(170, 224)),

                                             new Curve(new Vector(170, 224), new Vector(175.5, 226.5),new Vector(178, 228)),
                                             new Curve(new Vector(178, 228), new Vector(179.5, 240.5),new Vector(175.5, 242)),
                                             new Curve(new Vector(175.5, 242), new Vector(176.5, 246),new Vector(169.5, 242.5)), //18

                                             new Curve(new Vector(169.5, 242.5), new Vector(168, 242),new Vector(166.5, 242)),
                                             new Curve(new Vector(166.5, 242), new Vector(165, 240.5),new Vector(163, 240)),
                                             new Curve(new Vector(163, 240), new Vector(155, 237.5),new Vector(151, 234.5)),

                                             new Curve(new Vector(151, 234.5), new Vector(150, 218.5),new Vector(137, 193.5)),
                                             new Curve(new Vector(137, 193.5), new Vector(132, 195.5),new Vector(128, 198.5)),
                                             new Curve(new Vector(128, 198.5), new Vector(134, 204.5),new Vector(140, 207.5)),    //24

                                             new Curve(new Vector(140, 207.5), new Vector(139.5, 212.5),new Vector(128.5, 211)),
                                             new Curve(new Vector(128.5, 211), new Vector(120.5, 217.5),new Vector(94, 210)),
                                             new Curve(new Vector(94, 210), new Vector(87, 215),new Vector(79, 221.5)),

                                             new Curve(new Vector(79, 221.5), new Vector(86.5, 228),new Vector(110.5, 229)),
                                             new Curve(new Vector(110.5, 229), new Vector(124, 238.5),new Vector(122, 251)),
                                             new Curve(new Vector(122, 251), new Vector(120, 252.5),new Vector(119, 254.5)),       //30

                                             new Curve(new Vector(119, 254.5), new Vector(109.5, 255),new Vector(97.5, 244)),
                                             new Curve(new Vector(97.5, 244), new Vector(63.5, 231.5),new Vector(62, 227.5)),
                                             new Curve(new Vector(62, 227.5), new Vector(67.5, 221.5),new Vector(68, 211.5)),

                                             new Curve(new Vector(68, 211.5), new Vector(64, 204),new Vector(57, 204)),
                                             new Curve(new Vector(57, 204), new Vector(48.5, 211),new Vector(41, 205)),
                                             new Curve(new Vector(41, 205), new Vector(33.5, 199),new Vector(41, 190)),//36

                                             new Curve(new Vector(41, 190), new Vector(38.5, 180.5),new Vector(40, 177.5)),
                                             new Curve(new Vector(40, 177.5), new Vector(43, 145),new Vector(52, 137)),
                                             new Curve(new Vector(52, 137), new Vector(55.5, 136),new Vector(58, 130)),

                                             new Curve(new Vector(58, 130), new Vector(58, 125),new Vector(65, 121)),
                                             new Curve(new Vector(65, 121), new Vector(68.5, 117),new Vector(82, 115)),
                                             new Curve(new Vector(82, 115), new Vector(87, 115),new Vector(94, 114)),

                                             new Curve(new Vector(94, 114), new Vector(99.5, 113),new Vector(102.5, 112.5)),
                                             new Curve(new Vector(102.5, 112.5), new Vector(111.5, 113.5),new Vector(115.5, 110)),
                                             new Curve(new Vector(115.5, 110), new Vector(122.5, 110.5),new Vector(125, 105)),

                                             new Curve(new Vector(125, 105), new Vector(130, 104.5),new Vector(130.5, 91)),
                                             new Curve(new Vector(130.5, 91), new Vector(130, 104.5),new Vector(130, 97.5)),
                                             new Curve(new Vector(130, 97.5), new Vector(130.5, 91),new Vector(132.5, 87)), //48

                                             new Curve(new Vector(132.5, 87), new Vector(134.5, 82),new Vector(130, 75)),
                                             new Curve(new Vector(130, 75), new Vector(99, 60),new Vector(89, 26)),
                                             new Curve(new Vector(89, 26), new Vector(87, 10),new Vector(119, 33)),

                                             new Curve(new Vector(119, 33), new Vector(134, 42),new Vector(144.5, 77.5)),
                                             new Curve(new Vector(144.5, 77.5), new Vector(144.5, 77.5),new Vector(149, 77)),
                                             new Curve(new Vector(149, 77), new Vector(142, 57),new Vector(163, 27)),

                                             new Curve(new Vector(163, 27), new Vector(174.5, 15.5),new Vector(180, 15))    //55
                                                };


        public static Curve[] RelativeCoordinateCurves;
        //public static Curve[] RelativeCoordinateCurves2;
        public static Curve[] Result;

        public static List<Curve[]> Frames;

        static Curve()
        {
            //RelativeCoordinateCurves = new Curve[UpLeftCoordinateCurvesInit.Length];

            //Curve.TransformCurvesToRelativeCoordinates();

            //RelativeCoordinateCurves2 = new Curve[UpLeftCoordinateCurvesInit.Length];

            Result = new Curve[UpLeftCoordinateCurvesInit.Length];

            

            /*
            for (int i = 0; i < RelativeCoordinateCurves2.Length; i++)
            {
                RelativeCoordinateCurves2[i] = new Curve(new Vector(0, 0), new Vector(0, 0), new Vector(0, 0));
            }*/

            
            //Curve.RelativeCoordinateCurves2 = DeepClone(RelativeCoordinateCurves);


            Frames = new List<Curve[]>();

            //Frames.Add(RelativeCoordinateCurves);
            //Frames.Add(RelativeCoordinateCurves2);

            //Delta[50].r_b = new Vector(0.1, 0);
        }

        public static void WriteObject(string fileName)
        {
            Console.WriteLine(
                "Creating a Curve object and serializing it.");

            var curve = Curve.Frames[0][0];

            FileStream writer = new FileStream(fileName, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(List<Curve[]>));
            ser.WriteObject(writer, Curve.Frames);
            writer.Close();
        }

        public static void ReadObject(string fileName)
        {
            Console.WriteLine("Deserializing an instance of the object.");
            FileStream fs = new FileStream(fileName,
            FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(List<Curve[]>));

            // Deserialize the data and read it from the instance.
            Curve.Frames =
                (List<Curve[]>)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();

            selectedFrame = 0;
            //Console.WriteLine(String.Format("{0} {1}, ID: {2}",
            //frame[0][0].GetRa(), frame[0][0].GetRb(),
            //frame[0][0].GetRc()));
        }

        public static void Serialize ()
        {
            var curve = Curve.Frames[0][0];

            var serializer = new XmlSerializer(typeof(Curve));
            using (var writer = new StreamWriter("curves.xml"))
            {
                serializer.Serialize(writer, curve);
            }
        }

        public static Curve [] DeepClone(Curve [] obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (Curve[])formatter.Deserialize(ms);
            }
        }

        public Curve(Vector Ra, Vector Rb, Vector Rc)
        {
            this.r_a = Ra;
            this.r_b = Rb;
            this.r_c = Rc;
        }

        public static Curve[] TransformCurvesToRelativeCoordinates()
        {
            Curve[] frame = new Curve[Curve.UpLeftCoordinateCurvesInit.Length];

            for (int i = 0; i < UpLeftCoordinateCurvesInit.Length; i++)
            {
                frame[i] = new Curve(new Vector(TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRa().X), TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRa().Y)), new Vector(TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRb().X), TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRb().Y)), new Vector(TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRc().X), TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRc().Y)));
            }

            return frame;
        }

        private static double TransformPointX(double x)
        {
            return (x - fieldWidth / 2 ) / (fieldWidth / 2);
        }

        private static double TransformPointY(double y)
        {
            return ((fieldHeight - (y - 10)) - fieldHeight / 2) / (fieldHeight / 2);
        }
        public static Vector[] CalculateCurve(Vector ra, Vector rb, Vector rc)
        {
            Vector Ra = ra;
            Vector Rb = rb;
            Vector Rc = rc;

            //Bezier curve of second order

            Double Wa, Wb, Wc;

            Wa = 1;
            Wb = 1;
            Wc = 1;

            Double u = 0;

            Double step = 0.005;

            int array_length = (int)(1 / step) + 1;

            Vector[] CurvePoints = new Vector[array_length];

            Vector R;

            for (int i = 0; i < array_length; i++)
            {
                u = step * i;

                R = (Ra * Wa * (1 - u) * (1 - u) + 2 * Rb * Wb * u * (1 - u) + Rc * Wc * u * u) / (Wa * (1 - u) * (1 - u) + 2 * Wb * u * (1 - u) + Wc * u * u);

                CurvePoints[i] = R;
            }

            return CurvePoints;
        }

    }


    /// Example class handling the rendering for OpenGL.
    public static class ExampleScene {

        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        //static string path = @"D:\Projects\Rabbit2\UpLeftCoordinateCurvesInit.txt";

        static double distance_x = selectionRectWidth / 2;
        static double distance_y = selectionRectWidth / 2;

        static double selectionRectWidth = 0.01;

        static List<int> indexes = new List<int>();
        static List<int> orders = new List<int>();
        static int index = 0;
        static int order = 0;

        public static float ellapsed = 0;
        public static int sign = 1;

        public static bool renderTangentLines = false;
        public static bool renderSelectionRectangles = false;

        public static bool animation = false;

        public static int currentAnimationFrame1 = 0;
        public static int currentAnimationFrame2 = 1;



        //in vec4 a_Position;

        static ExampleScene()
        {
            
        }

        public static void Ready() {
            Console.WriteLine("GlWpfControl is now ready");
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
        }

        public static void Render(float alpha = 1.0f) {

            int index = (int)Curve.selectedFrame;
            var hue = (float)_stopwatch.Elapsed.TotalSeconds * 0.15f % 1;

            ellapsed += 0.01f * sign;

            if (ellapsed >= 1)
            {
                if (currentAnimationFrame1 != Curve.Frames.Count - 1)
                {
                    currentAnimationFrame1 += 1;
                }
                else
                {
                    currentAnimationFrame1 = 0;
                }
                if (currentAnimationFrame2 != Curve.Frames.Count - 1)
                {
                    currentAnimationFrame2 += 1;
                }
                else
                {
                    currentAnimationFrame2 = 0;
                }
                
               
                sign = 1;
               
                ellapsed = 0;
            }
            /*(
            if(ellapsed <= 0)
            {
                sign = 1;
            }*/

            var c = Color4.FromHsv(new Vector4(alpha * hue, alpha * 0.75f, alpha * 0.75f, alpha));
            
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            //GL.Begin(PrimitiveType.Triangles);

            //GL.Color4(Color4.Red);
            //GL.Vertex2(0.0f, 0.7f);

            //GL.Color4(Color4.Green);
            //GL.Vertex2(0.58f, -0.5f);

            //GL.Color4(Color4.Blue);
            //GL.Vertex2(-0.58f, -0.5f);

            // Draw the UpLeftCoordinateCurvesInit
            //GL.DrawArrays(PrimitiveType.UpLeftCoordinateCurvesInit, 0, 1);

            //GL.VertexAttrib2(100, 10, 10);

            //GL.End();
            
            
            DrawCurves(index, ellapsed);
            

            if (renderTangentLines)
            {
                DrawLines(index);
            }

            if(renderSelectionRectangles)
            {
                DrawSelectionRectangles(index);
            }

            GL.Finish();
        }

        public static void DrawSelectionRectangles(int id)
        {
            if (!animation)
            {
                for (int i = 0; i < Curve.Frames[id].Length; i++)
                {
                    DrawSelectionRectangle(Curve.Frames[id][i].GetRa());
                    DrawSelectionRectangle(Curve.Frames[id][i].GetRb());
                    DrawSelectionRectangle(Curve.Frames[id][i].GetRc());
                }
            }

            else
            {
                for (int i = 0; i < Curve.Result.Length; i++)
                {

                    DrawSelectionRectangle(Curve.Result[i].GetRa());
                    DrawSelectionRectangle(Curve.Result[i].GetRb());
                    DrawSelectionRectangle(Curve.Result[i].GetRc());
                }
            }
            /*
            for (int i = 0; i < Curve.Result.Length; i++)
            {
                
                DrawSelectionRectangle(Curve.Result[i].GetRa());
                DrawSelectionRectangle(Curve.Result[i].GetRb());
                DrawSelectionRectangle(Curve.Result[i].GetRc());
            }*/

        }

        private static void DrawLines(int id)
        {
            if (!animation)
            {
                for (int i = 0; i < Curve.Frames[id].Length; i++)
                {
                    DrawLine(Curve.Frames[id][i].GetRa(), Curve.Frames[id][i].GetRb(), Curve.Frames[id][i].GetRc());
                }
            }
            else
            {
                for (int i = 0; i < Curve.Result.Length; i++)
                {
                    DrawLine(Curve.Result[i].GetRa(), Curve.Result[i].GetRb(), Curve.Result[i].GetRc());
                }
            }
            /*
            */
        }

        private static void DrawLine(Vector ra, Vector rb, Vector rc)
        {
            Vector Ra = ra;
            Vector Rb = rb;
            Vector Rc = rc;

            GL.PointSize(3);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0, 255, 0);

            GL.Vertex2(Ra.X, Ra.Y);
            GL.Vertex2(Rb.X, Rb.Y);

            GL.Vertex2(Rb.X, Rb.Y);
            GL.Vertex2(Rc.X, Rc.Y);
        
            GL.End();
        }

        private static void DrawCurves(int index, float t)
        {
            if (!animation)
            {
                Curve[] curves = Curve.Frames[index];

                for (int i = 0; i < curves.Length; i++)
                {
                    Vector resRa = curves[i].GetRa();
                    Vector resRb = curves[i].GetRb();
                    Vector resRc = curves[i].GetRc();

                    Vector[] curve_points = Curve.CalculateCurve(curves[i].GetRa(), curves[i].GetRb(), curves[i].GetRc());

                    DrawCurve(curve_points);
                }
            }
            else
            {

                //int id = currentAnimationFrame;

                for (int i = 0; i < Curve.Frames[currentAnimationFrame1].Length; i++)
                {
                    Vector resRa = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRa(), Curve.Frames[currentAnimationFrame2][i].GetRa(), t);
                    Vector resRb = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRb(), Curve.Frames[currentAnimationFrame2][i].GetRb(), t);
                    Vector resRc = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRc(), Curve.Frames[currentAnimationFrame2][i].GetRc(), t);

                    Curve.Result[i] = new Curve(resRa, resRb, resRc);

                    //new Curve(Curve.RelativeCoordinateCurves[i].GetRa(), Curve.RelativeCoordinateCurves[i].GetRb(), Curve.RelativeCoordinateCurves[i].GetRc())
                    Vector[] curve_points = Curve.CalculateCurve(Curve.Frames[currentAnimationFrame1][i].GetRa(), Curve.Frames[currentAnimationFrame1][i].GetRb(), Curve.Frames[currentAnimationFrame1][i].GetRc());
                    Vector[] curve_points2 = Curve.CalculateCurve(Curve.Frames[currentAnimationFrame2][i].GetRa(), Curve.Frames[currentAnimationFrame2][i].GetRb(), Curve.Frames[currentAnimationFrame2][i].GetRc());

                    Vector[] animation = new Vector[curve_points.Length];

                    for (int j = 0; j < curve_points.Length; j++)
                    {
                        animation[j] = CalculateAnimationFrame(curve_points[j], curve_points2[j], t);
                    }

                    DrawCurve(animation);

                }
            }
        }

        private static void DrawCurve(Vector[] curvePoints)
        {
            GL.PointSize(3);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(0, 0, 0);

           

            foreach(Vector R in curvePoints)
            {
                GL.Vertex2(R.X, R.Y);
            }

            GL.End();
        }

        

     

        private static Vector CalculateAnimationFrame(Vector R1, Vector R2, double t)
        {
            return R1 * (1 - t) + R2 * t;
        }

        private static void DrawSelectionRectangle(Vector R)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0, 255, 0);

            GL.Vertex2(R.X - selectionRectWidth, R.Y - selectionRectWidth);
            GL.Vertex2(R.X - selectionRectWidth, R.Y + selectionRectWidth);
            GL.Vertex2(R.X + selectionRectWidth, R.Y + selectionRectWidth);
            GL.Vertex2(R.X + selectionRectWidth, R.Y - selectionRectWidth);

            GL.End();
        }

        public static void FindPoint(double rel_cursor_x, double rel_cursor_y)
        {
            int frame_index = (int)Curve.selectedFrame;

            for(int i = 0; i < Curve.Frames[frame_index].Length; i++)
            {
                index = i;

                Vector rel_point = Curve.Frames[frame_index][i].GetRa();
                Vector rel_point2 = Curve.Frames[frame_index][i].GetRb();
                Vector rel_point3 = Curve.Frames[frame_index][i].GetRc();

                if (CheckInSquare(rel_cursor_x, rel_cursor_y, rel_point))
                {

                    distance_x = rel_cursor_x - rel_point.X;
                    distance_y = rel_cursor_y - rel_point.Y;


                    order = 1;
                    //RelativeCoordinateCurves[i].Ra.X += rel_cursor_x;
                    break;
                    
                    //RelativeCoordinateCurves[i].Ra.X = rel_cursor_x;
                    //RelativeCoordinateCurves[i].Ra.Y = rel_cursor_y;
                }

                if (CheckInSquare(rel_cursor_x, rel_cursor_y, rel_point2))
                {

                    distance_x = rel_cursor_x - rel_point2.X;
                    distance_y = rel_cursor_y - rel_point2.Y;

                    order = 2;
                   
                    //RelativeCoordinateCurves[i].Rb.X += rel_cursor_x;
                    break;
                    
                   
                    //RelativeCoordinateCurves[i].Ra.X = rel_cursor_x;
                    //RelativeCoordinateCurves[i].Ra.Y = rel_cursor_y;
                }

                if (CheckInSquare(rel_cursor_x, rel_cursor_y, rel_point3))
                {

                    distance_x = rel_cursor_x - rel_point3.X;
                    distance_y = rel_cursor_y - rel_point3.Y;

                    order = 3;
                 
                    break;
                    //RelativeCoordinateCurves[i].Rc.X += rel_cursor_x;
                
                    //RelativeCoordinateCurves[i].Ra.X = rel_cursor_x;
                    //RelativeCoordinateCurves[i].Ra.Y = rel_cursor_y;
                }

                order = 0;
            }

            //Render();
            
        }

        public static void TryMovePoint(double rel_cursor_x, double rel_cursor_y)
        {
            int frame_index = (int)Curve.selectedFrame;

            Vector r = new Vector(rel_cursor_x + distance_x, rel_cursor_y + distance_y);

            if (order == 1)
            {                  
                Curve.Frames[frame_index][index].SetRa(r);

                Curve.Frames[frame_index][(index - 1) < 0 ? Curve.Frames[frame_index].Length - 1 : index - 1].SetRc(r);         
            }

            if (order == 2)
            {
                Curve.Frames[frame_index][index].SetRb(r);
            }

            if (order == 3)
            {
                Curve.Frames[frame_index][index].SetRc(r);

                Curve.Frames[frame_index][(index+1)% Curve.Frames[frame_index].Length].SetRa(r);
             }
        }

        public static bool CheckInSquare(double rel_cursor_x, double rel_cursor_y, Vector rel_point)
        {
            if (rel_cursor_x >= rel_point.X - selectionRectWidth / 2 && rel_cursor_x <= rel_point.X + selectionRectWidth / 2 && rel_cursor_y >= rel_point.Y - selectionRectWidth / 2 && rel_cursor_y <= rel_point.Y + selectionRectWidth / 2)
            {                
                return true;
            }
            return false;

        }
        

        
    }
}
