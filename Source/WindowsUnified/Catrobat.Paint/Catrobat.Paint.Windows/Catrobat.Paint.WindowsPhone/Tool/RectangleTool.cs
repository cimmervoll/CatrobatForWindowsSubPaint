﻿using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class RectangleTool : ToolBase
    {
        public RectangleTool()
        {
            this.ToolType = ToolType.Rect;

            if (PocketPaintApplication.GetInstance() != null && PocketPaintApplication.GetInstance().PaintingAreaView != null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setVisibilityOfUcRectangleSelectionControl = Visibility.Visible;
            }
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform = (RotateTransform)arg;

            TranslateTransform lastTranslateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastTranslateTransformation();
            RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastRotateTransformation();

            if (lastTranslateTransform != null && lastRotateTransform == null)
            {
                TranslateTransform originTranslateTransform = new TranslateTransform();
                originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
                originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(originTranslateTransform);
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(lastTranslateTransform);
            }
            else if (lastTranslateTransform == null && lastRotateTransform != null)
            {
                rotateTransform.Angle += lastRotateTransform.Angle;
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            }
            else if (lastTranslateTransform != null && lastRotateTransform != null)
            {
                TranslateTransform originTranslateTransform = new TranslateTransform();
                originTranslateTransform.X = (lastTranslateTransform.X * -1.0);
                originTranslateTransform.Y = (lastTranslateTransform.Y * -1.0);

                rotateTransform.Angle += lastRotateTransform.Angle;

                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(originTranslateTransform);
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(lastTranslateTransform);
            }
            else
            {
                PocketPaintApplication.GetInstance().RectangleSelectionControl.addTransformation(rotateTransform);
            }          
        }

        public override void HandleUp(object arg)
        {
            
        }

        public override void Draw(object o)
        {
            var strokeThickness = PocketPaintApplication.GetInstance().PaintData.strokeThickness;

            var coordinate = (Point)o;
            coordinate.X += strokeThickness / 2.0;
            coordinate.Y += strokeThickness / 2.0;

            double height = PocketPaintApplication.GetInstance().BarRecEllShape.getHeight();
            double width = PocketPaintApplication.GetInstance().BarRecEllShape.getWidth();
            height -= strokeThickness;
            width -= strokeThickness;

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(coordinate, new Point(coordinate.X + width, coordinate.Y + height));

            RotateTransform lastRotateTransform = PocketPaintApplication.GetInstance().RectangleSelectionControl.getLastRotateTransformation();
            if (lastRotateTransform != null)
            {
                lastRotateTransform.CenterX = coordinate.X;
                lastRotateTransform.CenterY = coordinate.Y;

                //myRectangleGeometry.Transform = rotateTransform;
            }

            Path _path = new Path();
            _path.Fill = PocketPaintApplication.GetInstance().PaintData.colorSelected;
            _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;
            _path.StrokeThickness = strokeThickness;
            _path.StrokeLineJoin = PocketPaintApplication.GetInstance().RectangleSelectionControl.strokeLineJoinOfRectangleToDraw;
            _path.Data = myRectangleGeometry;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().RectangleSelectionControl.resetRectangleSelectionControl();
        }
    }
}
