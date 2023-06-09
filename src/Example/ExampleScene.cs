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
        private Vector3 r_a;

        [DataMember()]
        private Vector3 r_b;

        [DataMember()]
        private Vector3 r_c;
        
        public Vector3 GetRa()
        {
            return r_a;
        }
        
        public void SetRa(Vector3 val)
        {
            r_a = val;
        }

        public Vector3 GetRb()
        {
            return r_b;
        }

        public void SetRb(Vector3 val)
        {
            r_b = val;
        }

        public Vector3 GetRc()
        {
            return r_c;
        }

        public void SetRc(Vector3 val)
        {
            r_c = val;
        }

        static double fieldWidth = 230;
        static double fieldHeight = 250;

        public static int? selectedFrame = null;

        public static Curve[] UpLeftCoordinateCurvesInit = new Curve[] {new Curve(new Vector3(180, 15, 1), new Vector3(190, 28, 1),new Vector3(170, 54, 1)),
                                             new Curve(new Vector3(170, 54, 1), new Vector3(163.5f, 60, 1), new Vector3(160.5f, 62.5f, 1)),
                                             new Curve(new Vector3(160.5f, 62.5f, 1), new Vector3(157.5f, 71, 1), new Vector3(157, 79.5f, 1)),

                                             new Curve(new Vector3(157, 79.5f, 1), new Vector3(169, 87, 1),new Vector3(167.5f, 94.5f, 1)),
                                             new Curve(new Vector3(167.5f, 94.5f, 1), new Vector3(169, 100, 1),new Vector3(172.5f, 104, 1)),
                                             new Curve(new Vector3(172.5f, 104, 1), new Vector3(176.5f, 110, 1),new Vector3(176.5f, 114.5f, 1)),

                                             new Curve(new Vector3(176.5f, 114.5f, 1), new Vector3(177, 123, 1),new Vector3(167.5f, 125, 1)),
                                             new Curve(new Vector3(167.5f, 125, 1), new Vector3(170, 130, 1),new Vector3(172, 135,1)),
                                             new Curve(new Vector3(172, 135, 1), new Vector3(175, 151.5f, 1),new Vector3(169, 164, 1)),      //

                                             new Curve(new Vector3(169, 164, 1), new Vector3(167.5f, 164.5f, 1), new Vector3(166, 163.5f, 1)),
                                             new Curve(new Vector3(166, 163.5f, 1), new Vector3(162, 173, 1),new Vector3(157, 176, 1)),
                                             new Curve(new Vector3(157, 176, 1), new Vector3(157, 193, 1),new Vector3(175, 212.5f, 1)),

                                             new Curve(new Vector3(175, 212.5f, 1), new Vector3(188, 215, 1),new Vector3(190, 223, 1)),
                                             new Curve(new Vector3(190, 223, 1), new Vector3(183.5f, 224, 1),new Vector3(176, 224.5f, 1)),
                                             new Curve(new Vector3(176, 224.5f, 1), new Vector3(174, 225, 1),new Vector3(170, 224, 1)),

                                             new Curve(new Vector3(170, 224, 1), new Vector3(175.5f, 226.5f, 1),new Vector3(178, 228, 1)),
                                             new Curve(new Vector3(178, 228, 1), new Vector3(179.5f, 240.5f, 1),new Vector3(175.5f, 242, 1)),
                                             new Curve(new Vector3(175.5f, 242, 1), new Vector3(176.5f, 246, 1),new Vector3(169.5f, 242.5f, 1)), //18

                                             new Curve(new Vector3(169.5f, 242.5f, 1), new Vector3(168, 242, 1),new Vector3(166.5f, 242, 1)),
                                             new Curve(new Vector3(166.5f, 242, 1), new Vector3(165, 240.5f, 1),new Vector3(163, 240, 1)),
                                             new Curve(new Vector3(163, 240, 1), new Vector3(155, 237.5f, 1),new Vector3(151, 234.5f, 1)),

                                             new Curve(new Vector3(151, 234.5f, 1), new Vector3(150, 218.5f, 1),new Vector3(137, 193.5f, 1)),
                                             new Curve(new Vector3(137, 193.5f, 1), new Vector3(132, 195.5f, 1),new Vector3(128, 198.5f, 1)),
                                             new Curve(new Vector3(128, 198.5f, 1), new Vector3(134, 204.5f, 1),new Vector3(140, 207.5f, 1)),    //24

                                             new Curve(new Vector3(140, 207.5f, 1), new Vector3(139.5f, 212.5f, 1),new Vector3(128.5f, 211, 1)),
                                             new Curve(new Vector3(128.5f, 211, 1), new Vector3(120.5f, 217.5f, 1),new Vector3(94, 210, 1)),
                                             new Curve(new Vector3(94, 210, 1), new Vector3(87, 215, 1),new Vector3(79, 221.5f, 1)),

                                             new Curve(new Vector3(79, 221.5f, 1), new Vector3(86.5f, 228, 1),new Vector3(110.5f, 229, 1)),
                                             new Curve(new Vector3(110.5f, 229, 1), new Vector3(124, 238.5f, 1),new Vector3(122, 251, 1)),
                                             new Curve(new Vector3(122, 251, 1), new Vector3(120, 252.5f, 1),new Vector3(119, 254.5f, 1)),       //30

                                             new Curve(new Vector3(119, 254.5f, 1), new Vector3(109.5f, 255, 1),new Vector3(97.5f, 244, 1)),
                                             new Curve(new Vector3(97.5f, 244, 1), new Vector3(63.5f, 231.5f, 1),new Vector3(62, 227.5f, 1)),
                                             new Curve(new Vector3(62, 227.5f, 1), new Vector3(67.5f, 221.5f, 1),new Vector3(68, 211.5f, 1)),

                                             new Curve(new Vector3(68, 211.5f, 1), new Vector3(64, 204, 1),new Vector3(57, 204, 1)),
                                             new Curve(new Vector3(57, 204, 1), new Vector3(48.5f, 211, 1),new Vector3(41, 205, 1)),
                                             new Curve(new Vector3(41, 205, 1), new Vector3(33.5f, 199, 1),new Vector3(41, 190, 1)),//36

                                             new Curve(new Vector3(41, 190, 1), new Vector3(38.5f, 180.5f, 1),new Vector3(40, 177.5f, 1)),
                                             new Curve(new Vector3(40, 177.5f, 1), new Vector3(43, 145, 1),new Vector3(52, 137, 1)),
                                             new Curve(new Vector3(52, 137, 1), new Vector3(55.5f, 136, 1),new Vector3(58, 130, 1)),

                                             new Curve(new Vector3(58, 130, 1), new Vector3(58, 125, 1),new Vector3(65, 121, 1)),
                                             new Curve(new Vector3(65, 121, 1), new Vector3(68.5f, 117, 1),new Vector3(82, 115, 1)),
                                             new Curve(new Vector3(82, 115, 1), new Vector3(87, 115, 1),new Vector3(94, 114, 1)),

                                             new Curve(new Vector3(94, 114, 1), new Vector3(99.5f, 113, 1),new Vector3(102.5f, 112.5f, 1)),
                                             new Curve(new Vector3(102.5f, 112.5f, 1), new Vector3(111.5f, 113.5f, 1),new Vector3(115.5f, 110, 1)),
                                             new Curve(new Vector3(115.5f, 110, 1), new Vector3(122.5f, 110.5f, 1),new Vector3(125, 105, 1)),

                                             new Curve(new Vector3(125, 105, 1), new Vector3(130, 104.5f, 1),new Vector3(130.5f, 91, 1)),
                                             new Curve(new Vector3(130.5f, 91, 1), new Vector3(130, 104.5f, 1),new Vector3(130, 97.5f, 1)),
                                             new Curve(new Vector3(130, 97.5f, 1), new Vector3(130.5f, 91, 1),new Vector3(132.5f, 87, 1)), //48

                                             new Curve(new Vector3(132.5f, 87, 1), new Vector3(134.5f, 82, 1),new Vector3(130, 75, 1)),
                                             new Curve(new Vector3(130, 75, 1), new Vector3(99, 60, 1),new Vector3(89, 26, 1)),
                                             new Curve(new Vector3(89, 26, 1), new Vector3(87, 10, 1),new Vector3(119, 33, 1)),

                                             new Curve(new Vector3(119, 33, 1), new Vector3(134, 42, 1),new Vector3(144.5f, 77.5f, 1)),
                                             new Curve(new Vector3(144.5f, 77.5f, 1), new Vector3(144.5f, 77.5f, 1),new Vector3(149, 77, 1)),
                                             new Curve(new Vector3(149, 77, 1), new Vector3(142, 57, 1),new Vector3(163, 27, 1)),

                                             new Curve(new Vector3(163, 27, 1), new Vector3(174.5f, 15.5f, 1),new Vector3(180, 15, 1))    //55
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

        public Curve(Vector3 Ra, Vector3 Rb, Vector3 Rc)
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
                Vector3 pointRa = new Vector3((float)TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRa().X), (float)TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRa().Y), 1);
                Vector3 pointRb = new Vector3((float)TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRb().X), (float)TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRb().Y), 1);
                Vector3 pointRc = new Vector3((float)TransformPointX(Curve.UpLeftCoordinateCurvesInit[i].GetRc().X), (float)TransformPointY(Curve.UpLeftCoordinateCurvesInit[i].GetRc().Y), 1);
                
                frame[i] = new Curve(pointRa, pointRb, pointRc);
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
        public static Vector3[] CalculateCurve(Vector3 ra, Vector3 rb, Vector3 rc)
        {
            Vector3 Ra = ra;
            Vector3 Rb = rb;
            Vector3 Rc = rc;

            //Bezier curve of second order

            float Wa, Wb, Wc;

            Wa = 1;
            Wb = 1;
            Wc = 1;

            float u = 0;

            float step = 0.005f;

            int array_length = (int)(1 / step) + 1;

            Vector3[] CurvePoints = new Vector3[array_length]; 

            Vector3 R;

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

        static float distance_x = selectionRectWidth / 2;
        static float distance_y = selectionRectWidth / 2;

        static float selectionRectWidth = 0.01f;

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

        private static void DrawLine(Vector3 ra, Vector3 rb, Vector3 rc)
        {
            Vector3 Ra = ra;
            Vector3 Rb = rb;
            Vector3 Rc = rc;

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
                    Vector3 resRa = curves[i].GetRa();
                    Vector3 resRb = curves[i].GetRb();
                    Vector3 resRc = curves[i].GetRc();

                    Vector3[] curve_points = Curve.CalculateCurve(curves[i].GetRa(), curves[i].GetRb(), curves[i].GetRc());

                    DrawCurve(curve_points);
                }
            }
            else
            {

                //int id = currentAnimationFrame;

                for (int i = 0; i < Curve.Frames[currentAnimationFrame1].Length; i++)
                {
                    Vector3 resRa = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRa(), Curve.Frames[currentAnimationFrame2][i].GetRa(), t);
                    Vector3 resRb = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRb(), Curve.Frames[currentAnimationFrame2][i].GetRb(), t);
                    Vector3 resRc = CalculateAnimationFrame(Curve.Frames[currentAnimationFrame1][i].GetRc(), Curve.Frames[currentAnimationFrame2][i].GetRc(), t);

                    Curve.Result[i] = new Curve(resRa, resRb, resRc);

                    //new Curve(Curve.RelativeCoordinateCurves[i].GetRa(), Curve.RelativeCoordinateCurves[i].GetRb(), Curve.RelativeCoordinateCurves[i].GetRc())
                    Vector3[] curve_points = Curve.CalculateCurve(Curve.Frames[currentAnimationFrame1][i].GetRa(), Curve.Frames[currentAnimationFrame1][i].GetRb(), Curve.Frames[currentAnimationFrame1][i].GetRc());
                    Vector3[] curve_points2 = Curve.CalculateCurve(Curve.Frames[currentAnimationFrame2][i].GetRa(), Curve.Frames[currentAnimationFrame2][i].GetRb(), Curve.Frames[currentAnimationFrame2][i].GetRc());

                    Vector3[] animation = new Vector3[curve_points.Length];

                    for (int j = 0; j < curve_points.Length; j++)
                    {
                        animation[j] = CalculateAnimationFrame(curve_points[j], curve_points2[j], t);
                    }

                    DrawCurve(animation);

                }
            }
        }

        private static void DrawCurve(Vector3[] curvePoints)
        {
            GL.PointSize(3);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(0, 0, 0);

           

            foreach(Vector3 R in curvePoints)
            {
                GL.Vertex2(R.X, R.Y);
            }

            GL.End();
        }

        

     

        private static Vector3 CalculateAnimationFrame(Vector3 R1, Vector3 R2, float t)
        {
            return R1 * (1 - t) + R2 * t;
        }

        private static void DrawSelectionRectangle(Vector3 R)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0, 255, 0);

            GL.Vertex2(R.X - selectionRectWidth, R.Y - selectionRectWidth);
            GL.Vertex2(R.X - selectionRectWidth, R.Y + selectionRectWidth);
            GL.Vertex2(R.X + selectionRectWidth, R.Y + selectionRectWidth);
            GL.Vertex2(R.X + selectionRectWidth, R.Y - selectionRectWidth);

            GL.End();
        }

        public static void FindPoint(float rel_cursor_x, float rel_cursor_y)
        {
            int frame_index = (int)Curve.selectedFrame;

            for(int i = 0; i < Curve.Frames[frame_index].Length; i++)
            {
                index = i;

                Vector3 rel_point = Curve.Frames[frame_index][i].GetRa();
                Vector3 rel_point2 = Curve.Frames[frame_index][i].GetRb();
                Vector3 rel_point3 = Curve.Frames[frame_index][i].GetRc();

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

        public static void TryMovePoint(float rel_cursor_x, float rel_cursor_y)
        {
            int frame_index = (int)Curve.selectedFrame;

            Vector3 r = new Vector3(rel_cursor_x + distance_x, rel_cursor_y + distance_y, 1);

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

        public static bool CheckInSquare(double rel_cursor_x, double rel_cursor_y, Vector3 rel_point)
        {
            if (rel_cursor_x >= rel_point.X - selectionRectWidth / 2 && rel_cursor_x <= rel_point.X + selectionRectWidth / 2 && rel_cursor_y >= rel_point.Y - selectionRectWidth / 2 && rel_cursor_y <= rel_point.Y + selectionRectWidth / 2)
            {                
                return true;
            }
            return false;

        }
        

        
    }
}
