using System;

namespace photo_printing_dispatcher
{
    public class Photo
    {
        public int Id { get; set; }
        public string OriginalPhotoUrl { get; set; }
        public string TransformedPhotoUrl { get; set; }
        public DateTime DatePrinted { get; set; }
        public DateTime DateTransformed { get; set; }
    }
}