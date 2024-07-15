namespace apibronco.bronco.com.br.DTOs
{
    public class InvoiceDTO
    {
        //    id: z.string (),
        //customerId: z.string (),
        //amount: z.coerce.number(),
        //status: z.enum(['pending', 'paid']),
        //date: z.string (),

        public string id { get; set; }

        public string customerId { get; set; }

        public decimal amount { get; set; }

        public string status { get; set; }

        public DateTime date { get; set; }
    }
}
