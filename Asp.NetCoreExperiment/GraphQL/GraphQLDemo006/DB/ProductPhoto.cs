using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo06
{
    public partial class ProductPhoto
    {
        public ProductPhoto()
        {
            ProductProductPhotos = new HashSet<ProductProductPhoto>();
        }

        public int ProductPhotoId { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public byte[] LargePhoto { get; set; }
        public string LargePhotoFileName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductProductPhoto> ProductProductPhotos { get; set; }
    }
}
