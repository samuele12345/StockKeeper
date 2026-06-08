using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp1.Models
{
    // Forza EF Core a usare il nome tabella "SerialNumber" nel database.
    [Table("SerialNumber")]
    // esempio one to one 
    public class SerialNumber
    {
        public int Id { get; set; }
        // null! evita l'avviso dei nullable perché il valore sarà assegnato in seguito.
        // pone il metodo uguale a null e stabilisce che verrà istanziato più avanti
        public string Name { get; set; } = null!;

        // punto di domanda perchè non è necessario che venga assegnato un serial number ad un item nel momento stesso in cui 
        // viene creato questo modello
        //public int? ItemId { get; set; } // non è necessario perchè c'è già il collegamento in Items.cs

        /*
            qui non è necessaio mettere [ForeignKey("ItemId")] perchè diventerebbe un secondo collegamento tra tabelle oltre a 
            [ForeignKey("IdSerial")] in Items.cs, basta un solo collegamento (join)
            
        */
        public Items? Item { set; get; } // è collegato ad un modello item
    }
}
