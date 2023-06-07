using System;
using System.Windows;
using OpenTK.Wpf;
using System.Windows.Input;
using System.Windows.Controls;
using System.Runtime.Serialization;
using System.IO;

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
            /*
            for (int i = 0; i < Curve.Result.Length; i++)
            {
                Curve.RelativeCoordinateCurves[i].SetRa(Curve.RelativeCoordinateCurves[i].GetRa() + new Vector(0, 0.01));
                Curve.RelativeCoordinateCurves[i].SetRb(Curve.RelativeCoordinateCurves[i].GetRb() + new Vector(0, 0.01));
                Curve.RelativeCoordinateCurves[i].SetRc(Curve.RelativeCoordinateCurves[i].GetRc() + new Vector(0, 0.01));
            }*/
        }

        private void btnAddFrameClick(object sender, RoutedEventArgs e)
        {
            AddFrame();
        }

        private void AddFrame()
        {
            ListBoxItem item = new ListBoxItem();

            int index = framesListBox.Items.IndexOf(framesListBox.SelectedItem) + 1;

            
            item.Content = "FRAME " + (framesListBox.Items.Count + 1).ToString();
            item.Selected += listBoxItemSelected;
            framesListBox.Items.Insert(index, item);

            
            Curve[] frame = new Curve[Curve.Frames[index - 1].Length];
            frame = Curve.DeepClone(Curve.Frames[index - 1]);
            Curve.Frames.Insert(index, frame);

            framesListBox.SelectedItem = item;
            //MessageBox.Show(Curve.Frames.Count.ToString());
        }

        private void btnRemoveFrameClick(object sender, RoutedEventArgs e)
        {
            if (framesListBox.Items.Count > 1)
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
            Curve.selectedFrame = Int32.Parse(((ListBoxItem)sender).Content.ToString().Split(' ')[1]) - 1;

            ExampleScene.Render();


        }

        private void toggleButtonChecked(object sender, RoutedEventArgs e)
        {
           


        }

        private void onSaveClicked(object sender, RoutedEventArgs e)
        {
            //Curve.Serialize();

            try
            {
                Curve.WriteObject("FramesDataset.xml");
                MessageBox.Show(Directory.GetCurrentDirectory());
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
                MessageBox.Show("Press <Enter> to exit....");
                
            }
        }

        private void onOpenClicked(object sender, RoutedEventArgs e)
        {
            //Curve.Serialize();

            try
            {
                MessageBox.Show(Directory.GetCurrentDirectory());
                Curve.ReadObject("FramesDataset.xml");
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
                MessageBox.Show("Press <Enter> to exit....");
            }

            //framesListBox.Items.Clear();
            int count = framesListBox.Items.Count;
            
            for(int i = 0; i < Curve.Frames.Count; i++)
            {
             
                ListBoxItem item = new ListBoxItem();

                item.Content = "FRAME " + (i + 1).ToString();
                item.Selected += listBoxItemSelected;
                framesListBox.Items.Add(item);

                framesListBox.SelectedItem = item;
            }

            //for (int i = 0; i < count; i++)
            //{

             //   framesListBox.Items.RemoveAt(i);
            //}




        }
    }
}
