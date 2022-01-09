using System.ComponentModel.DataAnnotations;

namespace GPU_api.Models
{
    public class GPU_model
    {
        [Key]
        public int sku { get; set; }
        public static int id = 0;
        public string model_name { get; set; }
        public string brand { get; set; }
        public double price { get; set; }
        public string store_name { get; set; }
        public string location { get; set; }
        public int quantity { get; set; }

        public GPU_model()
        {
            id++;
            this.sku = id;
        }
    }
}