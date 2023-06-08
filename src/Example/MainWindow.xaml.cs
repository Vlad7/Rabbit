using System;
using System.Windows;
using OpenTK.Wpf;
using System.Windows.Input;
using System.Windows.Controls;
using System.Runtime.Serialization;
using System.IO;
using Microsoft.Win32;

namespace Example {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow {

        //private static Point oldPoint;

        public MainWindow() {
            InitializeComponent();

            // You can start and rely on the Settings property that may be set in XAML or elsewhere in the codebase.
            OpenTkControl.Start();

            // Or, you can suppy a settings object directly.
            InsetControl.Start(new GLWpfControlSettings()
            {
                MajorVersion = 2,
                MinorVersion = 1,
                RenderContinuously = false,
            });

            //One listbox item when load form

            Curve[] frame = Curve.TransformCurvesToRelativeCoordinates();
            Curve.Frames.Add(frame);

            ListBoxItem item = new ListBoxItem();

            item.Content = "FRAME 1";
            item.Selected += listBoxItemSelected;
            framesListBox.Items.Add(item);
            framesListBox.SelectedItem = item;

            Curve[] frame2 = Curve.TransformCurvesToRelativeCoordinates();
            Curve.Frames.Add(frame2);

            ListBoxItem item2 = new ListBoxItem();

            item2.Content = "FRAME 2";
            item2.Selected += listBoxItemSelected;
            framesListBox.Items.Add(item2);
            framesListBox.SelectedItem = item2;
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenTkControl_OnRender(TimeSpan delta) {

            ExampleScene.Render();

            OpenTkControl.Width = 2 * OpenTkControl.ActualHeight / 2.2;
        }

        private void InsetControl_OnRender(TimeSpan delta) {

            ExampleScene.Render();
            
        }

        private void OpenTKControl_OnMouseMove(object sender, MouseEventArgs e)
        {
            if ((bool)checkBoxEditing.IsChecked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //Coordinates of pointer in range [-0.5; 0.5]

                    double relativePointX = (e.GetPosition(this.OpenTkControl).X - this.OpenTkControl.ActualWidth / 2) / (this.OpenTkControl.ActualWidth / 2);
                    double relativePointY = (this.OpenTkControl.ActualHeight - e.GetPosition(this.OpenTkControl).Y - this.OpenTkControl.ActualHeight / 2) / (this.OpenTkControl.ActualHeight / 2);

                    ExampleScene.TryMovePoint(relativePointX, relativePointY);
                }
            
                ExampleScene.Render();                                    
            }
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {

        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {

        }
        
       

        public void checkBoxShowNodesChecked(object sender, RoutedEventArgs e)
        {
            ExampleScene.renderSelectionRectangles = true;
        }

        public void checkBoxShowNodesUnchecked(object sender, RoutedEventArgs e)
        {
            ExampleScene.renderSelectionRectangles = false;
        }

        public void checkBoxShowLinesChecked(object sender, RoutedEventArgs e)
        {
            ExampleScene.renderTangentLines = true;
        }

        public void checkBoxShowLinesUnchecked(object sender, RoutedEventArgs e)
        {
            ExampleScene.renderTangentLines = false;
        }



        private void OpenTKControl_OnMouseDown(object sender, MouseEventArgs e)
        {       
            double relativePointX = (e.GetPosition(this.OpenTkControl).X - this.OpenTkControl.ActualWidth / 2) / (this.OpenTkControl.ActualWidth / 2);
            double relativePointY = (this.OpenTkControl.ActualHeight - e.GetPosition(this.OpenTkControl).Y - this.OpenTkControl.ActualHeight / 2) / (this.OpenTkControl.ActualHeight / 2);

            ExampleScene.FindPoint(relativePointX, relativePointY);
        }

       
        private void RedrawButton_OnClick(object sender, RoutedEventArgs e) {
            // re-draw the inset control when the button is clicked.
            InsetControl.InvalidateVisual();
        }

        private void btnUpClick(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < Curve.Frames[(int)Curve.selectedFrame
                ].Length; i++)
            {
                Curve.Frames[(int)Curve.selectedFrame][i].SetRa(Curve.Frames[(int)Curve.selectedFrame][i].GetRa() + new Vector(0, 0.01));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRb(Curve.Frames[(int)Curve.selectedFrame][i].GetRb() + new Vector(0, 0.01));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRc(Curve.Frames[(int)Curve.selectedFrame][i].GetRc() + new Vector(0, 0.01));
            }
        }

        private void btnDownClick(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Curve.Frames[(int)Curve.selectedFrame].Length; i++)
            {
                Curve.Frames[(int)Curve.selectedFrame][i].SetRa(Curve.Frames[(int)Curve.selectedFrame][i].GetRa() + new Vector(0, -0.01));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRb(Curve.Frames[(int)Curve.selectedFrame][i].GetRb() + new Vector(0, -0.01));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRc(Curve.Frames[(int)Curve.selectedFrame][i].GetRc() + new Vector(0, -0.01));
            }
        }

        private void btnRightClick(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Curve.Frames[(int)Curve.selectedFrame].Length; i++)
            {
                Curve.Frames[(int)Curve.selectedFrame][i].SetRa(Curve.Frames[(int)Curve.selectedFrame][i].GetRa() + new Vector(0.01, 0));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRb(Curve.Frames[(int)Curve.selectedFrame][i].GetRb() + new Vector(0.01, 0));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRc(Curve.Frames[(int)Curve.selectedFrame][i].GetRc() + new Vector(0.01, 0));
            }
        }

        private void btnLeftClick(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Curve.Frames[(int)Curve.selectedFrame].Length; i++)
            {
                Curve.Frames[(int)Curve.selectedFrame][i].SetRa(Curve.Frames[(int)Curve.selectedFrame][i].GetRa() + new Vector(-0.01, 0));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRb(Curve.Frames[(int)Curve.selectedFrame][i].GetRb() + new Vector(-0.01, 0));
                Curve.Frames[(int)Curve.selectedFrame][i].SetRc(Curve.Frames[(int)Curve.selectedFrame][i].GetRc() + new Vector(-0.01, 0));
            }
        }

        private void sliderValueChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((Slider)sender).Value.ToString());
          
        }
        

        private void btnAddFrameClick(object sender, RoutedEventArgs e)
        {
            AddFrame();
        }

        private void AddFrame()
        {
            ListBoxItem item = new ListBoxItem();
            

            int index = framesListBox.Items.IndexOf(framesListBox.SelectedItem);

            
            item.Content = "FRAME " + (index + 2).ToString();
            item.Selected += listBoxItemSelected;
            framesListBox.Items.Insert(index + 1, item);

            
            Curve[] frame = new Curve[Curve.Frames[index].Length];
            frame = Curve.DeepClone(Curve.Frames[index]);
            Curve.Frames.Insert(index + 1, frame);

            framesListBox.SelectedItem = item;
            //MessageBox.Show(Curve.Frames.Count.ToString());



            //int upper_value = Int32.Parse(item.Content.ToString().Split(' ')[1]);

            for (int i = index + 2; i < framesListBox.Items.Count; i++)
            {
                int old_index = Int32.Parse(((ListBoxItem)framesListBox.Items[i]).Content.ToString().Split(' ')[1]);

                

                    ((ListBoxItem)framesListBox.Items[i]).Content = "FRAME " + (old_index + 1).ToString();

                
            }

            
        }

        private void btnRemoveFrameClick(object sender, RoutedEventArgs e)
        {
            if (framesListBox.Items.Count > 2)
            {
                int index = framesListBox.Items.IndexOf(framesListBox.SelectedItem);
                int upper_value = Int32.Parse(((ListBoxItem)framesListBox.SelectedItem).Content.ToString().Split(' ')[1]);

                Curve.Frames.RemoveAt(index);

                framesListBox.Items.Remove(framesListBox.SelectedItem);

                for (int i = 0; i < framesListBox.Items.Count; i++)
                {
                    int old_index = Int32.Parse(((ListBoxItem)framesListBox.Items[i]).Content.ToString().Split(' ')[1]);

                    if (old_index > upper_value)
                    {

                        ((ListBoxItem)framesListBox.Items[i]).Content = "FRAME " + (old_index - 1).ToString();

                    }
                }

                if (index > 0)
                    framesListBox.SelectedItem = framesListBox.Items[index - 1];
                else
                    framesListBox.SelectedItem = framesListBox.Items[index];




            }

            
        }

        private void listBoxItemSelected(object sender, RoutedEventArgs e)
        {
            Curve.selectedFrame = framesListBox.Items.IndexOf((ListBoxItem)sender);
         

            ExampleScene.Render();


        }

        private void playToggleButtonChecked(object sender, RoutedEventArgs e)
        {

            ExampleScene.animation = true;
            ExampleScene.currentAnimationFrame1 = 0;
            ExampleScene.currentAnimationFrame2 = 1;


        }

        private void playToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ExampleScene.animation = false;
        }

        private void onNewClicked(object sender, RoutedEventArgs e)
        {
            Curve.Frames.Clear();
            framesListBox.Items.Clear();

            Curve[] frame = Curve.TransformCurvesToRelativeCoordinates();
            Curve.Frames.Add(frame);

            ListBoxItem item = new ListBoxItem();

            item.Content = "FRAME 1";
            item.Selected += listBoxItemSelected;
            framesListBox.Items.Add(item);
            framesListBox.SelectedItem = item;

            Curve[] frame2 = Curve.TransformCurvesToRelativeCoordinates();
            Curve.Frames.Add(frame2);

            ListBoxItem item2 = new ListBoxItem();

            item2.Content = "FRAME 2";
            item2.Selected += listBoxItemSelected;
            framesListBox.Items.Add(item2);
            framesListBox.SelectedItem = item2;
        }

        private void onExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void onSaveClicked(object sender, RoutedEventArgs e)
        {
            string file_name;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml file (*.xml)|*.xml|C# file (*.cs)|*.cs";

            if (saveFileDialog.ShowDialog() == true)
            { 
                file_name = saveFileDialog.FileName;
              

                //Curve.Serialize();

                try
                {
                    Curve.WriteObject(file_name);
                    //ReadObject("DataContractSerializerExample.xml");
                }

                catch (SerializationException serExc)
                {
                    MessageBox.Show("Serialization Failed");
                    MessageBox.Show(serExc.Message);
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(
                    //"The serialization operation failed: {0} StackTrace: {1}",
                    //exc.Message, exc.StackTrace);
                }

                finally
                {
                    //MessageBox.Show("Press <Enter> to exit....");
                
                }
            }
            
        }

        private void onOpenClicked(object sender, RoutedEventArgs e)
        {
            var filePath = string.Empty;


            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            //openFileDialog.RestoreDirectory = true;

            //if (openFileDialog.ShowDialog() == true)
            //    ;
            //.Text = File.ReadAllText(openFileDialog.FileName);
            //Curve.Serialize();
          

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                try
                {
                    Curve.ReadObject(filePath);
                }

                catch (SerializationException serExc)
                {
                    MessageBox.Show("Serialization Failed");
                    MessageBox.Show(serExc.Message);
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(
                    //"The serialization operation failed: {0} StackTrace: {1}",
                    //exc.Message, exc.StackTrace);
                }

                finally
                {
                    //MessageBox.Show("Press <Enter> to exit....");
                }

                framesListBox.Items.Clear();

                ListBoxItem item = null;

                for (int i = 0; i < Curve.Frames.Count; i++)
                {

                    item = new ListBoxItem();

                    item.Content = "FRAME " + (i + 1).ToString();
                    item.Selected += listBoxItemSelected;
                    framesListBox.Items.Add(item);


                }

                framesListBox.SelectedItem = item;
            }

            

            //for (int i = 0; i < count; i++)
            //{

            //   framesListBox.Items.RemoveAt(i);
            //}




        }
    }
}
