namespace APIConsumo.Models {
    public class Producto {

        public int id { get; set; }
        public string? nombre { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public Fabricante? fabricante { get; set; }

    }
}
