using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /**************************************************************
* 建造者模式
* 将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示。
**************************************************************/

    /// <summary>
    /// 抽象形状购建者
    /// </summary>
    public abstract class ShapeBuilder
    {
        public abstract void DrawLine();
        public abstract void FillColor();
    }
    /// <summary>
    /// 圆形
    /// </summary>
    public class CircleBuilder : ShapeBuilder
    {
        public override void DrawLine()
        {
            Console.WriteLine("CircleBuilder.DrawLine  画个圆");
        }

        public override void FillColor()
        {
            Console.WriteLine("CircleBuilder.FillColor  填充色彩");
        }
    }
    /// <summary>
    /// 矩形
    /// </summary>
    public class RectangleBuilder : ShapeBuilder
    {
        public override void DrawLine()
        {
            Console.WriteLine("RectangleBuilder.DrawLine  画个矩形");
        }

        public override void FillColor()
        {
            Console.WriteLine("RectangleBuilder.FillColor  填充色彩");
        }
    }
    /// <summary>
    /// 形状建造者
    /// </summary>
    public class ShapeDirector
    {
        ShapeBuilder _shapeBuilder;
        public ShapeDirector(ShapeBuilder shapeBuilder)
        {
            _shapeBuilder = shapeBuilder;
        }
        public void DrawShape()
        {
            _shapeBuilder.DrawLine();
            _shapeBuilder.FillColor();
        }
    }
}
