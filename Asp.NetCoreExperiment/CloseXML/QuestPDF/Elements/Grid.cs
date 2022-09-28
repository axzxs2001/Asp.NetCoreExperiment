﻿using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPDF.Elements
{
    internal class GridElement
    {
        public int Columns { get; set; } = 1;
        public Element? Child { get; set; }
    }
    
    internal class Grid : IComponent
    {
        public const int DefaultColumnsCount = 12;
        
        public List<GridElement> Children { get; } = new List<GridElement>();
        public Queue<GridElement> ChildrenQueue { get; set; } = new Queue<GridElement>();
        public int ColumnsCount { get; set; } = DefaultColumnsCount;

        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;
        public float VerticalSpacing { get; set; } = 0;
        public float HorizontalSpacing { get; set; } = 0;
        
        public void Compose(IContainer container)
        {
            ChildrenQueue = new Queue<GridElement>(Children);
            
            container.Column(column =>
            {
                column.Spacing(VerticalSpacing);
                
                while (ChildrenQueue.Any())
                    column.Item().Row(BuildRow);
            });
        }
        
        IEnumerable<GridElement> GetRowElements()
        {
            var rowLength = 0;
                
            while (ChildrenQueue.Any())
            {
                var element = ChildrenQueue.Peek();
                            
                if (rowLength + element.Columns > ColumnsCount)
                    break;

                rowLength += element.Columns;
                yield return ChildrenQueue.Dequeue();
            }
        }
            
        void BuildRow(RowDescriptor row)
        {
            row.Spacing(HorizontalSpacing);
                
            var elements = GetRowElements().ToList();
            var columnsWidth = elements.Sum(x => x.Columns);
            var emptySpace = ColumnsCount - columnsWidth;
            var hasEmptySpace = emptySpace >= Size.Epsilon;

            if (Alignment == HorizontalAlignment.Center)
                emptySpace /= 2;
            
            if (hasEmptySpace && Alignment != HorizontalAlignment.Left)
                row.RelativeItem(emptySpace);
                
            elements.ForEach(x => row.RelativeItem(x.Columns).Element(x.Child));

            if (hasEmptySpace && Alignment != HorizontalAlignment.Right)
                row.RelativeItem(emptySpace);
        }
    }
}