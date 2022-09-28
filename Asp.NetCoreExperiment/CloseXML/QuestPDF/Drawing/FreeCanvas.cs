﻿using QuestPDF.Infrastructure;
using SkiaSharp;

namespace QuestPDF.Drawing
{
    internal class FreeCanvas : ICanvas, IRenderingCanvas
    {
        #region IRenderingCanvas

        public void BeginDocument()
        {
            
        }

        public void EndDocument()
        {
            
        }

        public void BeginPage(Size size)
        {
            
        }

        public void EndPage()
        {
            
        }

        #endregion

        #region ICanvas

        public void Translate(Position vector)
        {
            
        }

        public void DrawRectangle(Position vector, Size size, string color)
        {
            
        }

        public void DrawText(SKTextBlob skTextBlob, Position position, TextStyle style)
        {
            
        }

        public void DrawImage(SKImage image, Position position, Size size)
        {
            
        }

        public void DrawHyperlink(string url, Size size)
        {
           
        }

        public void DrawSectionLink(string sectionName, Size size)
        {
            
        }

        public void DrawSection(string sectionName)
        {
            
        }

        public void Rotate(float angle)
        {
            
        }

        public void Scale(float scaleX, float scaleY)
        {
            
        }

        #endregion
    }
}