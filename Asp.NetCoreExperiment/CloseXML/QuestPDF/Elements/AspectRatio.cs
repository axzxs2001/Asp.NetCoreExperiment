using System;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

namespace QuestPDF.Elements
{
    internal class AspectRatio : ContainerElement, ICacheable
    {
        public float Ratio { get; set; } = 1;
        public AspectRatioOption Option { get; set; } = AspectRatioOption.FitWidth;
        
        internal override SpacePlan Measure(Size availableSpace)
        {
            if(Child == null)
                return SpacePlan.FullRender(0, 0);
            
            var targetSize = GetTargetSize(availableSpace);
            
            if (targetSize.Height > availableSpace.Height + Size.Epsilon)
                return SpacePlan.Wrap();
            
            if (targetSize.Width > availableSpace.Width + Size.Epsilon)
                return SpacePlan.Wrap();

            var childSize = base.Measure(targetSize);

            if (childSize.Type == SpacePlanType.Wrap)
                return SpacePlan.Wrap();

            if (childSize.Type == SpacePlanType.PartialRender)
                return SpacePlan.PartialRender(targetSize);

            if (childSize.Type == SpacePlanType.FullRender)
                return SpacePlan.FullRender(targetSize);
            
            throw new NotSupportedException();
        }

        internal override void Draw(Size availableSpace)
        {
            if (Child == null)
                return;
            
            var size = GetTargetSize(availableSpace);
            base.Draw(size);
        }
        
        private Size GetTargetSize(Size availableSpace)
        {
            var spaceRatio = availableSpace.Width / availableSpace.Height;

            var fitHeight = new Size(availableSpace.Height * Ratio, availableSpace.Height) ;
            var fitWidth = new Size(availableSpace.Width, availableSpace.Width / Ratio);

            return Option switch
            {
                AspectRatioOption.FitWidth => fitWidth,
                AspectRatioOption.FitHeight => fitHeight,
                AspectRatioOption.FitArea => Ratio < spaceRatio ? fitHeight : fitWidth,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}